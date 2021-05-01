using System;

namespace Core
{
	[Obsolete]
	public interface IMarshallable
	{
		int Length { get; }
		void Marshal(Span<byte> span);
	}
}
