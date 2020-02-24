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
            await ReplyAsync("You have " + dB.getMPforUser(Context.User.Id)+ " MP" );
        }

        [Command("CreateUser")]
        public async Task CreateUserAsync(int level, string race, string charClass, int totalMP,int permLevel, bool isLiving, int gold)
        {
            var user = Context.User;
            dB.CreateUser(user.Username, user.Id, 1, "Elf", "Ranger", 1000, 5, true, 5);
            await ReplyAsync("User Created");
        }



    }
}
