using System;

namespace Messages.Models
{
	public static class ClassExtensions
	{
		public static string DisplayName(this Class klass)
		{
			return klass.ToString().Split('_')[0];
		}
	}
}
