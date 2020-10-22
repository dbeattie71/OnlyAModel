using Core;
using System;
using System.Buffers.Binary;

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

			// [0..2]  length of [5..^2]
			// [2..4]  unknown
			// [5..^2] null-terminated version string
			// [^2..] client build version

			BinaryPrimitives.WriteUInt16LittleEndian(span[0..2], (ushort)(_version.Length + 1));
			for(var i = 0; i < _version.Length; ++i)
			{
				span[i + 4] = (byte)_version[i]; 
			}
			span[^3] = 0x00; // string terminator
			BinaryPrimitives.WriteUInt16LittleEndian(span[^2..], _build);

			// if you're lazy, the client also accepts all zeros
			//new byte[6].CopyTo(span);
		}
	}
}
