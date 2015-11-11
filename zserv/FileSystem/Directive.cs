using System;

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
		}

		/// <summary>
		/// Whether the directive is enforced (non-overwriteable).
		/// </summary>
		/// <value><c>true</c> if enforced; otherwise, <c>false</c>.</value>
		public bool enforced {
			get;
			private set;
		}

		/// <summary>
		/// Whether this directive is applicable for subdirectories.
		/// </summary>
		/// <value><c>true</c> if applicable for subdirectories; otherwise, <c>false</c>.</value>
		public bool recursive {
			get;
			private set;
		}

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
				throw new UnknownDirectiveException("The directive %s is invalid.");
		}
	}
}

