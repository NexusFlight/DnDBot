using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDBot
{
    public class DBCon
    {
        private MongoClient dbClient;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;

        public DBCon()
        {
            dbClient = new MongoClient("mongodb://localhost:27017/admin");
            database = dbClient.GetDatabase("MagicPointsBot");
            collection = database.GetCollection<BsonDocument>("Users");
        }

        public void CreateUser(string name, ulong id, int charLevel, string race, string charClass, int totalMP, int level, bool isAlive, int gold)
        {
            var document = new BsonDocument
            {
                {"Name", name},
                {"Discord_ID", id.ToString()},
                {"CharacterLevel", charLevel},
                {"Race", race},
                {"Class", charClass},
                {"TotalMP", totalMP},
                {"PermLevel", level},
                {"Living", isAlive},
                {"Gold",  gold}
            };
            collection.InsertOne(document);
        }
        public string getMPforUser(ulong id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("Discord_ID", id.ToString());
            
            return collection.Find(filter).FirstOrDefault()[6].ToString();
        }




    }
}
