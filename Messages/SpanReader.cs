using System;
using System.Buffers.Binary;
using System.Text;

namespace Messages
{
	public ref struct SpanReader
	{
		private ReadOnlySpan<byte> _span;
		private int _position;

		public SpanReader(ReadOnlySpan<byte> span)
		{
			_span = span;
			_position = 0;
		}

		public int Skip(int bytes)
		{
			_position += bytes;
			return _position;
		}

		/// <summary>
		/// A DAoC string consists of a 32-bit length, ASCII string data, and
		/// a null terminator.
		/// </summary>
		public string ReadDaocString()
		{
			var len = ReadUInt32LittleEndian();
			return ReadCString((int)len);
		}

		/// <summary>
		/// Reads a string from a fixed-length field padded with nulls.
		/// </summary>
		public string ReadFixedString(int length)
		{
			var slice = _span.Slice(_position, length);
			var value = Encoding.ASCII.GetString(slice.ToArray()).TrimEnd('\0');
			_position += length;
			return value;
		}

		public uint ReadUInt32LittleEndian()
		{
			var slice = _span.Slice(_position, sizeof(uint));
			var value = BinaryPrimitives.ReadUInt32LittleEndian(slice);
			_position += sizeof(uint);
			return value;
		}

		public uint ReadUInt32BigEndian()
		{
			var slice = _span.Slice(_position, sizeof(uint));
			var value = BinaryPrimitives.ReadUInt32BigEndian(slice);
			_position += sizeof(uint);
			return value;
		}

		private string ReadCString(int length)
		{
			var value = Encoding.ASCII.GetString(_span.Slice(_position, length - 1));
			_position += length;
			return value;
		}
	}
}
