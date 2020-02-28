using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DnDBot
{
    class Program
    {

        private DiscordSocketClient client;
        private CommandHandler Command;
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();


        public async Task MainAsync()
        {
            client = new DiscordSocketClient();
            client.Log += Log;
            Command = new CommandHandler(client, new CommandService());
            var token = File.ReadAllText(@"C:\Users\Altair\source\repos\DnDBot\DnDBot\Token.txt");

            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();
            await Command.InstallCommandsAsync();

            await Task.Delay(-1);

        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
