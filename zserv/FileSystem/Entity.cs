using System;
using zserv;

namespace zserv.filesytem
{
	/// <summary>
	/// Represents a filesystem entity (file or folder).
	/// </summary>
	public class Entity
	{
		protected string name;
		protected DirectiveList directives;
		protected bool isDir;

		public Entity ()
		{
			
		}
	}
}

