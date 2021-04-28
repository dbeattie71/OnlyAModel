namespace Messages.Models
{
	public struct Status
	{
		// order determined by CharacterStatus payload
		public ushort MaxMana { get; set; }
		public ushort MaxEndurance { get; set; }
		public ushort MaxConcentration { get; set; }
		public ushort MaxHealth { get; set; }
		public ushort Health { get; set; }
		public ushort Endurance { get; set; }
		public ushort Mana { get; set; }
		public ushort Concentration { get; set; }
	}
}
