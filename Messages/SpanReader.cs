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
		/// Reads DAoC's weird hybrid of UCSD and C strings. These consist
		/// of a <c>uint</c> length followed by a C string. The length
		/// includes the null terminator, so the actual string is one byte
		/// shorter than specified.
		/// </summary>
		public string ReadDaocString()
		{
			var len = ReadUInt32LittleEndian();
			return ReadCString(len);
		}

		/// <summary>
		/// Reads a <c>uint</c> but returns an <c>int</c> for convenience.
		/// </summary>
		private int ReadUInt32LittleEndian()
		{
			var value = BinaryPrimitives.ReadUInt32LittleEndian(_span.Slice(_position, sizeof(uint)));
			_position += sizeof(uint);
			return (int)value;
		}

		/// <summary>
		/// Length includes the null terminator, which is not returned.
		/// </summary>
		private string ReadCString(int length)
		{
			var value = Encoding.ASCII.GetString(_span.Slice(_position, length - 1));
			_position += length;
			return value;
		}
	}
}
