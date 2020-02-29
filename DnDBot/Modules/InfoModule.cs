using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace DnDBot
{
    public class InfoModule : ModuleBase<UserCommandContext>
    {
        private readonly DBCon dB;
        public InfoModule(DBCon dB)
        {
            this.dB = dB;
        }


        [RequirePerms(5)]
        [Command("Wallet")]
        [Summary("Returns Magic Point Wallet Total")]
        public async Task WalletAsync()
        {
            await ReplyAsync("You have " + Context.MessageUser.Character.MP + " MP" );
            await ReplyAsync("You have " + Context.MessageUser.Character.Gold + " Gold" );
        }

        [RequirePerms(5)]
        [Command("CharacterHelp")]
        public async Task CharacterHelpAsync()
        {
            await ReplyAsync("Use !Createcharacter *Name of Character* to create your character");
            await ReplyAsync("Use !SetClass *Name of Class* to set your characters class");
            await ReplyAsync("Use !SetRace *Name of Race* to set your characters Race");
        }

        [Command("Help")]
        public async Task HelpAsync()
        {
            await ReplyAsync("An admin must make you a user before you can do anything");
            await ReplyAsync("Type !Characterhelp to see how to make a character");
            await ReplyAsync("Type !showcharacter to see your characters details");
            await ReplyAsync("Type !wallet to see how many Magic Points and gold you have");

        }
        



    }
}
