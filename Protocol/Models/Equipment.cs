namespace Protocol.Models
{
	/// <summary>
	/// Equipment visible on the character model.
	/// </summary>
	public readonly struct Equipment
	{
		public ushort HelmetModel { get; }
		public ushort GlovesModel { get; }
		public ushort BootsModel { get; }
		public ushort MainhandColor { get; }
		public ushort TorsoModel { get; }
		public ushort CloakModel { get; }
		public ushort LegsModel { get; }
		public ushort ArmsModel { get; }
		public ushort HelmetColor { get; }
		public ushort GlovesColor { get; }
		public ushort BootsColor { get; }
		public ushort OffhandColor { get; }
		public ushort TorsoColor { get; }
		public ushort CloakColor { get; }
		public ushort LegsColor { get; }
		public ushort ArmsColor { get; }
		public ushort MainhandModel { get; }
		public ushort OffhandModel { get; }
		public ushort TwoHandModel { get; }
		public ushort RangedModel { get; }

		// TODO can two hand and ranged not be dyed?

		// TODO use builder pattern?

		public Equipment(ushort helmetModel, ushort glovesModel, ushort bootsModel, ushort mainhandColor,
			ushort torsoModel, ushort cloakModel, ushort legsModel, ushort armsModel,
			ushort helmetColor, ushort glovesColor, ushort bootsColor, ushort offhandColor,
			ushort torsoColor, ushort cloakColor, ushort legsColor, ushort armsColor,
			ushort mainhandModel, ushort offhandModel, ushort twoHandModel, ushort rangedModel)
		{
			HelmetModel = helmetModel;
			GlovesModel = glovesModel;
			BootsModel = bootsModel;
			MainhandColor = mainhandColor;
			TorsoModel = torsoModel;
			CloakModel = cloakModel;
			LegsModel = legsModel;
			ArmsModel = armsModel;
			HelmetColor = helmetColor;
			GlovesColor = glovesColor;
			BootsColor = bootsColor;
			OffhandColor = offhandColor;
			TorsoColor = torsoColor;
			CloakColor = cloakColor;
			LegsColor = legsColor;
			ArmsColor = armsColor;
			MainhandModel = mainhandModel;
			OffhandModel = offhandModel;
			TwoHandModel = twoHandModel;
			RangedModel = rangedModel;
		}
	}
}
