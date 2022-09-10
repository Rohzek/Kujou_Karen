using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kujou_Karen_Bot.Classes.Commands
{
    [Group("delete"), Alias("clear"), Name("Delete Messages"), Summary("Deletes messages. Caller and bot must have appropriate permissions."), RequireUserPermission(GuildPermission.Administrator), RequireBotPermission(ChannelPermission.ManageMessages)]
    public class PurgeMessages : ModuleBase<SocketCommandContext>
    {
        [Command(RunMode = RunMode.Async), Summary("!delete")]
        public async Task Delete()
        {
            await Remove(1);
        }

        [Command(RunMode = RunMode.Async), Remarks("[amount]"), Summary("!delete 10")]
        public async Task Delete(uint amount)
        {
            await Remove(amount);
        }

        [Command("all", RunMode = RunMode.Async), Summary("!delete all")]
        public async Task Clear()
        {
            await Remove(9999);
        }

        public async Task Remove(uint amount)
        {
            var messages = await Context.Channel.GetMessagesAsync((int)amount + 1).FlattenAsync();
            var filteredMessages = messages.Where(x => (DateTimeOffset.UtcNow - x.Timestamp).TotalDays <= 14);

            // Get the total amount of messages.
            var count = filteredMessages.Count();

            // Check if there are any messages to delete.
            if (count == 0)
            {
                await ReplyAsync("Nothing eligable for deletion.");
            }

            await (Context.Channel as ITextChannel).DeleteMessagesAsync(filteredMessages);

            const int delay = 2000;
            var m = await this.ReplyAsync($"Purge completed. _This message will be deleted in {delay / 1000} seconds._");
            await Task.Delay(delay);
            await m.DeleteAsync();
        }
    }
}
