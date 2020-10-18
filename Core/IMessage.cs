using System;

namespace Core
{
	public interface IMessage
	{
		ReadOnlyMemory<byte> Data { get; }
		byte Type { get; }
		ushort Sequence { get; }
		ReadOnlyMemory<byte> Payload { get; }
	}
}
