using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnDBot
{
  
    public class AdminModule : ModuleBase<UserCommandContext>
    {
        private readonly DBCon dB;
        public AdminModule(DBCon dB)
        {
            this.dB = dB;
        }

        [Command("MakeAdmin")]
        public async Task MakeAdminAsync(string userName)
        {
            var userId = Context.MessageUser.Discord_ID;
            var id = GetIDFromInput(userName);
            if (IsUserAdmin(userId))
            {
                Context.MessageUser.PermLevel = 1;
                var result = await dB.setPermLevelforUserAsync(id, 1);
                if(result == false)
                {
                    await ReplyAsync("Update Failed");
                }else
                {
                    await ReplyAsync("Update applied");
                }
            }
            else
            {
                await ReplyAsync("Insufficient Permissions");
            }
        }

        [Command("CreateUser")]
        public async Task CreateUserAsync(string username)
        {
            var user = Context.User;
            if (IsUserAdmin(user.Id))
            {
                var id = GetIDFromInput(username);
                var creationUser = new User(Context.Guild.GetUser(id).Username, id, 5);
                await dB.CreateUserAsync(creationUser);
                await ReplyAsync("User Created");
            }
            else
            {
                await ReplyAsync("Insufficient Permissions");
            }
        }

        

        private ulong GetIDFromInput(string username)
        {
            Regex regex = new Regex("([<>!@])");
            var id = Convert.ToUInt64(regex.Replace(username, ""));
            return id;
        }
        private bool IsUserAdmin(ulong id)
        {
            var permLevel = Context.MessageUser.PermLevel;

            return permLevel <= 2;
        }
    }
}
