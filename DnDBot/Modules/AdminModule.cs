using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnDBot
{
  
    public class AdminModule : ModuleBase<UserCommandContext>
    {
        private readonly DBCon dB;
        public AdminModule(DBCon dB)
        {
            this.dB = dB;
        }

        [Command("MakeAdmin")]
        public async Task MakeAdminAsync(string userName)
        {
            var id = GetIDFromInput(userName);
            if (IsUserAdmin())
            {
                Context.MessageUser.PermLevel = 1;
                var result = await dB.setPermLevelforUserAsync(id, 1);
                if(result == false)
                {
                    await ReplyAsync("Update Failed");
                }else
                {
                    await ReplyAsync("Update applied");
                }
            }
            else
            {
                await ReplyAsync("Insufficient Permissions");
            }
        }

        [Command("CreateUser")]
        public async Task CreateUserAsync(string username)
        {
            
            if (IsUserAdmin())
            {
                var id = GetIDFromInput(username);
                var creationUser = new User(Context.Guild.GetUser(id).Username, id, 5);
                await dB.CreateUserAsync(creationUser);
                await ReplyAsync("User Created");
            }
            else
            {
                await ReplyAsync("Insufficient Permissions");
            }
        }

        [Command("setCharacterLevel")]
        public async Task SetCharacterLevelAsync(string username, int level)
        {
           
            if (IsUserAdmin())
            {
                var id = GetIDFromInput(username);
                var creationUser = await dB.GetUserAsync(id);
                creationUser.Character.CharLevel = level;
                await dB.updateUserAsync(creationUser);
                await ReplyAsync("Level of " + creationUser.Character.CharName + " = " + level);
            }
            else
            {
                await ReplyAsync("Insufficient Permissions");
            }
        }

        [Command("addCharactergold")]
        public async Task AddCharacterGoldAsync(string username, int gold)
        {

            if (IsUserAdmin())
            {
                var id = GetIDFromInput(username);
                var creationUser = await dB.GetUserAsync(id);
                creationUser.Character.Gold += gold;
                await dB.updateUserAsync(creationUser);
                await ReplyAsync("Gold Content of " + creationUser.Character.CharName + " = " + creationUser.Character.Gold);
            }
            else
            {
                await ReplyAsync("Insufficient Permissions");
            }
        }
        [Command("addCharactermp")]
        public async Task AddCharacterMpAsync(string username, int mp)
        {

            if (IsUserAdmin())
            {
                var id = GetIDFromInput(username);
                var creationUser = await dB.GetUserAsync(id);
                creationUser.Character.MP += mp;
                await dB.updateUserAsync(creationUser);
                await ReplyAsync("MP Content of " + creationUser.Character.CharName + " = " + creationUser.Character.MP);
            }
            else
            {
                await ReplyAsync("Insufficient Permissions");
            }
        }

        [Command("ShowCharacter")]
        public async Task ShowCharacterAsync(string username)
        {
            if(IsUserAdmin())
                await ReplyAsync(dB.GetUserAsync(GetIDFromInput(username)).Result.Character.ToString());
        }

        private ulong GetIDFromInput(string username)
        {
            Regex regex = new Regex("([<>!@])");
            var id = Convert.ToUInt64(regex.Replace(username, ""));
            return id;
        }
        private bool IsUserAdmin()
        {
            var permLevel = Context.MessageUser.PermLevel;

            return permLevel <= 2;
        }
    }
}
