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
        private readonly IDbCon dB;
        public InfoModule(IDbCon dB)
        {
            this.dB = dB;
        }


        [RequirePerms(5)]
        [Command("Wallet")]
        [Summary("Returns Magic Point Wallet Total")]
        public async Task WalletAsync()
        {
            await ReplyAsync("You have " + Context.MessageUser.Character.MP + " MP"  +
            "You have " + Context.MessageUser.Character.Gold + " Gold" );
        }

        [RequirePerms(5)]
        [Command("CharacterHelp")]
        public async Task CharacterHelpAsync()
        {
            await ReplyAsync("Use !Createcharacter *Name of Character* to create your character" +
            "Use !SetClass *Name of Class* to set your characters class" +
            "Use !SetRace *Name of Race* to set your characters Race");
        }

        [Command("Help")]
        public async Task HelpAsync()
        {
            await ReplyAsync("An admin must make you a user before you can do anything\n" +
            "Type !Characterhelp to see how to make a character\n" +
            "Type !showcharacter to see your characters details\n" +
            "Type !wallet to see how many Magic Points and gold you have");

        }

        [RequirePerms(2)]
        [Command("DMHelp")]
        public async Task DMHelpAsync()
        {
            await ReplyAsync("As a DM you can do the following \n" +
            "Type !setcharacterlevel @name *level* to set characters level\n" +
            "Type !addcharactergold @name *gold to add* to add gold to the users pot\n" +
            "Type !addcharactermp @name *MP to add* to add MP to the users pot\n" +
            "Type !showcharacter @name to show users character\n" +
            "Type !clearcharacter @name to clear users character");
        }

        [RequirePerms(1)]
        [Command("AdminHelp")]
        public async Task AdminHelpAsync()
        {
            await ReplyAsync("As admin you can do the following \n" +
            "Type !makeadmin @name to make another user a Admin\n" +
            "Type !createuser @name to make a User\n" +
            "Type !addrace *race* to add a race\n" +
            "Type !removerace *race* to remove a race\n" +
            "Type !addclass *class* to add a class\n" +
            "Type !removeclass *class* to remove a class\n" +
            "Type !clear *number* clears up to 100 messages in a channel\n" +
            "Type !DMHelp for the help available to DMs which you can access\n" +
            "Type !setperms *number*\n1 - Admin \n2 - DM \n5 - User\n");

        }
    }
}
