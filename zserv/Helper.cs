using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

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

		/// <summary>
		/// Executes action for each element in self.
		/// </summary>
		/// <param name="self">A list of T.</param>
		/// <param name="action">An action to call for every T.</param>
		public static void each<T>(this IList<T> self, Action<T> action)
		{
			foreach (T t in self)
				action (t);
		}

		/// <summary>
		/// Executes action for each element in self.
		/// </summary>
		/// <param name="self">A list of T.</param>
		/// <param name="action">An action to call for every T.</param>
		/// <returns>The count of elements in self.</returns>
		public static int each<T>(this IList<T> self, Action<T, int> action)
		{
			int i = 0;
			foreach (T t in self)
				action (t, i++);

			return i;
		}

		/// <summary>
		/// Executes action for each element in self.
		/// </summary>
		/// <param name="self">A list of T.</param>
		/// <param name="action">An action to call for every T.</param>
		/// <returns>The count of elements in self.</returns>
		public static int each<T>(this IList<T> self, Action<int, T> action)
		{
			int i = 0;
			foreach (T t in self)
				action (i++, t);

			return i;
		}
	}
}

