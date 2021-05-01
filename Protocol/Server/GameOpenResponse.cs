using Core;
using System;

namespace Protocol.Server
{
	public class GameOpenResponse : ISendable
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

		public byte Type => MessageType.Server.GameOpenResponse;

		public int Length(int protocolVersion) => 1;

		public void Marshal(Span<byte> span, int protocolVersion)
		{
			span[0] = _unknown;
		}
	}
}
