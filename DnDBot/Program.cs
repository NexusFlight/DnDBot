using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
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
            var token = "NDU4MzE1OTY2MzMwNDM3NjQz.XlQx1Q.fEOE5VmK5kfmalwfH9kBs4hjG4M";

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
