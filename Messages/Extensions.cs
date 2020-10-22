using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Messages
{
	public static class Extensions
	{
		public static string FullName(this MethodInfo method)
		{
			return string.Format("{0}.{1}", method.DeclaringType.FullName, method.Name);
		}

		public static void RetainAll<K,V>(this IDictionary<K,V> dictionary, IEnumerable<K> retain)
		{
			foreach(var key in dictionary.Keys)
			{
				if(!retain.Contains(key))
				{
					dictionary.Remove(key);
				}
			}
		}
	}
}
