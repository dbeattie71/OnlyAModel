using Core;
using System;

namespace Messages.ClientToServer
{
	public class GameOpenRequest
	{
		// TODO don't know what this is for yet. represent as a bool?
		public byte ConfirmUdp { get; }

		private GameOpenRequest(byte confirmUdp)
		{
			ConfirmUdp = confirmUdp;
		}

		[AutowiredFactory(MessageType.ClientToServer.GameOpenRequest)]
		public static GameOpenRequest Unmarshall(ReadOnlyMemory<byte> payload)
		{
			var reader = new SpanReader(payload.Span);
			byte confirmUdp = reader.ReadByte();
			return new GameOpenRequest(confirmUdp);
		}
	}
}
