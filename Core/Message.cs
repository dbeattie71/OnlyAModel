using System;
using System.Buffers.Binary;
using System.IO;

namespace Core
{
	public struct Message
	{
		public byte Type { get; }
		public ushort Sequence { get; }
		public ReadOnlyMemory<byte> Payload { get; }

		internal Message(ReadOnlyMemory<byte> data)
		{
			if (ComputeChecksum(data) != BinaryPrimitives.ReadUInt16BigEndian(data[^2..].Span))
			{
				throw new InvalidDataException();
			}
			Type = data.Span[9];
			Sequence = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(2, 2).Span);
			Payload = data[10..^2].ToArray();
		}

		private static ushort ComputeChecksum(ReadOnlyMemory<byte> data)
		{
			var s = data.Span;
			byte b0 = 0x7E;
			byte b1 = 0x7E;
			for (int i = 0; i < s.Length - 2; ++i)
			{
				b0 += s[i];
				b1 += b0;
			}
			return (ushort)(b1 - ((b0 + b1) << 8));
		}
	}
}
