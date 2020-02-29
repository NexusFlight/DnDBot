using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot
{
    public class RequirePermsAttribute : PreconditionAttribute
    {
        private readonly int RequiredPerm;
        public RequirePermsAttribute(int requiredPerm) => RequiredPerm = requiredPerm;

        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var dbCon = (DBCon)services.GetService(typeof(DBCon));
            var user = dbCon.GetUserAsync(context.User.Id).Result;
            if(user == null)
            {
                Console.WriteLine(context.User.Username + " Tired to run " + context.Message);
                var error = "Speak To An Admin/DM to Approve You";
                await context.Channel.SendMessageAsync(error);
                return PreconditionResult.FromError(error);
            }
            if(user.PermLevel <= RequiredPerm)
            {
                return PreconditionResult.FromSuccess();
            }
            else
            {
                Console.WriteLine(context.User.Username + " Tired to run " + context.Message);
                var error = "You Do Not Have The Required Permissions To Run This Command";
                await context.Channel.SendMessageAsync(error);
                return PreconditionResult.FromError(error);
                
            }
        }
    }
}
