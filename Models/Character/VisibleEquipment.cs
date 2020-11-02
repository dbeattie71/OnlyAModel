namespace Models.Character
{
	public readonly struct VisibleEquipment
	{
		ushort HelmetModel { get; }
		ushort GlovesModel { get; }
		ushort BootsModel { get; }
		ushort MainhandColor { get; }
		ushort TorsoModel { get; }
		ushort CloakModel { get; }
		ushort LegsModel { get; }
		ushort ArmsModel { get; }
		ushort HelmetColor { get; }
		ushort GlovesColor { get; }
		ushort BootsColor { get; }
		ushort OffhandColor { get; }
		ushort TorsoColor { get; }
		ushort CloakColor { get; }
		ushort LegsColor { get; }
		ushort ArmsColor { get; }
		ushort MainhandModel { get; }
		ushort OffhandModel { get; }
		ushort TwoHandModel { get; }
		ushort RangedModel { get; }

		// TODO can two hand and ranged not be dyed?

		public VisibleEquipment(ushort helmetModel, ushort glovesModel, ushort bootsModel, ushort mainhandColor,
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
