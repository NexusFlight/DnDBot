using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DnDBot
{
    [RequirePerms(1)]
    public class AdminModule : ModuleBase<UserCommandContext>
    {
        private readonly IDbCon dB;
        public AdminModule(IDbCon dB)
        { 
            this.dB = dB;
        }

        

        [Command("MakeAdmin")]
        public async Task MakeAdminAsync(string mention)
        {
            await SetPermsAsync(mention, 1);
            await Context.Client.GetUser(User.GetIDFromMention(mention)).SendMessageAsync("You are now an Admin Please use !Adminhelp for more info");
        }

        [Command("MakeDM")]
        public async Task MakeDMAsync(string mention)
        {
            await SetPermsAsync(mention, 2);
            await Context.Client.GetUser(User.GetIDFromMention(mention)).SendMessageAsync("You are now an DM Please use !DMhelp for more info");
        }

        [Command("setperms")]
        public async Task SetPermsAsync(string mention, int permlevel)
        {
            var id = User.GetIDFromMention(mention);
            var user = dB.GetUserAsync(id);
            user.Result.PermLevel = permlevel;
            var result = await dB.UpdateUserAsync(user.Result);
            if (result == false)
            {
                await ReplyAsync("Update Failed");
            }
            else
            {
                await ReplyAsync("Update applied");
            }

        }

        [Command("CreateUser")]
        public async Task CreateUserAsync(string mention)
        {
            var id = User.GetIDFromMention(mention);
            var creationUser = new User(Context.Guild.GetUser(id).Username, id, 5);
            await dB.CreateUserAsync(creationUser);
            await ReplyAsync("User Created");
            await Context.Client.GetUser(id).SendMessageAsync("You have been approved for the server\n" +
                "Please follow the !characterhelp command to create your character");
        }

        [Command("Clear")]
        public async Task ClearChatAsync(int numberOfMsgs)
        {
            var messages = await Context.Channel.GetMessagesAsync(numberOfMsgs+1).FlattenAsync();
            await Context.Guild.GetTextChannel(Context.Channel.Id).DeleteMessagesAsync(messages);
        }

        [Command("AddRace")]
        public async Task AddRaceAsync(string race)
        {
            await ReplyAsync("Races are now:");
            await ReplyAsync(Character.AddRace(race));
        }

        [Command("AddClass")]
        public async Task AddClassAsync(string classes)
        {
            await ReplyAsync("Classes are now:");
            await ReplyAsync(Character.AddClass(classes));
        }

        [Command("RemoveRace")]
        public async Task RemoveRaceAsync(string race)
        {
            await ReplyAsync("Races are now:");
            await ReplyAsync(Character.RemoveRace(race));
        }

        [Command("RemoveClass")]
        public async Task RemoveClassAsync(string classes)
        {
            await ReplyAsync("Classes are now:");
            await ReplyAsync(Character.RemoveClass(classes));
        }

        
    }
}
