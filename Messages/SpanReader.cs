using System;
using System.Buffers.Binary;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
			return len == 0 ? null : ReadCString((int)len);
		}

		public ushort ReadUInt16LittleEndian()
		{
			var slice = _span.Slice(_position, sizeof(ushort));
			var value = BinaryPrimitives.ReadUInt16LittleEndian(slice);
			_position += sizeof(ushort);
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

		/// <summary>
		/// Reads a null-terminated string of known length.
		/// </summary>
		private string ReadCString(int length)
		{
			var value = Encoding.ASCII.GetString(_span.Slice(_position, length - 1));
			_position += length;
			return value;
		}

		/// <summary>
		/// Reads a null-terminated string from a fixed-width field.
		/// </summary>
		public string ReadFixedString(int width)
		{
			// allows the string to use the entire field with no terminator.
			// not sure if that's what we want.
			int len;
			for(len = 0; len < width; ++len)
			{
				if (_span[_position + len] == 0)
				{
					break;
				}
			}
			var slice = _span.Slice(_position, len);
			var value = Encoding.ASCII.GetString(slice.ToArray());
			_position += width;
			return value;
		}

		public byte ReadByte()
		{
			return _span[_position++];			
		}

		public T Read<T>() where T : unmanaged
		{
			var len = Marshal.SizeOf(typeof(T));
			var value = MemoryMarshal.Read<T>(_span.Slice(_position, len));
			_position += len;
			return value;
		}
	}
}
