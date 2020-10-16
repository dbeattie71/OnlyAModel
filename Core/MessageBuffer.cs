using System;
using System.Buffers.Binary;

namespace Core
{
	internal class MessageBuffer
	{
		private const int ENVELOPE_BYTES = 12; // 10 byte header, 2 byte checksum
		private readonly Memory<byte> _buffer;
		private int _offset;
		private int _count;

		internal MessageBuffer(int bytes)
		{
			_buffer = new byte[bytes];
		}

		internal void Append(Memory<byte> data)
		{
			if (_offset + _count + data.Length <= _buffer.Length)
			{
				data.CopyTo(_buffer.Slice(_offset + _count));
				_count += data.Length;
			}
			else if (_count + data.Length <= _buffer.Length)
			{
				_buffer.Slice(_offset, _count).CopyTo(_buffer);
				_offset = 0;
				data.CopyTo(_buffer.Slice(_count));
				_count += data.Length;
			}
			else
			{
				throw new InvalidOperationException(string.Format("capacity={0}, count={1}, append={2}", _buffer.Length, _count, data.Length));
			}
		}

		internal bool TryGetMessage(out Message message)
		{
			if (_count > ENVELOPE_BYTES)
			{
				var size = BinaryPrimitives.ReadUInt16BigEndian(_buffer.Slice(_offset, 2).Span) + ENVELOPE_BYTES;
				if (_count >= size)
				{
					message = new Message(_buffer.Slice(_offset, size));
					if (_count == size)
					{
						_offset = 0;
						_count = 0;
					}
					else
					{
						_offset += size;
						_count -= size;
					}
					return true;
				}
			}
			message = default;
			return false;
		}
	}
}
