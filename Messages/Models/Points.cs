namespace Messages.Models
{
	public struct Points
	{
		public uint RealmPoints { get; set; }
		/// <summary>
		/// 0-999, 100 = 1 bub
		/// </summary>
		public ushort ExperienceBubbles { get; set; }
		public ushort SpecPoints { get; set; }
		public ushort BountyPoints { get; set; }
		public ushort RealmSpecPoints { get; set; }
		/// <summary>
		/// 0-999, 100 = 1 bub
		/// </summary>
		public ushort ChampionBubbles { get; set; }
		public ulong Experience { get; set; }
		public ulong NextLevel { get; set; }
		public ulong ChampionExperience { get; set; }
		public ulong NextChampionLevel { get; set; }
	}
}
