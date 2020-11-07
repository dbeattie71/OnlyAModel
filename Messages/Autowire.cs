using Core;
using Core.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Messages
{
	public static class Autowire
	{
		public static EventHandler<MessageEventArgs> CreateMessageHandler(int version, params object[] observers)
		{
			return CreateMessageHandler(version, new List<Assembly>() { Assembly.GetAssembly(typeof(Autowire)) }, observers);
		}

		public static EventHandler<MessageEventArgs> CreateMessageHandler(int version, IEnumerable<Assembly> assemblies, params object[] observers)
		{
			var factories = FindFactories(version, assemblies);
			var payloads = factories.Values.Select(o => o.ReturnType).Distinct();
			var handlers = FindHandlers(observers);
			/* Discard handlers for payloads we don't have factories for. */
			foreach(var type in handlers.Keys)
			{
				if(!payloads.Contains(type))
				{
					handlers.Remove(type);
				}
			}
			/* Discard factories for payloads we don't have handlers for. */
			foreach(var type in factories.Keys)
			{
				if(!handlers.ContainsKey(factories[type].ReturnType))
				{
					factories.Remove(type);
				}
			}
			var actions = factories.Keys
				.ToDictionary(o => o, o => (Action<Server, MessageEventArgs>)((server, args) =>
				{
					var type = args.Message.Type;
					var payload = factories[type].Invoke(null, new object[] { args.Message.Payload });
					handlers[payload.GetType()].Invoke(server, args, payload);
				}));
			return (server, args) =>
			{
				if (actions.TryGetValue(args.Message.Type, out Action<Server, MessageEventArgs> action))
				{
					action.Invoke((Server)server, args);
				}
			};
		}

		private static IDictionary<byte, MethodInfo> FindFactories(int version, IEnumerable<Assembly> assemblies)
		{
			var methods = assemblies
				.SelectMany(o => o.GetTypes())
				.SelectMany(o => o.GetMethods())
				.Where(o => o.GetCustomAttribute<AutowiredFactoryAttribute>() != null);
			ValidateFactoryMethods(methods);
			return methods.Where(o => o.GetCustomAttribute<AutowiredFactoryAttribute>().Version <= version)
				.GroupBy(o => o.GetCustomAttribute<AutowiredFactoryAttribute>().Type)
				.ToDictionary(o => o.Key, o => o.OrderBy(o => o.GetCustomAttribute<AutowiredFactoryAttribute>().Version).Last());
		}

		private static void ValidateFactoryMethods(IEnumerable<MethodInfo> methods)
		{
			foreach (var method in methods)
			{
				if (!method.IsStatic)
				{
					var msg = string.Format("{0} cannot be applied to non-static method {1}.", typeof(AutowiredFactoryAttribute).FullName, method.FullName());
					throw new Exception(msg);
				}
				if (!method.IsPublic)
				{
					var msg = string.Format("{0} cannot be applied to non-public method {1}.", typeof(AutowiredFactoryAttribute).FullName, method.FullName());
					throw new Exception(msg);
				}
				if (method.ReturnType == typeof(void))
				{
					var msg = string.Format("{0} cannot be applied to method {1} with return type void.", typeof(AutowiredFactoryAttribute).FullName, method.FullName());
					throw new Exception(msg);
				}
				var p = method.GetParameters();
				if (p.Count() != 1 || p.Single().ParameterType != typeof(ReadOnlyMemory<byte>))
				{
					var types = string.Join(", ", p.Select(o => o.ParameterType.FullName));
					var msg = string.Format("{0} cannot be applied to method {1} with parameters ({2}).", typeof(AutowiredFactoryAttribute).FullName, method.FullName(), types);
				}
			}

			foreach (var group in methods.GroupBy(o => o.GetCustomAttribute<AutowiredFactoryAttribute>()))
			{
				if (group.Count() > 1)
				{
					var names = string.Join(", ", methods.Select(o => o.FullName()));
					var msg = string.Format("Found duplicate {0} type {1} version {2}: {3}", typeof(AutowiredFactoryAttribute).FullName, group.Key.Type, group.Key.Version, names);
				}
			}
		}

		private static IDictionary<Type, Action<Server, MessageEventArgs, object>> FindHandlers(IEnumerable<object> observers)
		{
			var handlers = new Dictionary<Type, List<Action<Server, MessageEventArgs, object>>>();
			foreach (var observer in observers)
			{
				var methods = observer.GetType().GetMethods().Where(o => o.GetCustomAttribute<AutowiredHandlerAttribute>() != null);
				ValidateHandlerMethods(methods);
				foreach (var method in methods)
				{
					var type = method.GetParameters().ToArray()[2].ParameterType;
					if (!handlers.ContainsKey(type))
					{
						handlers.Add(type, new List<Action<Server, MessageEventArgs, object>>());
					}
					handlers[type].Add((server, args, payload) => method.Invoke(observer, new object[] { server, args, payload }));
				}
			}
			return handlers.ToDictionary(kvp => kvp.Key, kvp => (Action<Server, MessageEventArgs, object>)((server, args, payload) =>
			{
				foreach (var action in kvp.Value)
				{
					action.Invoke(server, args, payload);
				}
			}));
		}

		private static void ValidateHandlerMethods(IEnumerable<MethodInfo> methods)
		{
			foreach(var method in methods)
			{
				if (method.IsStatic)
				{
					var msg = string.Format("{0} cannot be applied to static method {1}.", typeof(AutowiredHandlerAttribute).FullName, method.FullName());
					throw new Exception(msg);
				}
				if (!method.IsPublic)
				{
					var msg = string.Format("{0} cannot be applied to non-public method {1}.", typeof(AutowiredHandlerAttribute).FullName, method.FullName());
					throw new Exception(msg);
				}
				var p = method.GetParameters().ToArray();
				if(p.Length != 3 || p[0].ParameterType != typeof(Server) || p[1].ParameterType != typeof(MessageEventArgs))
				{
					var types = string.Join(", ", p.Select(o => o.ParameterType.FullName));
					var msg = string.Format("{0} cannot be applied to method {1} with parameters ({2}).", typeof(AutowiredHandlerAttribute).FullName, method.FullName(), types);
					throw new Exception(msg);
				}
			}
		}
	}
}
