using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot.Modules
{
    public class UserModule : ModuleBase<SocketCommandContext>
    {
        private readonly DBCon dB;
        public UserModule(DBCon dB)
        {
            this.dB = dB;
        }


        [Command("createcharacter")]
        public async Task CreateUserAsync()
        {

        }
    }
}
