using System;

namespace Protocol.Autowire
{
	[AttributeUsage(AttributeTargets.Method)]
	internal class UnmarshallerAttribute : Attribute
	{
		internal byte MessageType { get; }

		internal UnmarshallerAttribute(byte messageType)
		{
			MessageType = messageType;
		}
	}
}
