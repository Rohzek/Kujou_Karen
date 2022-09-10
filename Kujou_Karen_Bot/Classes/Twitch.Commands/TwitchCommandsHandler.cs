using System;
using System.Text.RegularExpressions;
using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace Kujou_Karen_Bot.Classes.Twitch.Commands
{
    class TwitchCommandsHandler
    {
        static Regex regex;
        static Match match;

        static string command;

        public static void Check(TwitchClient client, ChatMessage msg, string prefix)
        {
            
            string patternCommand = $"^{prefix}\\w+";
            regex = new Regex(patternCommand);
            match = regex.Match(msg.Message);

            if (match.Success)
            {
                Console.WriteLine($"I received the command: {match.Value}");
                command = match.Value.Substring(1);
                Console.WriteLine($"Command by itself is: {command}");
            }
        }
    }
}
