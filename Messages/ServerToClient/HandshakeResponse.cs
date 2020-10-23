using Core;
using System;

namespace Messages.ServerToClient
{
	public class HandshakeResponse : IPayload
	{
		private readonly string _version;
		private readonly ushort _build;

		public HandshakeResponse(string version, ushort build)
		{
			_version = version;
			_build = build;
		}

		public MessageType.ServerToClient Type => MessageType.ServerToClient.HandshakeResponse;

		public ushort Length => (ushort)(_version.Length + 7);

		public void Marshal(Span<byte> span)
		{
			// since around 1110 or 1115, packet type 0x22 no longer contains an RSA public key.
			// the live servers send a client version and build number.
			var writer = new SpanWriter(span);
			writer.WriteDaocString(_version);
			writer.WriteUInt16LittleEndian(_build);
		}
	}
}
