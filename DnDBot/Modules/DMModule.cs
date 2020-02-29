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
        DBCon dB { get; }
        public DMModule(DBCon dBCon)
        {
            dB = dBCon;
        }

        [Command("DMHelp")]
        public async Task HelpAsync()
        {
            await ReplyAsync("As admin you can do the following \n" +
            "Type !setcharacterlevel @name *level* to set characters level\n" +
            "Type !addcharactergold @name *gold to add* to add gold to the users pot\n" +
            "Type !addcharactermp @name *MP to add* to add MP to the users pot\n" +
            "Type !showcharacter @name to show users character\n" +
            "Type !clearcharacter @name to clear users character");
        }


        [Command("setCharacterLevel")]
        public async Task SetCharacterLevelAsync(string mention, int level)
        {
            var id = User.GetIDFromMention(mention);
            var creationUser = await dB.GetUserAsync(id);
            creationUser.Character.CharLevel = level;
            await dB.updateUserAsync(creationUser);
            await ReplyAsync("Level of " + creationUser.Character.CharName + " = " + level);
        }

        [Command("addCharactergold")]
        public async Task AddCharacterGoldAsync(string mention, int gold)
        {

            var id = User.GetIDFromMention(mention);
            var creationUser = await dB.GetUserAsync(id);
            creationUser.Character.Gold += gold;
            await dB.updateUserAsync(creationUser);
            await ReplyAsync("Gold Content of " + creationUser.Character.CharName + " = " + creationUser.Character.Gold);
        }
        [Command("addCharactermp")]
        public async Task AddCharacterMpAsync(string mention, int mp)
        {

            var id = User.GetIDFromMention(mention);
            var creationUser = await dB.GetUserAsync(id);
            creationUser.Character.MP += mp;
            await dB.updateUserAsync(creationUser);
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
    }
}
