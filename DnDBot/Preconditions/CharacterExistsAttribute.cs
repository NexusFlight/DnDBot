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
            var dbCon = (IDbCon)services.GetService(typeof(IDbCon));
            var character = dbCon.GetUserAsync(context.User.Id).Result.Character;

            switch (CharAttribute)
            {
                case "Name":
                    if (character.CharName != null && character.CharName != "")
                    {
                        return Task.FromResult(PreconditionResult.FromError("Character Already Name Set, Speak to Admin/Dm to change"));
                    }
                    break;
                case "Class":
                    if (character.CharClass != null && character.CharClass != "")
                    {
                        return Task.FromResult(PreconditionResult.FromError("Character Already Class Set, Speak to Admin / Dm to change"));
                    }
                    break;
                case "Race":
                    if (character.CharRace != null && character.CharRace != "")
                    {
                        return Task.FromResult(PreconditionResult.FromError("Character Already Race Set, Speak to Admin/Dm to change"));
                    }
                    break;
            }

            return Task.FromResult(PreconditionResult.FromSuccess());
        }
    }
}
