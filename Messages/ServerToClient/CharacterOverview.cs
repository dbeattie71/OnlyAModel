using Models.Character;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Messages.ServerToClient
{
	public class CharacterOverview : IPayload
	{
		public byte MessageType => Messages.MessageType.ServerToClient.CharacterOverview;

		private IEnumerable<Character> _characters;

		public CharacterOverview(IEnumerable<Character> characters)
		{
			_characters = characters;
		}

		public int Length => _characters.Select(o => GetLength(o)).Sum() + 8;

		public void Marshal(Span<byte> span)
		{
			var writer = new SpanWriter(span);
			writer.Skip(8);
			foreach (var c in _characters)
			{
				if (c == null)
				{
					writer.WriteByte(0);
				}
				else
				{
					writer.WriteByte(c.Level);
					writer.WriteDaocString(c.Name);
					writer.WriteUInt32LittleEndian(0x18);
					writer.Write(c.Customization);
					writer.Skip(13);
					writer.WriteDaocString(c.LocationDescription);
					writer.WriteDaocString(c.Classification.Class.DisplayName());
					writer.WriteDaocString(c.Classification.Race.ToString());
					writer.WriteUInt16LittleEndian(c.Model);
					writer.WriteUInt16LittleEndian(c.Region);
					writer.Write(c.VisibleEquipment);
					writer.Write(c.Stats);
					writer.Write(c.Classification);
					// active weapon slots - see DoL's PacketLib1125.cs line 340
					writer.WriteByte(0xFF);
					writer.WriteByte(0xFF);
					writer.WriteByte(0x01); // something about region
					writer.WriteByte(c.Stats.Constitution);
				}
			}
			// TODO real stuff - see DoL's PacketLib1125.SendCharacterOverview
		}

		private static int GetLength(Character c)
		{
			if(c == null)
			{
				return 1;
			}
			// length + 5 bytes for each DAoC string
			return
				1 + // level
				c.Name.Length + 5 +
				4 + // skipped
				System.Runtime.InteropServices.Marshal.SizeOf(typeof(Customization)) +
				13 + // skipped
				c.LocationDescription.Length + 5 +
				c.Classification.Class.DisplayName().Length + 5 +
				c.Classification.Race.ToString().Length + 5 +
				2 + // model
				2 + // region
				System.Runtime.InteropServices.Marshal.SizeOf(typeof(VisibleEquipment)) +
				System.Runtime.InteropServices.Marshal.SizeOf(typeof(Stats)) +
				System.Runtime.InteropServices.Marshal.SizeOf(typeof(Classification)) +
				4; // other junk
		}
	}
}
