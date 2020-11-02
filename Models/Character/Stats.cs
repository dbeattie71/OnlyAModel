namespace Models.Character
{
	public readonly struct Stats
	{
		public byte Strength { get; }
		public byte Dexterity { get; }
		public byte Constitution { get; }
		public byte Quickness { get; }
		public byte Intelligence { get; }
		public byte Piety { get; }
		public byte Empathy { get; }
		public byte Charisma { get; }

		public Stats(byte strength, byte dexterity, byte constitution, byte quickness,
			byte intelligence, byte piety, byte empathy, byte charisma)
		{
			Strength = strength;
			Dexterity = dexterity;
			Constitution = constitution;
			Quickness = quickness;
			Intelligence = intelligence;
			Piety = piety;
			Empathy = empathy;
			Charisma = charisma;
		}
	}
}
