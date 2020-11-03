using Core;
using System;

namespace Messages.ServerToClient
{
	public class GameOpenResponse : IPayload
	{
		// DoL hardcodes 0x00 in its response
		// guessing it might be more correct to echo the value from the request
		// guessing this might be telling the client whether or not to use UDP
		// TODO test what the client does if we send 0x01

		private readonly byte _unknown;

		public GameOpenResponse(byte unknown)
		{
			_unknown = unknown;
		}

		public MessageType.ServerToClient Type => MessageType.ServerToClient.GameOpenResponse;

		public int Length => 1;

		public void Marshal(Span<byte> span)
		{
			span[0] = _unknown;
			var writer = new SpanWriter(span);
			writer.WriteByte(_unknown);
		}
	}
}
