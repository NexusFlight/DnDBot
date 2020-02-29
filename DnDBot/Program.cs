﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DnDBot
{
    class Program
    {

        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();


        public async Task MainAsync()
        {
            var client = new DiscordSocketClient(new DiscordSocketConfig { ExclusiveBulkDelete = true });
            client.Log += Log;
            var command = new CommandHandler(client, new CommandService());
            var token = File.ReadAllText(@"Token.txt");

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            await command.InstallCommandsAsync();

            await Task.Delay(-1);

        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
