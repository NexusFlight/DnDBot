using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDBot
{
	public class Character
	{
		public string CharName { get; set; }
		public int CharLevel { get; set; }
		public string CharRace { get; set; }
		public string CharClass { get; set; }
		public int MP { get; set; }
		public int Gold { get; set; }
		public bool IsAlive { get; set; } = true;
		public MagicItem[] MagicItems { get; set; }

		public Character() { }
		public Character(string charName, int charLevel, string charRace, string charClass, int mP, int gold, bool isAlive)		
		{
			CharName = charName;
			CharLevel = charLevel;
			CharRace = charRace;
			CharClass = charClass;
			MP = mP;
			Gold = gold;
			IsAlive = isAlive;
		}

		public override string ToString()
		{
			return "Character Name " + CharName + "\n"
				+ "Character Level " + CharLevel + "\n"
				+ "Character Race " + CharRace + "\n"
				+ "Character Class " + CharClass;
		}
	}
}
