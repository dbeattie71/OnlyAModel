using System;

namespace Protocol
{
	public static class StringExtensions
	{
		public static bool EqualsIgnoreCase(this string s, string t)
		{
			return s.Equals(t, StringComparison.InvariantCultureIgnoreCase);
		}
	}
}
