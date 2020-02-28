using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
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
        private IMongoCollection<User> collection;

        public DBCon()
        {
            dbClient = new MongoClient("mongodb://localhost:27017/admin");
            database = dbClient.GetDatabase("MagicPointsBot");
            collection = database.GetCollection<User>("Users");
        }

        public async System.Threading.Tasks.Task CreateUserAsync(User user)
        {
            await collection.InsertOneAsync(user);
        }
        public async System.Threading.Tasks.Task<int> getMPforUserAsync(ulong id)
        {
            var result = await collection.FindAsync(u => u.Discord_ID == id).Result.FirstOrDefaultAsync();
            
            return result.Character.MP;
        }

        public async System.Threading.Tasks.Task<int> getPermLevelforUserAsync(ulong id)
        {
            var result = await collection.FindAsync(u => u.Discord_ID == id).Result.FirstOrDefaultAsync();
            Console.WriteLine(result.PermLevel);
            return result.PermLevel;
        }
        public async System.Threading.Tasks.Task<bool> setPermLevelforUserAsync(ulong id,int permLevel)
        {
            var update = Builders<User>.Update.Set(u => u.PermLevel, permLevel);
            var result = await collection.UpdateOneAsync(u => u.Discord_ID == id, update);
            
            return result.IsAcknowledged;
        }

    }
}
