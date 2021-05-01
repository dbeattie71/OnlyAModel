using Core;

namespace Messages
{
	public interface IServerMessage : IMarshallable
	{
		public byte Type { get; }
	}
}
