using Core;
using System;
using System.Buffers.Binary;

namespace Messages.ServerToClient
{
	public class SessionId : IPayload
	{
		private readonly ushort _id;

		public SessionId(ushort id)
		{
			_id = id;
		}

		public MessageType.ServerToClient Type => MessageType.ServerToClient.SessionId;

		public int Length => 2;

		public void Marshal(Span<byte> span)
		{
			BinaryPrimitives.WriteUInt16LittleEndian(span, _id);
		}
	}
}
