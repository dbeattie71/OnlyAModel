using Messages;
using Models.Character;
using Models.World;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MultiPlayer.Services
{
	public class CharacterService
	{
		private readonly ConcurrentDictionary<string, List<Character>> _characters = new ConcurrentDictionary<string, List<Character>>();

		private List<Character> Chars(string user) =>
			_characters.GetOrAdd(user, s => new Character[30].ToList());

		internal void Create(string user, Character character, int slot)
		{
			Chars(user)[slot] = character;
			InitCharacter(character);
		}

		internal Character GetBySlot(string user, int slot) => Chars(user)[slot];

		internal IEnumerable<Character> GetByRealm(string user, Realm realm) => Chars(user).Skip(((int)realm - 1) * 10).Take(10);

		internal Character GetByName(string user, string name) => Chars(user).SingleOrDefault(o => name.EqualsIgnoreCase(o?.Name));

		internal bool NameExists(string name) => _characters.Values.SelectMany(o => o).Any(o => name.EqualsIgnoreCase(o?.Name));

		private void InitCharacter(Character c)
		{
			// TODO real stats and starting locations
			c.Status = new Status()
			{
				Health = 100,
				MaxHealth = 100,
				Mana = 100,
				MaxMana = 100,
				Endurance = 100,
				MaxEndurance = 100,
				Concentration = 100,
				MaxConcentration = 100
			};
			// cotswold
			c.Region = 1;
			c.Coordinates = new Coordinates(560467, 511652, 2344, 3398);
		}
	}
}
