using System;
using System.IO;
using zserv;

namespace zserv.filesytem
{
	/// <summary>
	/// Represents a filesystem entity (file or folder).
	/// </summary>
	public class Entity
	{
		protected string path, // absolut path
			relativePath, // path in relation to /
			directiveString; // params

		private Entity parent;

		protected DirectiveList directives;
		protected bool isDir {
			get {
				return System.IO.Directory.Exists (path);
			}
			private set { isDir = value; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="zserv.filesytem.Entity"/> class.
		/// </summary>
		/// <param name="parser">A parser for all matching directives.</param>
		/// <param name="absolutePath">The absolute path to the file/dir.</param>
		/// <param name="relativePath">Relative path.</param>
		/// <param name="directiveString">Directive string.</param>
		public Entity (DirectiveParser parser, string absolutePath, string relativePath, string directiveString)
		{
			path = absolutePath;
			this.relativePath = relativePath;

			// parse permissions
		}

		public bool IsDirectiveSet(string name)
		{
			return directives.IsDirectiveSet (name);
		}

		public bool IsDirectiveNotSet(string name)
		{
			return !IsDirectiveSet (name);
		}
	}
}

