using System;
using System.Buffers.Binary;
using System.IO;

namespace Core
{
	internal class Message : IMessage
	{
		public ReadOnlyMemory<byte> Data { get; }
		public byte Type => Data.Span[9];
		public ushort Sequence => BinaryPrimitives.ReadUInt16BigEndian(Data.Slice(2, 2).Span);
		public ReadOnlyMemory<byte> Payload => Data[10..^2];
		internal Message(ReadOnlyMemory<byte> data)
		{
			Data = data;
			if (ComputeChecksum(Data[..^2].Span) != BinaryPrimitives.ReadUInt16BigEndian(Data[^2..].Span))
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
