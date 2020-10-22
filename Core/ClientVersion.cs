using System;
using System.Runtime.InteropServices;

namespace Core.Data
{
	/// <summary>
	/// Client version and capabilities.
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ClientVersion
	{
#pragma warning disable 0649 // private readonly fields can be written by MemoryMarshal
		public ClientContent Content { get; }
		public byte MajorVersion { get; }
		private readonly byte _minorVersion0;
		private readonly byte _minorVersion1;
		private readonly byte _revision;
		public ushort Build { get; } // build number seems more plausible if it's little endian
#pragma warning restore 0649
		public int MinorVersion { get => _minorVersion0 * 100 + _minorVersion1; }
		public char Revision { get => (char)_revision; }
		public int ProtocolVersion { get => MajorVersion * 100 * (int)Math.Pow(10, _minorVersion0) + MinorVersion; }
		public override string ToString() => MajorVersion + "." + MinorVersion + Revision;
	}
}
