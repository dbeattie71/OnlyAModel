using System;

namespace Messages
{
	public static class StringExtensions
	{
		public static bool EqualsIgnoreCase(this string s, string t)
		{
			return s.Equals(t, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
