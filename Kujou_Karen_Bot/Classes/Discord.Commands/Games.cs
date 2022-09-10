using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Kujou_Karen_Bot.Classes.Commands.Helpers;
using Newtonsoft.Json;

namespace Kujou_Karen_Bot.Classes.Commands
{
    [Name("Games"), Group("games"), Summary("Commands for simple entertainment")]
    public class Games : ModuleBase<SocketCommandContext>
    {
        string[] rockPaperScissorsPossibility = { "paper", "scissors", "rock" };

        [Command("8ball", RunMode = RunMode.Async), Alias("8b"), Remarks("[Your question]"), Summary("!8ball Will an update for Simple Ore Generation ever come out?")]
        public async Task EightBall([Remainder] string question)
        {
            string url = "https://8ball.delegator.com/magic/JSON/";

            var json = WebConnection.Get(url + question);
            var answer = JsonConvert.DeserializeObject<Helpers.RootObject>(json);

            var builder = new EmbedBuilder()
            {
                Title = "The Magic Eight Ball says:",
                Color = Color.Purple,
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Requested by {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };

            builder.AddField(x =>
            {
                x.Name = "Question:";
                x.Value = answer.magic.question;
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "Answer:";
                x.Value = answer.magic.answer;
                x.IsInline = false;
            });

            await ReplyAsync("", false, builder.Build());
        }

        [Command("rockpaperscissors"), Alias("rps"), Remarks("[choice]"), Summary("!rockpaperscissors rock")]
        public async Task RockPaperScissors([Remainder] string choice)
        {
            string result = "";

            choice = choice.ToLower();
            Random random = new Random();
            string botChoice = rockPaperScissorsPossibility[random.Next(rockPaperScissorsPossibility.Length)];

            switch (choice)
            {
                case ("paper"):
                    if (botChoice == "rock")
                    {
                        result = "You win!";
                    }
                    else if (botChoice == "scissors")
                    {
                        result = "You lose.";
                    }
                    else
                    {
                        result = "It's a tie!";
                    }
                    break;

                case ("scissor"):
                case ("scissors"):
                    if (botChoice == "paper")
                    {
                        result = "You win!";
                    }
                    else if (botChoice == "rock")
                    {
                        result = "You lose.";
                    }
                    else
                    {
                        result = "It's a tie!";
                    }
                    break;

                case ("rock"):
                case ("rocks"):
                    if (botChoice == "scissors")
                    {
                        result = "You win!";
                    }
                    else if (botChoice == "paper")
                    {
                        result = "You lose.";
                    }
                    else
                    {
                        result = "It's a tie!";
                    }
                    break;
                default:
                    await ReplyAsync("Do... you... even understand this game?");
                    break;
            }

            await ReplyAsync($"I chose {botChoice} so: {Context.Message.Author.Mention}, {result}");
        }
    }
}
