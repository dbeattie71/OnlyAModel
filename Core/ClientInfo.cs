using System.Runtime.InteropServices;

namespace Core.Data
{
	/// <summary>
	/// Client version and capabilities.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ClientInfo
	{
#pragma warning disable 0649 // private readonly fields can be written by MemoryMarshal
		public Content Content { get; }
		public byte MajorVersion { get; }
		private readonly byte _minorVersion0;
		private readonly byte _minorVersion1;
		private readonly byte _revision;
		public ushort Build { get; } // TODO confirm endianness of build number by comparing value shown on loading screen in 1.126+
#pragma warning restore 0649
		public int MinorVersion { get => _minorVersion0 * 100 + _minorVersion1; }
		public char Revision { get => (char)_revision; }
		public string Version { get => MajorVersion + "." + MinorVersion + Revision; }
	}
}
