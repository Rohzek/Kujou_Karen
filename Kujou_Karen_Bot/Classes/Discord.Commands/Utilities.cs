using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Kujou_Karen_Bot.Classes.Commands
{
    [Name("Utilities"), Summary("Small commands with a little utility")]
    public class Utilities : ModuleBase<SocketCommandContext>
    {
        [Command("ping", RunMode = RunMode.Async), Summary("!ping"), RequireContext(ContextType.Guild)]
        public async Task GetPing()
        {
            await ReplyAsync($"Pong! {(Context.Client as DiscordSocketClient).Latency}ms");
        }

        [Command("say", RunMode = RunMode.Async), Summary("!say I don't like this server."), Remarks("<message>"), RequireBotPermission(GuildPermission.ManageMessages)]
        public async Task Say(params string[] echo)
        {
            string output = "";

            foreach (string temp in echo)
            {
                output += temp + " ";
            }

            await Context.Message.DeleteAsync();

            await ReplyAsync(output);
        }
    }
}
