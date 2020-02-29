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
        private readonly DBCon dB;
        public AdminModule(DBCon dB)
        {
            this.dB = dB;
        }

        [Command("AdminHelp")]
        public async Task HelpAsync()
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
            "Type !setperms *number*\n1 - Admin \n2 - DM \n5 - User\n"

            );

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
            var result = await dB.updateUserAsync(user.Result);
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
