using Core.Event;
using Protocol.Autowire;
using Protocol.Models;

namespace Protocol.Client
{
	public class CharacterCreateRequest
	{
		public byte Slot { get; }
		public Character Character { get; }

		private CharacterCreateRequest(byte slot, Character character)
		{
			Slot = slot;
			Character = character;
		}

		[Unmarshaller(MessageType.Client.CharacterCreateRequest)]
		public static CharacterCreateRequest Unmarshal(MessageEventArgs args)
		{
			var reader = new SpanReader(args.Message.Payload.Span);
			var slot = reader.ReadByte();
			var c = new Character();
			c.Level = 1;
			c.Name = reader.ReadDaocString();
			reader.Skip(4); // 0x18 0x00 0x00 0x00 - DoL notes say this is an array length
			c.Customization = reader.Read<Customizations>();
			reader.Skip(9);
			var operation = reader.ReadByte(); // 1
			var custType = reader.ReadByte(); // 0
			reader.Skip(2); // ???
			c.LocationDescription = reader.ReadDaocString(); // empty
			reader.ReadDaocString(); // class description; redundant and empty
			reader.ReadDaocString(); // race description; redundant and empty
			reader.Skip(1); // ???
			c.Classification = reader.Read<Classification>();
			c.Model = reader.ReadUInt16LittleEndian(); // 12352
			c.Region = reader.ReadUInt16LittleEndian(); // byte? 27
			reader.Skip(4);
			c.Stats = reader.Read<Stats>();
			c.Equipment = reader.Read<Equipment>(); // empty
			; // 4 bytes left
			return new CharacterCreateRequest(slot, c);
		}

	}
}
