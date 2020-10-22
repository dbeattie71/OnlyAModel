using Core;
using System;

namespace Messages
{
	/// <summary>
	/// Applicable to public static methods that receive
	/// <c>(ReadOnlyMemory&lt;byte&gt;)</c> and return any type other than
	/// void. Each combination of message type and protocol version must be
	/// unique across factory methods in the same autowired event handler.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class AutowiredFactoryAttribute : Attribute
	{
		public MessageType.ClientToServer Type { get; }
		public int Version { get; }

		public AutowiredFactoryAttribute(MessageType.ClientToServer type, int version = 1125)
		{
			Type = type;
			Version = version;
		}

		public override bool Equals(object obj)
		{
			return obj is AutowiredFactoryAttribute attribute &&
				   base.Equals(obj) &&
				   Type == attribute.Type &&
				   Version == attribute.Version;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(base.GetHashCode(), Type, Version);
		}
	}
}
