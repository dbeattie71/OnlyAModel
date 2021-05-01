namespace Core.Data
{
	/// <summary>
	/// Available game content.
	/// </summary>
	public struct Content
	{
		// expansions
		private const int CLASSIC = 1;
		private const int SI = 2;
		private const int TOA = 3;
		private const int CAT = 4;
		private const int DR = 5;
		private const int LAB = 6;
		// addons
		private const int NEW_NEW_FRONTIERS = 0x20;
		private const int FOUNDATIONS = 0x40;
		private const int NEW_FRONTIERS = 0x80;

#pragma warning disable 0649 // private readonly fields can be written by MemoryMarshal
		private readonly byte _content;
#pragma warning restore 0649
		public bool Classic { get => (_content & 0x0F) >= CLASSIC; }
		public bool ShroudedIsles { get => (_content & 0x0F) >= SI; }
		public bool TrialsOfAtlantis { get => (_content & 0x0F) >= TOA; }
		public bool Catacombs { get => (_content & 0x0F) >= CAT; }
		public bool DarknessRising { get => (_content & 0x0F) >= DR; }
		public bool LabyrinthOfTheMinotaur { get => (_content & 0x0F) >= LAB; }
		public bool NewNewFrontiers { get { return (_content & NEW_NEW_FRONTIERS) != 0; } }
		public bool Foundations { get { return (_content & FOUNDATIONS) != 0; } }
		public bool NewFrontiers { get { return (_content & NEW_FRONTIERS) != 0; } }
	}
}
