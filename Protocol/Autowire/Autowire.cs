using Core.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Protocol.Autowire
{
	public static class Autowire
	{
		public static EventHandler<MessageEventArgs> CreateHandler(IEnumerable<object> observers)
		{
			var unmarshallers = FindUnmarshallers();
			var types = unmarshallers.Values.Select(o => o.ReturnType);
			var handlers = FindHandlers(observers);
			/* Discard handlers for types we don't have unmarshallers for. */
			foreach(var type in handlers.Keys)
			{
				if(!types.Contains(type))
				{
					handlers.Remove(type);
				}
			}
			/* Discard unmarshallers for types we don't have handlers for. */
			foreach(var kvp in unmarshallers)
			{
				if(!handlers.ContainsKey(kvp.Value.ReturnType))
				{
					unmarshallers.Remove(kvp.Key);
				}
			}
			return (server, args) =>
			{
				if(unmarshallers.TryGetValue(args.Message.Type, out MethodInfo unmarshaller))
				{
					var obj = unmarshaller.Invoke(null, new object[] { args });
					foreach (var handler in handlers[obj.GetType()])
					{
						handler.Invoke(args, obj);
					}
				}
			};
		}

		private static IDictionary<byte, MethodInfo> FindUnmarshallers()
		{
			var methods = Assembly.GetAssembly(typeof(Autowire))
				.GetTypes()
				.SelectMany(o => o.GetMethods())
				.Where(o => o.GetCustomAttribute<UnmarshallerAttribute>() != null);
			ValidateUnmarshallers(methods);
			return methods
				.GroupBy(o => o.GetCustomAttribute<UnmarshallerAttribute>().MessageType)
				.ToDictionary(o => o.Key, o => o.Single());
		}

		private static void ValidateUnmarshallers(IEnumerable<MethodInfo> methods)
		{
			foreach(var method in methods)
			{
				if (!method.IsStatic)
				{
					var msg = string.Format("{0} cannot be applied to non-static method {1}.", typeof(UnmarshallerAttribute).FullName, method.FullName());
					throw new Exception(msg);
				}
				if (!method.IsPublic)
				{
					var msg = string.Format("{0} cannot be applied to non-public method {1}.", typeof(UnmarshallerAttribute).FullName, method.FullName());
					throw new Exception(msg);
				}
				if (method.ReturnType == typeof(void))
				{
					var msg = string.Format("{0} cannot be applied to method {1} with return type void.", typeof(UnmarshallerAttribute).FullName, method.FullName());
					throw new Exception(msg);
				}
				var p = method.GetParameters();
				if (p.Count() != 1 || p.Single().ParameterType != typeof(MessageEventArgs))
				{
					var types = string.Join(", ", p.Select(o => o.ParameterType.FullName));
					var msg = string.Format("{0} cannot be applied to method {1} with parameters ({2}).", typeof(UnmarshallerAttribute).FullName, method.FullName(), types);
					throw new Exception(msg);
				}
			}
		}

		private static IDictionary<Type, List<Action<MessageEventArgs, object>>> FindHandlers(IEnumerable<object> observers)
		{
			var handlers = new Dictionary<Type, List<Action<MessageEventArgs, object>>>();
			foreach(var observer in observers)
			{
				var methods = observer.GetType().GetMethods().Where(o => o.GetCustomAttribute<HandlerAttribute>() != null);
				ValidateHandlers(methods);
				foreach(var method in methods)
				{
					var type = method.GetParameters().Last().ParameterType;
					if(!handlers.ContainsKey(type))
					{
						handlers.Add(type, new List<Action<MessageEventArgs, object>>());
					}
					handlers[type].Add((args, payload) => method.Invoke(observer, new object[] { args, payload }));
				}
			}
			return handlers;
		}

		private static void ValidateHandlers(IEnumerable<MethodInfo> methods)
		{
			foreach (var method in methods)
			{
				if (method.IsStatic)
				{
					var msg = string.Format("{0} cannot be applied to static method {1}.", typeof(HandlerAttribute).FullName, method.FullName());
					throw new Exception(msg);
				}
				if (!method.IsPublic)
				{
					var msg = string.Format("{0} cannot be applied to non-public method {1}.", typeof(HandlerAttribute).FullName, method.FullName());
					throw new Exception(msg);
				}
				var p = method.GetParameters();
				if (p.Count() != 2 || p.First().ParameterType != typeof(MessageEventArgs))
				{
					var types = string.Join(", ", p.Select(o => o.ParameterType.FullName));
					var msg = string.Format("{0} cannot be applied to method {1} with parameters ({2}).", typeof(HandlerAttribute).FullName, method.FullName(), types);
					throw new Exception(msg);
				}
			}
		}
	}
}
