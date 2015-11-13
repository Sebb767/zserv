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


	}
}

