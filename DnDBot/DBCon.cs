using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot
{
    public class DBCon : IDbCon
    {
        private IMongoCollection<User> Collection;

        public DBCon()
        {
            Collection = new MongoClient("mongodb://192.168.1.252:27017/admin").GetDatabase("MagicPointsBot").GetCollection<User>("Users");
        }

        public async Task CreateUserAsync(User user)
        {
            await Collection.InsertOneAsync(user);
        }
        
        public async Task<bool> UpdateUserAsync(User user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Discord_ID, user.Discord_ID);
            var result = await Collection.ReplaceOneAsync(filter,user);
            return result.IsAcknowledged;
        }

        public async Task<User> GetUserAsync(ulong id)
        {
            return await Collection.Find(u => u.Discord_ID == id).SingleOrDefaultAsync();
            
        }




        //public async System.Threading.Tasks.Task<int> getMPforUserAsync(ulong id)
        //{
        //    var result = await collection.FindAsync(u => u.Discord_ID == id).Result.FirstOrDefaultAsync();

        //    return result.Character.MP;
        //}

        //public async System.Threading.Tasks.Task<int> getPermLevelforUserAsync(ulong id)
        //{
        //    var result = await collection.FindAsync(u => u.Discord_ID == id).Result.FirstOrDefaultAsync();
        //    return result.PermLevel;
        //}
        //public async System.Threading.Tasks.Task<bool> setPermLevelforUserAsync(ulong id, int permLevel)
        //{
        //    var update = Builders<User>.Update.Set(u => u.PermLevel, permLevel);
        //    var result = await collection.UpdateOneAsync(u => u.Discord_ID == id, update);

        //    return result.IsAcknowledged;
        //}
    }
}
