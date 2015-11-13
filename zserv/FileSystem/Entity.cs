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
			private set;
		}

		public Entity (Entity parent, string absolutePath, string relativePath, string directiveString)
		{
			parent = parent;

			path = absolutePath;
			relativePath = relativePath;

			//directives = new DirectiveList()
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

