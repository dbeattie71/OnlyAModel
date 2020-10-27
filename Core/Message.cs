using System;
using System.Buffers.Binary;
using System.IO;

namespace Core
{
	internal class Message : IMessage
	{
#pragma warning disable IDE0032
		private readonly ReadOnlyMemory<byte> _data;
#pragma warning restore IDE0032
		public ReadOnlyMemory<byte> Data => _data;
		public byte Type => _data.Span[9];
		public ushort Sequence => BinaryPrimitives.ReadUInt16BigEndian(_data.Slice(2, 2).Span);
		public ReadOnlyMemory<byte> Payload => _data[10..^2];
		internal Message(ReadOnlyMemory<byte> data)
		{
			_data = data;
			if (ComputeChecksum(_data[..^2].Span) != BinaryPrimitives.ReadUInt16BigEndian(_data[^2..].Span))
			{
				throw new InvalidDataException();
			}
		}

		private static ushort ComputeChecksum(ReadOnlySpan<byte> span)
		{
			byte b0 = 0x7E;
			byte b1 = 0x7E;
			for (int i = 0; i < span.Length; ++i)
			{
				b0 += span[i];
				b1 += b0;
			}
			return (ushort)(b1 - ((b0 + b1) << 8));
		}
	}
}
