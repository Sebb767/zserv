using System;
using System.Collections.Generic;
using zserv;

namespace zserv.filesytem
{
	/// <summary>
	/// This class provides methods for handling directories and URLs as well as a cache
	/// </summary>
	public class DirHandler
	{
		private Dictionary<string, Entity> cache = new Dictionary<string, Entity>();

		public DirHandler ()
		{
		}


		/// <summary>
		/// Strips a path of all double-dots, dots etc and prevents path traversal.
		/// </summary>
		/// <returns>The clean path.</returns>
		/// <param name="path">Path.</param>
		public static string resolvePath(string path)
		{
			// contains the current dirstack (never goes under /)
			Stack<string> dirstack = new Stack<string> ();

			var dirs = path.Split ('/');
			foreach(string dir in dirs)
			{
				if(dir == "..") // pop a dir
				{
					dirstack.Pop ();
				}
				else if (dir != ".") // ignore /./
				{
					dirstack.Push (dir);
				}
			}

			string result = "", current = dirstack.Pop();
			while(current != null)
			{
				result = "/" + current + result;
			}

			if (result.Empty ())
				return "/";

			return result;
		}
	}
}

