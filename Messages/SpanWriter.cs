using System;
using System.Buffers.Binary;
using System.Text;

namespace Messages
{
	public ref struct SpanWriter
	{
		private Span<byte> _span;
		private int _position;

		public SpanWriter(Span<byte> span)
		{
			_span = span;
			_position = 0;
		}

		public void Skip(int bytes)
		{
			_position += bytes;
		}

		public void WriteDaocString(string value)
		{
			// length includes the null terminator
			WriteUInt32LittleEndian((uint)value.Length + 1);
			WriteString(value);
			WriteByte(0);
		}

		public void WriteUInt16LittleEndian(ushort value)
		{
			var slice = _span.Slice(_position, sizeof(ushort));
			BinaryPrimitives.WriteUInt16LittleEndian(slice, value);
			_position += sizeof(ushort);
		}

		public void WriteUInt16BigEndian(ushort value)
		{
			var slice = _span.Slice(_position, sizeof(ushort));
			BinaryPrimitives.WriteUInt16BigEndian(slice, value);
			_position += sizeof(ushort);
		}

		private void WriteUInt32LittleEndian(uint value)
		{
			var slice = _span.Slice(_position, sizeof(uint));
			BinaryPrimitives.WriteUInt32LittleEndian(slice, value);
			_position += sizeof(uint);
		}

		public void WriteUInt32BigEndian(uint value)
		{
			var slice = _span.Slice(_position, sizeof(uint));
			BinaryPrimitives.WriteUInt32BigEndian(slice, value);
			_position += sizeof(uint);
		}

		private void WriteString(string value)
		{
			var slice = _span.Slice(_position, value.Length);
			Encoding.ASCII.GetBytes(value).CopyTo(slice);
			_position += value.Length;
		}

		public void WriteByte(byte value)
		{
			_span[_position] = value;
			++_position;
		}
	}
}
