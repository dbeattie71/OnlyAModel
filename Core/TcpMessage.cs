using System;
using System.Buffers.Binary;

namespace Core
{
	internal class TcpMessage : IMessage
	{
#pragma warning disable IDE0032
		private readonly ReadOnlyMemory<byte> _data;
#pragma warning restore IDE0032
		public ReadOnlyMemory<byte> Data => _data;

		public byte Type => _data.Span[2];

		public ushort Sequence => 0;

		public ReadOnlyMemory<byte> Payload => _data[3..];

		[Obsolete]
		internal TcpMessage(byte type, IMarshallable payload)
		{
			// [0..2] length
			// [2]    type
			// [3..]  payload

			Memory<byte> data = new byte[payload.Length + 3];
			var span = data.Span;
			BinaryPrimitives.WriteUInt16BigEndian(span[0..2], (ushort)payload.Length);
			span[2] = type;
			payload.Marshal(span[3..]);
			_data = data;
		}

		internal TcpMessage(ISendable sendable, int protocolVersion)
		{
			var len = sendable.Length(protocolVersion);
			Memory<byte> data = new byte[len + 3];
			var span = data.Span;
			BinaryPrimitives.WriteUInt16BigEndian(span[0..2], len);
			span[2] = sendable.Type;
			sendable.Marshal(span[3..], protocolVersion);
			_data = data;
		}
	}
}
