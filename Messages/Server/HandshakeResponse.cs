using System;

namespace Messages.Server
{
	public class HandshakeResponse : IServerMessage
	{
		private readonly string _version;
		private readonly ushort _build;

		public HandshakeResponse(string version, ushort build)
		{
			_version = version;
			_build = build;
		}

		public byte Type => MessageType.Server.HandshakeResponse;

		public int Length => _version.Length + 7;

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
