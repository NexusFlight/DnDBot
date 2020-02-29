using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace DnDBot
{
    public class UserCommandContext : SocketCommandContext
    {
        public User MessageUser { get; }
        public UserCommandContext(DiscordSocketClient client, SocketUserMessage msg, User user)
            : base(client,msg)
        {
            MessageUser = user;
        }
    }
}
