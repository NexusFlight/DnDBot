using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Modules
{
    public class UserModule : ModuleBase<UserCommandContext>
    {
        private readonly DBCon dB;
        public UserModule(DBCon dB)
        {
            this.dB = dB;
        }


        [Command("createcharacter")]
        public async Task CreateUserAsync(string name)
        {
            var user = Context.MessageUser;
            if (!hasUserAsync().Result)
                return;
            user.Character.CharName = name;

            var result = await dB.updateUserAsync(Context.MessageUser);
            if (result)
                await ReplyAsync("Character " + name + " Created");
            else
                await ReplyAsync("Failed To Update");
        }

        [Command("setclass")]
        public async Task SetClassAsync(string charClass)
        {
            var user = Context.MessageUser;
            if (!hasUserAsync().Result)
                return;
            user.Character.CharClass = charClass;
            var result = await dB.updateUserAsync(Context.MessageUser);
            if (result)
                await ReplyAsync("Characters Class  " + charClass);
            else
                await ReplyAsync("Failed To Update");

        }
        [Command("setrace")]
        public async Task SetRaceAsync(string race)
        {
            var user = Context.MessageUser;
            if (!hasUserAsync().Result)
                return;
            user.Character.CharRace = race;
            var result = await dB.updateUserAsync(Context.MessageUser);
            if (result)
                await ReplyAsync("Characters Race " + race);
            else
                await ReplyAsync("Failed To Update");
        }

        [Command("ShowCharacter")]
        public async Task ShowCharacterAsync()
        {
            await ReplyAsync(Context.MessageUser.Character.ToString());
        }

        private async Task<bool> hasUserAsync()
        {
            var user = Context.MessageUser;
            if (user == null)
            {
                await ReplyAsync("An admin needs to approve you as a user first");
                return false;
            }
            return true;
        }
    }
}
