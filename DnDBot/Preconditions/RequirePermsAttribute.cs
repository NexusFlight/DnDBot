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

        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var dbCon = (IDbCon)services.GetService(typeof(IDbCon));
            var user = dbCon.GetUserAsync(context.User.Id).Result;
            if(user == null)
            {
                return Task.FromResult(PreconditionResult.FromError("Speak To An Admin/DM to Approve You"));
            }
            if(user.PermLevel <= RequiredPerm)
            {
                return Task.FromResult(PreconditionResult.FromSuccess());
            }
            else
            {
                return Task.FromResult(PreconditionResult.FromError("You Do Not Have The Required Permissions To Run This Command"));
            }
        }
    }
}
