using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDBot
{
	public class User
	{
		public string Name { get; set; }
		[BsonId]
		public ulong Discord_ID { get; set; }
		public Character Character { get; set; }
		public int PermLevel { get; set; }

		public User(string name, ulong discord_ID,  int permLevel)
		{
			Name = name;
			Discord_ID = discord_ID;
			Character = new Character();
			PermLevel = permLevel;
		}

		public User(string name, ulong discord_ID, Character character, int permLevel)
		{
			Name = name;
			Discord_ID = discord_ID;
			Character = character;
			PermLevel = permLevel;
		}


		
	}
}
