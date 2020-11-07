using System;

namespace Core
{
	public interface IMarshallable
	{
		int Length { get; }
		void Marshal(Span<byte> span);
	}
}
