using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot
{
    [RequirePerms(2)]
    public class DMModule : ModuleBase<UserCommandContext>
    {
        private readonly IDbCon dB;
        public DMModule(IDbCon dBCon)
        {
            dB = dBCon;
        }

        


        [Command("setCharacterLevel")]
        public async Task SetCharacterLevelAsync(string mention, int level)
        {
            var id = User.GetIDFromMention(mention);
            var creationUser = await dB.GetUserAsync(id);
            creationUser.Character.CharLevel = level;
            await dB.UpdateUserAsync(creationUser);
            await ReplyAsync("Level of " + creationUser.Character.CharName + " = " + level);
        }

        [Command("addCharactergold")]
        public async Task AddCharacterGoldAsync(string mention, int gold)
        {

            var id = User.GetIDFromMention(mention);
            var creationUser = await dB.GetUserAsync(id);
            creationUser.Character.Gold += gold;
            await dB.UpdateUserAsync(creationUser);
            await ReplyAsync("Gold Content of " + creationUser.Character.CharName + " = " + creationUser.Character.Gold);
        }
        [Command("addCharactermp")]
        public async Task AddCharacterMpAsync(string mention, int mp)
        {

            var id = User.GetIDFromMention(mention);
            var creationUser = await dB.GetUserAsync(id);
            creationUser.Character.MP += mp;
            await dB.UpdateUserAsync(creationUser);
            await ReplyAsync("MP Content of " + creationUser.Character.CharName + " = " + creationUser.Character.MP);

        }

        [Command("ShowCharacter")]
        public async Task ShowCharacterAsync(string mention)
        {
            await ReplyAsync(dB.GetUserAsync(User.GetIDFromMention(mention)).Result.Character.ToString());
        }

        [Command("ClearCharacter")]
        public async Task ClearCharacterAsync(string mention)
        {
            var user = await dB.GetUserAsync(User.GetIDFromMention(mention));
            user.Character = new Character();
            var result = await dB.UpdateUserAsync(user);
            if (result)
            {
                await ReplyAsync("Character Cleared");
            }
            else
            {
                await ReplyAsync("Clearing Failed");
            }
        }

        [Command("ClearClass")]
        public async Task ClearClassAsync(string mention)
        {
            var user = await dB.GetUserAsync(User.GetIDFromMention(mention));
            user.Character.CharClass = "";
            var result = await dB.UpdateUserAsync(user);
            if (result)
            {
                await ReplyAsync("Character Class Cleared");
            }
            else
            {
                await ReplyAsync("Clearing Failed");
            }
        }

        [Command("ClearRace")]
        public async Task ClearRaceAsync(string mention)
        {
            var user = await dB.GetUserAsync(User.GetIDFromMention(mention));
            user.Character.CharRace = "";
            var result = await dB.UpdateUserAsync(user);
            if (result)
            {
                await ReplyAsync("Character Race Cleared");
            }
            else
            {
                await ReplyAsync("Clearing Failed");
            }
        }
    }
}
