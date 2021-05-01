using System.Reflection;

namespace Protocol.Autowire
{
	internal static class MethodInfoExtensions
	{
		internal static string FullName(this MethodInfo method)
		{
			return string.Format("{0}.{1}", method.DeclaringType.FullName, method.Name);
		}
	}
}
