using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnDBot
{
    [RequirePerms(2)]
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
                Context.MessageUser.PermLevel = 1;
                var result = await dB.updateUserAsync(Context.MessageUser);
                if(result == false)
                {
                    await ReplyAsync("Update Failed");
                }else
                {
                    await ReplyAsync("Update applied");
                }
            
        }

        [Command("CreateUser")]
        public async Task CreateUserAsync(string username)
        {
                var id = GetIDFromInput(username);
                var creationUser = new User(Context.Guild.GetUser(id).Username, id, 5);
                await dB.CreateUserAsync(creationUser);
                await ReplyAsync("User Created");
        }

        [Command("setCharacterLevel")]
        public async Task SetCharacterLevelAsync(string username, int level)
        {
                var id = GetIDFromInput(username);
                var creationUser = await dB.GetUserAsync(id);
                creationUser.Character.CharLevel = level;
                await dB.updateUserAsync(creationUser);
                await ReplyAsync("Level of " + creationUser.Character.CharName + " = " + level);
        }

        [Command("addCharactergold")]
        public async Task AddCharacterGoldAsync(string username, int gold)
        {

                var id = GetIDFromInput(username);
                var creationUser = await dB.GetUserAsync(id);
                creationUser.Character.Gold += gold;
                await dB.updateUserAsync(creationUser);
                await ReplyAsync("Gold Content of " + creationUser.Character.CharName + " = " + creationUser.Character.Gold);
        }
        [Command("addCharactermp")]
        public async Task AddCharacterMpAsync(string username, int mp)
        {

                var id = GetIDFromInput(username);
                var creationUser = await dB.GetUserAsync(id);
                creationUser.Character.MP += mp;
                await dB.updateUserAsync(creationUser);
                await ReplyAsync("MP Content of " + creationUser.Character.CharName + " = " + creationUser.Character.MP);
            
        }

        [Command("ShowCharacter")]
        public async Task ShowCharacterAsync(string username)
        {
                await ReplyAsync(dB.GetUserAsync(GetIDFromInput(username)).Result.Character.ToString());
        }

        [Command("ClearCharacter")]
        public async Task ClearCharacterAsync(string username)
        {
            var user = await dB.GetUserAsync(GetIDFromInput(username));
            user.Character = new Character();
            var result = await dB.updateUserAsync(user);
            if (result)
            {
                await ReplyAsync("Character Cleared");
            }
            else
            {
                await ReplyAsync("Clearing Failed");
            }
        }

        private ulong GetIDFromInput(string username)
        {
            Regex regex = new Regex("([<>!@])");
            var id = Convert.ToUInt64(regex.Replace(username, ""));
            return id;
        }
    }
}
