using System;
using System.Buffers.Binary;

namespace Messages.Server
{
	public class SessionId : IServerMessage
	{
		private readonly ushort _id;

		public SessionId(ushort id)
		{
			_id = id;
		}

		public byte Type => MessageType.Server.SessionId;

		public int Length => 2;

		public void Marshal(Span<byte> span)
		{
			BinaryPrimitives.WriteUInt16LittleEndian(span, _id);
		}
	}
}
