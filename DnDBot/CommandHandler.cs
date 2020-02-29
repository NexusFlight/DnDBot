using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace DnDBot
{
    class CommandHandler
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commands;
        private readonly IServiceProvider services;
        private readonly DBCon db;
        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            this.client = client;
            this.commands = commands;
            db = new DBCon();
            services = new ServiceCollection()
                .AddSingleton<DBCon>()
                .BuildServiceProvider();
        }

        public async Task InstallCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;

            await commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: services);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) 
                return;

            int argPos = 0;

            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos) || message.Author.IsBot))
                return;

            var user = await db.GetUserAsync(message.Author.Id);
            var context = new UserCommandContext(client, message,user);
            
            var result = await commands.ExecuteAsync(context: context, argPos: argPos, services: services);

        }
    }
}
