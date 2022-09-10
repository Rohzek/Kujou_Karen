using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Kujou_Karen_Bot.Classes.Twitch.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace Kujou_Karen_Bot
{
    public class Program
    {
        // Global Stuff
        BotSettings settings;

        // Discord Bot Stuff
        DiscordSocketClient clientDiscord;
        public static CommandService commandsDiscord;
        IServiceProvider services;

        // Twitch stuff
        TwitchClient clientTwitch;
        

        // Starts main as an asycable program
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            SetupGlobal().Wait();

            await SetupDiscordBot();
            await SetupTwitchBot();

            await Task.Delay(-1); // This will keep it alive forever
        }

        async Task SetupGlobal()
        {
            if (settings == null) 
            {
                settings = Settings.GetSettings();
            }

            if (settings.DiscordToken.Length < 2)
            {
                Console.WriteLine(settings.DiscordToken);
                // Close the program if it's gonna cause an error here
                Environment.Exit(1);
            }

            if (settings.TwitchToken.Length < 2)
            {
                Console.WriteLine(settings.TwitchToken);
                // Close the program if it's gonna cause an error here
                Environment.Exit(1);
            }
        }

        async Task<Task> SetupDiscordBot() 
        {
            // https://discord.com/developers/applications/359724776069136387/information
            // https://discord.com/oauth2/authorize?client_id=359724776069136387&permissions=0&scope=bot%20applications.commands

            clientDiscord = new DiscordSocketClient();
            commandsDiscord = new CommandService();

            // Limits our client and command listener services
            services = new ServiceCollection().AddSingleton(clientDiscord).AddSingleton(commandsDiscord).BuildServiceProvider();

            // Event subscriptions
            clientDiscord.Log += DiscordLog;
            clientDiscord.MessageReceived += DiscordMessageReceived; // Watch for messages to log
            clientDiscord.MessageReceived += DiscordHandleCommands; // Watch for commands

            // Login to the bot services
            await DiscordRegisterCommands();
            await clientDiscord.LoginAsync(TokenType.Bot, settings.DiscordToken);
            await clientDiscord.StartAsync();
            await clientDiscord.SetGameAsync(settings.DiscordGame);

            return Task.CompletedTask;
        }

        Task DiscordLog(LogMessage msg) 
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        Task DiscordLogIncoming(string msg)
        {
            Console.WriteLine(msg);
            //Logger.Log(msg.ToString());
            return Task.CompletedTask;
        }

        async Task DiscordMessageReceived(SocketMessage message)
        {
            string output = "#" + Conversions.ChannelConvert(message) + " " + Conversions.NameConvert(message) + " " + Conversions.TimeConvert(message) + ": " + Conversions.MessageConvert(message);
            await DiscordLogIncoming(output);
        }

        Task DiscordRegisterCommands()
        {
            commandsDiscord.AddModulesAsync(Assembly.GetEntryAssembly(), services);
            Console.WriteLine(commandsDiscord.Commands.ToString());

            return Task.CompletedTask;
        }

        async Task DiscordHandleCommands(SocketMessage message)
        {
            var msg = message as SocketUserMessage;

            if (message is null || message.Author.IsBot)
            {
                return;
            }

            int argPos = 0;
            // If command starts with ! or with a mention
            if (msg.HasStringPrefix(settings.Prefix, ref argPos) || msg.HasMentionPrefix(clientDiscord.CurrentUser, ref argPos))
            {
                var context = new SocketCommandContext(clientDiscord, msg);

                var result = await commandsDiscord.ExecuteAsync(context, argPos, services);

                if (!result.IsSuccess)
                {
                    await message.Channel.SendMessageAsync("Error. Reason: " + result.ErrorReason);
                }
            }
        }

        // Twitch variables
        bool ShouldAnnounceModerators = false;
        bool ShouldAnnounceViewers = false;

        public Task SetupTwitchBot() 
        {
            // Examples: https://github.com/TwitchLib/TwitchLib
            ConnectionCredentials credentials = new ConnectionCredentials(settings.TwitchUser, settings.TwitchToken);
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30)
            };

            var customClient = new WebSocketClient(clientOptions);

            clientTwitch = new TwitchClient(customClient);
            clientTwitch.Initialize(credentials, settings.TwitchUser);

            clientTwitch.OnConnected += TwitchOnConnected;
            clientTwitch.OnLog += TwitchOnLog;
            clientTwitch.OnJoinedChannel += TwitchOnJoinedChannel;
            clientTwitch.OnMessageReceived += TwitchOnMessageReceived;

            clientTwitch.OnModeratorJoined += TwitchOnModJoin;
            clientTwitch.OnModeratorLeft += TwitchOnModLeave;
            clientTwitch.OnUserJoined += TwitchOnViewerJoin;
            clientTwitch.OnUserLeft += TwitchOnViewerLeft;



            clientTwitch.Connect();

            return Task.CompletedTask;
        }

        private void TwitchOnConnected(object sender, OnConnectedArgs e)
        {
            Console.WriteLine($"Connected to {e.AutoJoinChannel}");
        }

        private void TwitchOnLog(object sender, OnLogArgs e)
        {
            Console.WriteLine($"{e.DateTime.ToString()}: {e.BotUsername} - {e.Data}");
        }

        private void TwitchOnJoinedChannel(object sender, OnJoinedChannelArgs e)
        {
            Console.WriteLine($"I just joined {e.Channel.ToString()}!");
            clientTwitch.SendMessage(e.Channel, "Hihi~! It's me, Karen-desu~!");

            // Serialize all users in chat
        }

        private void TwitchOnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            // Log chat
            Console.WriteLine($"#{e.ChatMessage.Channel} - {e.ChatMessage.DisplayName}: {e.ChatMessage.Message}");

            // Check for commands
            TwitchCommandsHandler.Check(clientTwitch, e.ChatMessage, settings.Prefix);

            //if (e.ChatMessage.Message.Contains("badword"))
            //clientTwitch.TimeoutUser(e.ChatMessage.Channel, e.ChatMessage.Username, TimeSpan.FromMinutes(30), "Bad word! 30 minute timeout!");
        }

        private void TwitchOnModJoin(object sender, OnModeratorJoinedArgs e)
        {
            if (ShouldAnnounceModerators) 
            {
                clientTwitch.SendMessage(e.Channel, $"Everybody watch out! Officer {e.Username} is back!");
            }
            
        }

        private void TwitchOnModLeave(object sender, OnModeratorLeftArgs e)
        {
            if (ShouldAnnounceModerators)
            {
                clientTwitch.SendMessage(e.Channel, $"Alright, time to relax. Officer {e.Username} is gone.");
            }
        }

        private void TwitchOnViewerLeft(object sender, OnUserLeftArgs e)
        {
            if (ShouldAnnounceViewers)
            {
                clientTwitch.SendMessage(e.Channel, $"Hihi {e.Username}~!");
            }
        }

        private void TwitchOnViewerJoin(object sender, OnUserJoinedArgs e)
        {
            if (ShouldAnnounceViewers)
            {
                clientTwitch.SendMessage(e.Channel, $"Awwww. We'll miss you, {e.Username}.");
            }
        }
    }
}
