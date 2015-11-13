using System;
using System.IO;
using System.Collections.Generic;

namespace zserv.filesytem
{
	/// <summary>
	/// Provides a wrapper for the directives config file.
	/// </summary>
	public class DirectiveParser
	{
		public List<Tuple<string, string>> directives;

		public readonly string[] validDirectives = { "nozip", "nozipi", "priv", "nodot" };

		public DirectiveParser (string file)
		{
			parse (System.IO.File.ReadAllLines(file));
		}

		public DirectiveParser(string[] lines)
		{
			parse (lines);
		}

		private void parse(string[] lines)
		{
			// parse directives line by line.
			// directives may contain a wildcard (*) at the end.
			foreach(var line in lines)
			{
				line = line.Trim ();

				// at first split directives and path.
				int separatorIndex = line.IndexOfAny(new char[] {' ', '\t'});

				if(separatorIndex < 1 || line.Length < 3)
					throw new MalformedDirectiveException(line, "The line does not contain valid syntax.");

				// check if path is in quotes
				if(line['0'] == '\"')
				{
					line = line.Substring (1); // remove first quote.

					// set the new separator index at the ending quotes position
					separatorIndex = line.IndexOf('\"');

					// if no ending quote exists ...
					if(separatorIndex < 1)
						throw new MalformedDirectiveException(line, "The line does not contain a closing quote or quotes are empty.");
				}

				// get path & strip quotes
				string path = line.Substring (0, separatorIndex).Replace("\"", ""),
					// get the directives and strip comments
					directiveStr = line.Substring (separatorIndex).TrimStart ().Split(new char[] {' ', '\t'}, 2)[0];

				// the path _must_ start with a slash
				// this provides forward compatibility if the server will ever support relative paths
				if(path[0] != '/')
					throw new MalformedDirectiveException(line, "The path must start with a slash");

				path = DirHandler.resolvePath(path);

				string[] directives = directiveStr.Split (',');

				foreach (var i in directives) 
				{
					if (i.StartsWith (new char[] { '+', '-', '!' })) // strip prefix if any
						i = i.Substring (1);

					if (i.Empty ())
						throw new MalformedDirectiveException (line, "Contains an empty or prefix-only directive");

					// lets ignore invalid directives for now
					//if(!validDirectives.Contains(i))
						//throw new MalformedDirectiveException (line, "Directive %s is unknown".incorporate(i));

					// ... anything more to validate?
				}

				// if we're still here, its probably valid
				// so add it to the list
				this.directives.Add (new Tuple<string, string> (path, directives));

			} // end foreach line
		}

		public static List<Tuple<string, string>> findMatching(string path)
		{
			//from d in directives
			//	where d.Item1 == 
		}

		/// <summary>
		/// Checks whether challenger is matching path.
		/// </summary>
		/// <returns><c>true</c>, if they match, <c>false</c> otherwise.</returns>
		/// <param name="path">The path to match against.</param>
		/// <param name="challenger">The pattern.</param>
		public static bool isPathMatching(string path, string challenger)
		{
			string[] pathParts = path.Split ('/'),
				challengerParts = path.Split ('/');

			for (int i = 0; i < challengerParts.Length; i++) {
				if (pathParts.Length == i) // if the challenger is longer, no macht is possible
					return false;

				if (!dirNameMatching (pathParts [i], challengerParts [i]))
					return false;
			}


			return true;
		}

		/// <summary>
		/// Checks if the directory or file $name matches $challenger.
		/// This function supports wildcards (*), which may be placeholder for any character or nothing.
		/// </summary>
		/// <returns><c>true</c>, if name matches challenger, <c>false</c> otherwise.</returns>
		/// <param name="name">Name.</param>
		/// <param name="challenger">Challenger.</param>
		public static bool dirNameMatching(string name, string challenger)
		{
			if (name.Contains ("/") || challenger.Contains ("/"))
				throw new ArgumentException ("Directory or file name may not contain a slash!");

			if (challenger.Contains ('*')) { // wildcard
				if (challenger.Replace ("*", "").Length == 0)
					return true; // wildcard-only string

				// filter all parts which need to match
				// add slashes (can't be contained) to filter start/end
				string[] matchP = ("/" + challenger + "/").Split("*");

				foreach(string match in matchP)
				{
					if(match.StartsWith("/")) // start of the string, e.g. *.txt
					{
						// strip the leading slash
						match = match.Substring (1);

						if (match.Length == 0) // wildcard at start (like *.cs)
							continue;

						// if not (like priv*), the string must start with this phrase
						if (!name.StartsWith (match))
							return false;

						// if it starts with it, this part gets removed to not taint further matches
						name = name.Substring (match.Length);
					}
					else if (match.EndsWith("/")) // end of string, e.g. test*
					{
						// strip the leading slash
						match = match.Substring (1);

						if (match.Length == 0) // wildcard the end
							continue;

						// since we're at the end of the string (no wildcard can be 
						// after this) the string has to end with the match
						if (!name.EndsWith (match))
							return false;

						// theoretically name should be stripped, but we're at the end anyway.

					}
					else // middle of the string; e.g. priv*2*.data
					{
						throw new NotImplementedException ("Wildcards in the middle of a string are currently not supported!");
					}
				}

				// if we didn't return by now, they match
				return true; 
			} else
				return name == challenger;
		}
	}
}

