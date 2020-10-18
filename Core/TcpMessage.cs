using System;
using System.Buffers.Binary;

namespace Core
{
	internal class TcpMessage : IMessage
	{
		public ReadOnlyMemory<byte> Data { get; }

		public byte Type => Data.Span[2];

		public ushort Sequence => 0;

		public ReadOnlyMemory<byte> Payload => Data[3..];

		internal TcpMessage(byte type, ReadOnlyMemory<byte> payload)
		{
			Memory<byte> data = new byte[payload.Length + 3];
			BinaryPrimitives.WriteUInt16BigEndian(data[0..2].Span, (ushort)payload.Length);
			data.Span[2] = type;
			payload.CopyTo(data[3..]);
			Data = data;
		}
	}
}
