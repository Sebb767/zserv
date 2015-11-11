using System;
using System.Text.RegularExpressions;

namespace zserv
{
	/// <summary>
	/// Contains some helper functions.
	/// </summary>
	public static class Helper
	{
		/// <summary>
		/// Incorporates the specified objects into self.
		/// </summary>
		/// <param name="self">The string to incorporate into.</param>
		/// <param name="format">The objects to enter.</param>
		public static string incorporate(this string self, params object[] insert)
		{
			int i = 0;
			return string.Format(Regex.Replace(self,"%.",m=>("{"+ ++i +"}")), insert);
		}
	}
}

