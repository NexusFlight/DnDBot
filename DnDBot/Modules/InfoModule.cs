using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace DnDBot
{
    public class InfoModule : ModuleBase<SocketCommandContext>
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
            await ReplyAsync("You have " + dB.getMPforUserAsync(Context.User.Id).Result+ " MP" );
        }

        

        



    }
}
