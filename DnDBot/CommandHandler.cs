using Discord;
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
        private readonly LoggingService logging;
        public CommandHandler(DiscordSocketClient client, CommandService commands, LoggingService logging)
        {
            this.client = client;
            this.commands = commands;
            this.logging = logging;
            services = new ServiceCollection()
                .AddTransient(typeof(IDbCon),typeof(DBCon))
                .BuildServiceProvider();
        }

        public async Task InstallCommandsAsync()
        {
            client.MessageReceived += HandleCommandAsync;
            commands.CommandExecuted += Commands_CommandExecuted;
            await commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: services);
            
        }

        private async Task Commands_CommandExecuted(Discord.Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!command.IsSpecified)
            {
                await logging.LogAsync(new LogMessage(LogSeverity.Info, "CommandHandler", "Command not Specified"));
                await context.Channel.SendMessageAsync(result.ErrorReason);
                return;
            }
            if (result.IsSuccess)
            {
                await logging.LogAsync(new LogMessage(LogSeverity.Info, "CommandHandler", "Function " +command.Value.Name+ " Ran successfully by "+context.User.Username));
                return;
            }

            await logging.LogAsync(new LogMessage(LogSeverity.Error,"CommandHandler",context.User.Username + " Tried to run " +command.Value.Name));
            await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) 
                return;

            int argPos = 0;

            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(client.CurrentUser, ref argPos)) || message.Author.IsBot)
                return;

            var db = (IDbCon)services.GetService(typeof(IDbCon));
            var user = await db.GetUserAsync(message.Author.Id);
            var context = new UserCommandContext(client, message,user);
            
            var result = await commands.ExecuteAsync(context: context, argPos: argPos, services: services);

        }
    }
}
