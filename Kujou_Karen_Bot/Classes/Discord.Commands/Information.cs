using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace Kujou_Karen_Bot.Classes.Commands
{
    [Name("Information"), Summary("Information about the bot, or creator")]
    public class Information : ModuleBase<SocketCommandContext>
    {
        [Command("about", RunMode = RunMode.Async), Summary("!about")]
        public async Task About()
        {

            var builder = new EmbedBuilder()
            {
                Title = "Here's a little information about me:",
                Color = Color.Purple,
                ThumbnailUrl = (Context.Client.CurrentUser.GetAvatarUrl()),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Requested by {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };

            builder.AddField((greeting) =>
            {
                greeting.Name = "Hello!";
                greeting.IsInline = true;
                greeting.Value =
                    $"I am {Context.Client.CurrentUser.Username}#{Context.Client.CurrentUser.Discriminator}\nNice to meet you.";
            });

            builder.AddField((creator) =>
            {
                creator.Name = "Creator";
                creator.IsInline = true;
                creator.Value =
                    "I was created by Rohzek#3073.";
            });

            builder.AddField((props) =>
            {
                props.Name = "Properties";
                props.IsInline = true;
                props.Value =
                    "I was written in C# using the Discord.NET 1.8.0 API.\nFor more info look at my source code on [Github](https://github.com/Rohzek/Discord-Bot)." +
                    "\nI'm currently on version: " + Settings.version;
            });

            await ReplyAsync("", false, builder.Build());
        }

        [Command("simplefoodrebalance"), Alias("food", "foodrebalance", "foodinfo", "foodupdate", "foodrebalanceinfo", "foodrebalanceupdate"), Summary("!simplefoodrebalance")]
        public async Task InfoFood()
        {
            var builder = new EmbedBuilder()
            {
                Title = "Simple Food Rebalance",
                Color = Color.Purple,
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Requested by {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };

            builder.AddField(x =>
            {
                x.Name = $"More Information";
                x.Value = $"More information about the mod, and downloads, can be found here: https://minecraft.curseforge.com/projects/simple-food-rebalance";
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "Currently Supported Versions:";
                x.Value = $"None. This mod is still very much in a buggy early alpha state.";
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "How's Progress Going?:";
                x.Value = $"It's not. I'm stumped by a bug, currently.";
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "Future Versions:";
                x.Value = $"Soon:tm:";
                x.IsInline = false;
            });

            await ReplyAsync("", false, builder.Build());
        }

        [Command("simpledivegear"), Alias("divegear", "divegearinfo", "divegearupdate"), Summary("!simpledivegear")]
        public async Task InfoDiveGear()
        {
            var builder = new EmbedBuilder()
            {
                Title = "Simple Diving Gear",
                Color = Color.Purple,
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Requested by {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };

            builder.AddField(x =>
            {
                x.Name = $"More Information";
                x.Value = $"More information about the mod, and downloads, can be found here: https://minecraft.curseforge.com/projects/simple-diving-gear";
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "Currently Supported Versions:";
                x.Value = $"Minecraft 1.19.X+";
                x.IsInline = false;
            });

            await ReplyAsync("", false, builder.Build());
        }

        [Command("smithingtable"), Alias("smithing", "actuallyusefulsmithingtable"), Summary("!smithingtable")]
        public async Task InfoSmithingTable()
        {
            var builder = new EmbedBuilder()
            {
                Title = "Actually Useful Smithing Table",
                Color = Color.Purple,
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Requested by {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };

            builder.AddField(x =>
            {
                x.Name = $"More Information";
                x.Value = $"More information about the mod, and downloads, can be found here: https://www.curseforge.com/minecraft/mc-mods/actually-useful-smithing-table";
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "Currently Supported Versions:";
                x.Value = $"Minecraft 1.19.X+";
                x.IsInline = false;
            });

            await ReplyAsync("", false, builder.Build());
        }

        [Command("stonecutter"), Alias("actuallyusefulstonecutter"), Summary("!stonecutter")]
        public async Task InfoStonecutter()
        {
            var builder = new EmbedBuilder()
            {
                Title = "Actually Useful Stonecutter",
                Color = Color.Purple,
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Requested by {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                }
            };

            builder.AddField(x =>
            {
                x.Name = $"More Information";
                x.Value = $"More information about the mod, and downloads, can be found here: https://www.curseforge.com/minecraft/mc-mods/actually-useful-stonecutter";
                x.IsInline = false;
            });

            builder.AddField(x =>
            {
                x.Name = "Currently Supported Versions:";
                x.Value = $"Minecraft 1.19.X+";
                x.IsInline = false;
            });

            await ReplyAsync("", false, builder.Build());
        }
    }
}
