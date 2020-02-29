using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace DnDBot
{
    [RequirePerms(5)]
    public class InfoModule : ModuleBase<UserCommandContext>
    {
        private readonly DBCon dB;
        public InfoModule(DBCon dB)
        {
            this.dB = dB;
        }


        [Command("Wallet")]
        [Summary("Returns Magic Point Wallet Total")]
        public async Task WalletAsync()
        {
            await ReplyAsync("You have " + Context.MessageUser.Character.MP + " MP" );
            await ReplyAsync("You have " + Context.MessageUser.Character.Gold + " Gold" );
        }

        [Command("CharacterHelp")]
        public async Task CharacterHelpAsync()
        {
            await ReplyAsync("Use !Createcharacter *Name of Character* to create your character");
            await ReplyAsync("Use !SetClass *Name of Class* to set your characters class");
            await ReplyAsync("Use !SetRace *Name of Race* to set your characters Race");
        }

        



    }
}
