using System;
using System.Buffers.Binary;
using System.Runtime.InteropServices;
using System.Text;

namespace Protocol
{
	internal ref struct SpanWriter
	{
		private Span<byte> _span;
		private int _position;

		internal SpanWriter(Span<byte> span)
		{
			_span = span;
			_position = 0;
		}

		internal void Skip(int bytes)
		{
			_position += bytes;
		}

		internal void WriteShortString(string value)
		{
			WriteByte((byte)value.Length);
			WriteString(value);
		}

		internal void WriteDaocString(string value)
		{
			if (value == null)
			{
				WriteUInt32LittleEndian(0);
			}
			else
			{
				// length includes the null terminator
				WriteUInt32LittleEndian((uint)value.Length + 1);
				WriteString(value);
				WriteByte(0);
			}
		}

		internal void WriteFixedString(string value, int width)
		{
			// allows the string to use the entire field with no terminator.
			// not sure if that's what we want.
			if (value.Length > width)
			{
				throw new ArgumentException();
			}
			WriteString(value);
			Skip(width - value.Length);
		}

		internal void WriteUInt16LittleEndian(ushort value)
		{
			var slice = _span.Slice(_position, sizeof(ushort));
			BinaryPrimitives.WriteUInt16LittleEndian(slice, value);
			_position += sizeof(ushort);
		}

		internal void WriteUInt16BigEndian(ushort value)
		{
			var slice = _span.Slice(_position, sizeof(ushort));
			BinaryPrimitives.WriteUInt16BigEndian(slice, value);
			_position += sizeof(ushort);
		}

		internal void WriteUInt32LittleEndian(uint value)
		{
			var slice = _span.Slice(_position, sizeof(uint));
			BinaryPrimitives.WriteUInt32LittleEndian(slice, value);
			_position += sizeof(uint);
		}

		internal void WriteUInt32BigEndian(uint value)
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

		internal void WriteByte(byte value)
		{
			_span[_position] = value;
			++_position;
		}

		internal void Write<T>(T value) where T : unmanaged
		{
			var len = Marshal.SizeOf(typeof(T));
			MemoryMarshal.Write(_span.Slice(_position, len), ref value);
			_position += len;
		}

		internal void WriteFloat(float value)
		{
			// used for X/Y/Z coordinates, running speeds, and falling speeds
			// DoL notes suggest the protocol only started using floats in 1.124
			// the values being sent are mostly ints and shorts
			// maybe we should model those things as floats...?
			var slice = _span.Slice(_position, sizeof(float));
			BitConverter.TryWriteBytes(slice, value);
			_position += sizeof(float);
		}
	}
}
