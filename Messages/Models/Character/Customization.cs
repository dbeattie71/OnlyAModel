namespace Messages.Models.Character
{
	public readonly struct Customization
	{
		public byte CustomMode { get; }
		public byte EyeSize { get; }
		public byte LipSize { get; }
		public byte EyeColor { get; }
		public byte HairColor { get; }
		public byte FaceType { get; }
		public byte HairStyle { get; }
		public byte BootsGloves { get; } // unsure how this works - see DoL character overview
		public byte TorsoHood { get; } // ditto
		public byte CustomizationStep { get; }
		public byte Mood { get; }

		public Customization(byte customMode, byte eyeSize, byte lipSize, byte eyeColor, byte hairColor, byte faceType,
			byte hairStyle, byte bootsGloves, byte torsoHood, byte customizationStep, byte mood)
		{
			CustomMode = customMode;
			EyeSize = eyeSize;
			LipSize = lipSize;
			EyeColor = eyeColor;
			HairColor = hairColor;
			FaceType = faceType;
			HairStyle = hairStyle;
			BootsGloves = bootsGloves;
			TorsoHood = torsoHood;
			CustomizationStep = customizationStep;
			Mood = mood;
		}
	}
}
