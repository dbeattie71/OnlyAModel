using System;

namespace Core
{
	/// <summary>
	/// The marshalled form of a client or server message.
	/// </summary>
	public interface IMessage
	{
		ReadOnlyMemory<byte> Data { get; }
		byte Type { get; }
		ushort Sequence { get; }
		ReadOnlyMemory<byte> Payload { get; }
	}
}
