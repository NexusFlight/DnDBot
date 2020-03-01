using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Modules
{
    [RequirePerms(5)]
    public class UserModule : ModuleBase<UserCommandContext>
    {
        private readonly IDbCon dB;
        public UserModule(IDbCon dB)
        {
            this.dB = dB;
        }


        [Command("createcharacter")]
        [CharacterExists("Name")]
        public async Task CreateUserAsync(string name)
        {
            var user = Context.MessageUser;
            user.Character.CharName = name;

            var result = await dB.UpdateUserAsync(Context.MessageUser);
            if (result)
                await ReplyAsync("Character " + name + " Created");
            else
                await ReplyAsync("Failed To Update");
        }

        [Command("setclass")]
        [CharacterExists("Class")]
        public async Task SetClassAsync(string charClass)
        {
            var user = Context.MessageUser;
            if(Character.classes.Find(c => c.ToLower().Equals(charClass.ToLower())) == null)
            {
                await ReplyAsync("Invalid Class Try Again! \nValid clases are:\n");
                StringBuilder sb = new StringBuilder();
                foreach (var item in Character.classes)
                {
                    sb.Append(item + "\n");
                }
                await ReplyAsync(sb.ToString());
                return;
            }

            user.Character.CharClass = charClass;
            var result = await dB.UpdateUserAsync(Context.MessageUser);
            if (result)
                await ReplyAsync("Characters Class  " + charClass);
            else
                await ReplyAsync("Failed To Update");

        }

        [Command("setrace")]
        [CharacterExists("Race")]
        public async Task SetRaceAsync(string race)
        {
            if (Character.races.Find(c => c.ToLower().Equals(race.ToLower())) == null)
            {
                await ReplyAsync("Invalid Race Try Again! \nValid Races are:\n");
                StringBuilder sb = new StringBuilder();
                foreach (var item in Character.races)
                {
                    sb.Append(item + "\n");
                }
                await ReplyAsync(sb.ToString());
                return;
            }
            var user = Context.MessageUser;
            user.Character.CharRace = race;
            var result = await dB.UpdateUserAsync(Context.MessageUser);
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

    }
}
