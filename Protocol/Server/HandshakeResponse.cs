using Core;
using System;

namespace Protocol.Server
{
	public class HandshakeResponse : ISendable
	{
		private readonly string _version;
		private readonly ushort _build;

		public HandshakeResponse(string version, ushort build)
		{
			_version = version;
			_build = build;
		}

		public byte Type => MessageType.Server.HandshakeResponse;

		public ushort Length(int protocolVersion)
		{
			return (ushort)(_version.Length + 7);
		}

		public void Marshal(Span<byte> span, int protocolVersion)
		{
			var writer = new SpanWriter(span);
			writer.WriteDaocString(_version);
			writer.WriteUInt16LittleEndian(_build);
		}
	}
}
