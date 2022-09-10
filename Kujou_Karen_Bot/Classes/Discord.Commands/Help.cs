using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace Kujou_Karen_Bot.Classes.Commands
{
    [Name("Help"), Group("help"), Alias("commands"), Summary("Lists all available commands, or info about a specific command.")]
    public class Help : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService service;

        public Help(CommandService service)
        {
            this.service = service;
        }

        [Command, Summary("!help")]
        public async Task HelpAsync()
        {
            string prefix = "!";
            var builder = new EmbedBuilder()
            {
                Title = "These are the commands you can use:",
                Color = Color.Purple,
            };

            foreach (var module in service.Modules)
            {
                string description = null, summary = null;

                foreach (var cmd in module.Commands)
                {
                    string add = $"{prefix}{cmd.Aliases.First()} {cmd.Remarks}\n";
                    var result = await cmd.CheckPreconditionsAsync(Context);

                    if (result.IsSuccess)
                    {
                        description += $"`{prefix}{cmd.Aliases.First()} {cmd.Remarks}`\n";
                        summary = cmd.Summary;
                    }
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = module.Name;
                        x.Value = $"_{module.Summary}_\n";
                        x.Value += description + "\n";
                        x.IsInline = false;
                    });
                }
            }

            // To send to channel:
            //await ReplyAsync("", false, builder.Build());
            // To send to DM:
            var dmchannel = await Context.User.CreateDMChannelAsync();

            await Context.Message.DeleteAsync();

            await dmchannel.SendMessageAsync("", false, builder.Build());
        }

        [Command, Remarks("<command>"), Summary("!help help")]
        public async Task HelpAsync(string command)
        {
            var result = service.Search(Context, command);

            if (!result.IsSuccess)
            {
                await ReplyAsync($"{Context.User.Mention} I'm sorry, but I couldn't find a command like **{command}**.");
                return;
            }

            var builder = new EmbedBuilder()
            {
                Title = $"Here are some commands like **{command}**:",
                Color = Color.Purple,
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Requested by {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };

            foreach (var match in result.Commands)
            {
                var cmd = match.Command;

                builder.AddField(x =>
                {
                    x.Name = string.Join(", ", cmd.Aliases);
                    x.Value = $"Usage: {cmd.Remarks}\n" +
                              $"Example: {cmd.Summary}";
                    x.IsInline = false;
                });
            }

            await ReplyAsync("", false, builder.Build());
        }
    }
}
