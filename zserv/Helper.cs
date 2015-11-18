using System;
using System.Linq;
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

		/// <summary>
		/// Returns whether a given string is null or empty.
		/// </summary>
		/// <param name="self">The string to check.</param>
		public static bool Empty(this string self)
		{
			return String.IsNullOrEmpty (self);
		}

		/// <summary>
		/// Strips the leading char[s] if the given string starts with one of them or the first char, if none are given.
		/// </summary>
		/// <returns>The string without the removed char[s].</returns>
		/// <param name="chars">The char[s] to remove or none to strip the first char.</param>
		public static string StripLeadingChar(this string self, params char[] chars)
		{
			if (chars.Length == 0)
				return self.Substring (1);

			int offset = 0;
			foreach(char c in self)
			{
				if (chars.Contains (c))
					offset++;
				else
					break;
			}

			return self.Substring (offset);
		}

		/// <summary>
		/// Strips the trailing char[s] if the given string ends with one of them or the last char, if none are given.
		/// </summary>
		/// <returns>The string without the removed char[s].</returns>
		/// <param name="chars">The char[s] to remove or none to strip the last char.</param>
		public static string StripTrailingChar(this string self, params char[] chars)
		{
			if (chars.Length == 0)
				return self.Substring (0, self.Length -1);

			int offset = 0;

			var selfChars = self.ToCharArray ().Reverse ();
			foreach(char c in selfChars)
			{
				if (chars.Contains (c))
					offset++;
				else
					break;
			}

			return self.Substring (0, self.Length - offset);
		}

		/// <summary>
		/// Returns whether a given string starts with one of the given chars.
		/// </summary>
		/// <returns><c>true</c>, if the given string starts with one of the chars, <c>false</c> otherwise.</returns>
		/// <param name="self">The string to test.</param>
		/// <param name="test">All characters to test for.</param>
		public static bool StartsWith(this string self, params char[] test)
		{
			if (String.IsNullOrEmpty(self))
				return false;

			foreach (char c in test)
				if (self [0] == c)
					return true;

			return false;
		}

	}
}

