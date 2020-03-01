using Discord;
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
                await ReplyAsync("Invalid Class Try Again!");
                await AvailableClassesAsync();
                return;
            }

            user.Character.CharClass = charClass;
            var result = await dB.UpdateUserAsync(Context.MessageUser);
            if (result)
                await ReplyAsync("Characters Class  " + charClass);
            else
                await ReplyAsync("Failed To Update");

        }

        [Command("availableclasses")]
        public async Task AvailableClassesAsync()
        {
            var embed = new EmbedBuilder
            {
                Title = "Available Classes",
                Description = "Claases Available to pick *Admins can add classes*"
            };
            StringBuilder sb = new StringBuilder();
            foreach (var item in Character.classes)
            {
                sb.Append(item + "\n");
            }

            embed.AddField("Available Classes", sb.ToString());

            await ReplyAsync(embed: embed.Build());
        }

        [Command("availableraces")]
        public async Task AvailableRacesAsync()
        {
            var embed = new EmbedBuilder
            {
                Title = "Available Races",
                Description = "Races Available to pick *Admins can add races*"
            };
            StringBuilder sb = new StringBuilder();
            foreach (var item in Character.races)
            {
                sb.Append(item + "\n");
            }

            embed.AddField("Available Races", sb.ToString());

            await ReplyAsync(embed: embed.Build());
        }

        [Command("setrace")]
        [CharacterExists("Race")]
        public async Task SetRaceAsync(string race)
        {
            if (Character.races.Find(c => c.ToLower().Equals(race.ToLower())) == null)
            {
                await ReplyAsync("Invalid Race Try Again!");
                await AvailableRacesAsync();
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
            var character = Context.MessageUser.Character;
            var embed = new EmbedBuilder
            {
                Title = character.CharName,
                Description = "Character Info"
            };
            embed.AddField("Character Name", (character.CharName == "" || character.CharName == null ? "Not Set Please Follow !CharacterHelp" : character.CharName))
                .AddField("Character Level", (character.CharLevel == 0 ? "Speak to DM" : character.CharLevel.ToString()))
                .AddField("Character Race", (character.CharRace == "" || character.CharRace == null ? "Not Set Please Follow !CharacterHelp" : character.CharRace))
                .AddField("Character Class", (character.CharClass == "" || character.CharClass == null ? "Not Set Please Follow !CharacterHelp" : character.CharClass));


            await ReplyAsync(embed: embed.Build());
        }

    }
}
