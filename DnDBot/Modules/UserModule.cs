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
            Context.MessageUser.Character.CharName = name;
            var result = await dB.updateUserAsync(Context.MessageUser);
            if (result)
                await ReplyAsync("Character " + name + " Created");
            else
                await ReplyAsync("Failed To Update");
        }

        [Command("setclass")]
        public async Task SetClassAsync(string charClass)
        {
            Context.MessageUser.Character.CharClass = charClass;
            var result = await dB.updateUserAsync(Context.MessageUser);
            if(result)
                await ReplyAsync("Characters Class  " + charClass);
            else
                await ReplyAsync("Failed To Update");

        }
        [Command("setrace")]
        public async Task SetRaceAsync(string race)
        {
            Context.MessageUser.Character.CharRace = race;
            var result = await dB.updateUserAsync(Context.MessageUser);
            if(result)
                await ReplyAsync("Characters Race " + race);
            else
                await ReplyAsync("Failed To Update");
        }

        [Command("ShowCharacter")]
        public async Task ShowCharacterAsync()
        {
            await ReplyAsync(Context.MessageUser.Character.ToString());
        }
    }
}
