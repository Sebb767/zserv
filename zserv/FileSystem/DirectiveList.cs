using System;
using System.Linq;
using zserv;
using System.Collections.Generic;

namespace zserv.filesytem
{
	/// <summary>
	/// Class to hold all directives.
	/// </summary>
	public class DirectiveList
	{
		private List<Directive> directives = new List<Directive>();
		private char prefix = null;

		public DirectiveList (params Directive[] directives)
		{
			foreach (Directive d in directives)
				Add (d);
			
		}

		public DirectiveList(DirectiveParser presets, string path)
		{
			throw new NotImplementedException ();
		}

		public void Add(Directive d)
		{
			if(IsDirectiveSet(d.name))			
				throw new DirectiveAlreadySetException (d.name);

			directives.Add (d);
		}

		public bool IsDirectiveSet(string name)
		{
			return (from dir in directives 
					where name == dir.name && dir.enabled
				select dir).Count() > 0;
		}

		public bool IsDirectiveNotSet(string name)
		{
			return !IsDirectiveSet (name);
		}

		/// <summary>
		/// Merges two directory lists (i.e. a parsed one and one of a parent directory).
		/// </summary>
		/// <returns>The merged directory directives.</returns>
		/// <param name="parent">The parent directory.</param>
		/// <param name="child">The subdirectory.</param>
		public static DirectiveList MergeLists(DirectiveList parent, DirectiveList child)
		{
			var merged = new DirectiveList ();

			// take over all child directives
			merged.directives = child.directives;

			// take over all recursive directives from the parent
			(from direc in parent.directives where direc.recursive select direc)
				.each(d => {
					var parentdirec = (from x in child.directives
						where d == x select x).First();

					// if the directive is already set only take it over if it's enforced
					if(parentdirec != null && parentdirec.enforced)
					{
						// remove the directive from the child (remember: == is overridden)
						merged.directives.Remove(parentdirec);

						// and add the parent one
						merged.directives.Add(parentdirec);
					}
					else // if not, just take it over
						merged.Add(d);
				});

			// yey, done :)
			return merged;
		}
	}
}
