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

        [Command("AdminHelp")]
        public async Task HelpAsync()
        {
            await ReplyAsync("As admin you can do the following \n" +
            "Type !makeadmin @name to make another user a Admin\n" +
            "Type !createuser @name to make a User\n" +
            "Type !setcharacterlevel @name *level* to set characters level\n" +
            "Type !addcharactergold @name *gold to add* to add gold to the users pot\n" +
            "Type !addcharactermp @name *MP to add* to add MP to the users pot\n" +
            "Type !showcharacter @name to show users character\n" +
            "Type !clearcharacter @name to clear users character\n" +
            "Type !addrace *race* to add a race\n" +
            "Type !removerace *race* to remove a race\n" +
            "Type !addclass *class* to add a class\n" +
            "Type !removeclass *class* to remove a class\n" +
            "Type !setperms *number* 1 - Admin 5 - User\n"

            );

        }

        [Command("MakeAdmin")]
        public async Task MakeAdminAsync(string userName)
        {
            var id = GetIDFromInput(userName);
            var user = dB.GetUserAsync(id);
            user.Result.PermLevel = 1;
            var result = await dB.updateUserAsync(user.Result);
            if (result == false)
            {
                await ReplyAsync("Update Failed");
            }
            else
            {
                await ReplyAsync("Update applied");
            }

        }

        [Command("setperms")]
        public async Task SetPermsAsync(string userName, int permlevel)
        {
            var id = GetIDFromInput(userName);
            var user = dB.GetUserAsync(id);
            user.Result.PermLevel = permlevel;
            var result = await dB.updateUserAsync(user.Result);
            if (result == false)
            {
                await ReplyAsync("Update Failed");
            }
            else
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

        [Command("AddRace")]
        public async Task AddRaceAsync(string race)
        {
            await ReplyAsync("Races are now:");
            await ReplyAsync(Character.AddRace(race));
        }

        [Command("AddClass")]
        public async Task AddClassAsync(string classes)
        {
            await ReplyAsync("Classes are now:");
            await ReplyAsync(Character.AddClass(classes));
        }

        [Command("RemoveRace")]
        public async Task RemoveRaceAsync(string race)
        {
            await ReplyAsync("Races are now:");
            await ReplyAsync(Character.RemoveRace(race));
        }

        [Command("RemoveClass")]
        public async Task RemoveClassAsync(string classes)
        {
            await ReplyAsync("Classes are now:");
            await ReplyAsync(Character.RemoveClass(classes));
        }

        private ulong GetIDFromInput(string username)
        {
            Regex regex = new Regex("([<>!@])");
            var id = Convert.ToUInt64(regex.Replace(username, ""));
            return id;
        }
    }
}
