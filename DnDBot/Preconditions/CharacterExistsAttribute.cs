using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DnDBot
{
    class CharacterExistsAttribute : PreconditionAttribute
    {
        public string CharAttribute { get; }
        public CharacterExistsAttribute(string charAttribute) => CharAttribute = charAttribute;
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var dbCon = (DBCon)services.GetService(typeof(DBCon));
            var character = dbCon.GetUserAsync(context.User.Id).Result.Character;

            var error = "";
            switch (CharAttribute)
            {
                case "Name":
                    if (character.CharName != null)
                    {
                        error = "Character Already Name Set, Speak to Admin/Dm to change";
                        context.Channel.SendMessageAsync(error);
                        return Task.FromResult(PreconditionResult.FromError(error));
                    }
                    break;
                case "Class":
                    if (character.CharClass != null)
                    {
                        error = "Character Already Class Set, Speak to Admin/Dm to change";
                        context.Channel.SendMessageAsync(error);
                        return Task.FromResult(PreconditionResult.FromError(error));
                    }
                    break;
                case "Race":
                    if (character.CharRace != null)
                    {
                        error = "Character Already Race Set, Speak to Admin/Dm to change";
                        context.Channel.SendMessageAsync(error);
                        return Task.FromResult(PreconditionResult.FromError(error));
                    }
                    break;
            }

            return Task.FromResult(PreconditionResult.FromSuccess());
        }
    }
}
