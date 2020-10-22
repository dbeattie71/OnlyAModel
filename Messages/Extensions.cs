using System.Reflection;

namespace Messages
{
	public static class Extensions
	{
		public static string FullName(this MethodInfo method)
		{
			return string.Format("{0}.{1}", method.DeclaringType.FullName, method.Name);
		}
	}
}
