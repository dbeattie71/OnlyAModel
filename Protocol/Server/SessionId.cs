using Core;
using System;
using System.Buffers.Binary;

namespace Protocol.Server
{
	public class SessionId : ISendable
	{
		private readonly ushort _id;

		public SessionId(ushort id)
		{
			_id = id;
		}

		public byte Type => MessageType.Server.SessionId;

		public int Length(int protocolVersion) => 2;

		public void Marshal(Span<byte> span, int protocolVersion)
		{
			BinaryPrimitives.WriteUInt16LittleEndian(span, _id);
		}
	}
}
