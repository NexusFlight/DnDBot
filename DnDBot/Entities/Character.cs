using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DnDBot
{
	public class Character
	{
		[BsonIgnore]
		public static List<string> races = new List<string>(File.ReadAllLines(@"Races.txt"));
		[BsonIgnore]
		public static List<string> classes = new List<string>(File.ReadAllLines(@"Classes.txt"));

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

		public static string AddClass(string className)
		{
			classes.Add(className);
			StringBuilder sb = new StringBuilder();
			foreach (var item in classes)
			{
				sb.Append(item + "\n");
			}
			File.WriteAllText(@"Classes.txt", sb.ToString());
			return sb.ToString();
		}

		public static string AddRace(string race)
		{
			races.Add(race);
			StringBuilder sb = new StringBuilder();
			foreach (var item in races)
			{
				sb.Append(item + "\n");
			}
			File.WriteAllText(@"Races.txt", sb.ToString());
			return sb.ToString();
		}

		internal static string RemoveRace(string race)
		{
			races.RemoveAll(c => c.Equals(race, StringComparison.OrdinalIgnoreCase));
			StringBuilder sb = new StringBuilder();
			foreach (var item in races)
			{
				sb.Append(item + "\n");
			}
			File.WriteAllText(@"Races.txt", sb.ToString());
			return sb.ToString();
		}

		internal static string RemoveClass(string className)
		{
			classes.RemoveAll(c => c.Equals(className,StringComparison.OrdinalIgnoreCase));
			StringBuilder sb = new StringBuilder();
			foreach (var item in classes)
			{
				sb.Append(item + "\n");
			}
			File.WriteAllText(@"Classes.txt", sb.ToString());
			return sb.ToString();
		}
	}
}
