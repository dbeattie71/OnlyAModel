namespace Models.Character
{
	public class Character
	{
		public byte Level { get; set; }
		public string Name { get; set; }
		public Classification Classification { get; set; }
		public Customization Customization { get; set; }
		public VisibleEquipment VisibleEquipment { get; set; }
		public Status Status { get; set; }
		public Stats Stats { get; set; }
		public Points Points { get; set; }
		public byte ActiveMainhand { get; set; } // represent differently?
		public byte ActiveOffhand { get; set; } // represent differently?
		public ushort Model { get; set; } // uncertain
		public ushort Region { get; set; }
		public string LocationDescription { get; set; }
		public bool Sitting { get; set; }
	}
}
