using System;
using zserv;

namespace zserv.filesytem
{
	/// <summary>
	/// Represents a directory
	/// </summary>
	public class Directive
	{
		/// <summary>
		/// The name of the directive.
		/// </summary>
		/// <value>The name.</value>
		public string name {
			get;
			private set;
		} = "";

		/// <summary>
		/// Whether the directive is enforced (non-overwriteable).
		/// </summary>
		/// <value><c>true</c> if enforced; otherwise, <c>false</c>.</value>
		public bool enforced {
			get;
			private set;
		} = false;

		/// <summary>
		/// Whether this directive is applicable for subdirectories.
		/// </summary>
		/// <value><c>true</c> if applicable for subdirectories; otherwise, <c>false</c>.</value>
		public bool recursive {
			get;
			private set;
		} = false;

		/// <summary>
		/// Gets a value indicating whether this <see cref="zserv.filesytem.Directive"/> is enabled.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool enabled {
			get;
			private set;
		} = true;

		public Directive (string name, bool enforced, bool recursive)
		{
			this.name = name;
			this.enforced = enforced;
			this.recursive = recursive;
		}

		public Directive (string bare)
		{
			/* At first we need to filter the prefix. */
			if(bare.Length < 1)
				throw new UnknownDirectiveException("The directive %s is invalid.".incorporate(bare));

			bool hasPrefix = false;

			switch (bare[0]) // check the prefix
			{
			case '!':
				enforced = true;
				goto case '+';
			case '+':
				recursive = true;
				hasPrefix = true;
			case '-': 
				enabled = false;
				hasPrefix = true;
				break;
			}

			if (hasPrefix) // strip prefix
				this.name = name.Substring (1);
			else
				this.name = name;
		}

		public static bool Equals(Directive a, Directive b)
		{
			return a == b;
		}

		public static bool operator ==(Directive a, Directive b)
		{
			return a.name == b.name;
		}

		public static bool operator !=(Directive a, Directive b)
		{
			return a.name != b.name;
		}
	}
}


