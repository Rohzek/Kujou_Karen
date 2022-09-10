using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace Kujou_Karen_Bot.Classes.Commands
{
    [Name("Emotes"), Summary("Commands to display various emotes")]
    public class Emotes : ModuleBase<SocketCommandContext>
    {
        [Command("piggy"), Alias("oink"), Summary("!piggy")]
        public async Task Piggy()
        {
            await Delete();

            await ReplyAsync("^∞^");
        }

        [Command("kitty"), Alias("meow"), Summary("!kitty")]
        public async Task Kitty()
        {
            await Delete();

            await ReplyAsync("=＾● ⋏ ●＾=");
        }

        [Command("seal"), Alias("arf"), Summary("!seal")]
        public async Task Seal()
        {
            await Delete();

            await ReplyAsync("^ᴥ^");
        }

        [Command("mouse"), Summary("!mouse")]
        public async Task Mouse()
        {
            await Delete();

            await ReplyAsync("٩ʕ◕౪◕ʔو");
        }

        [Command("lenny"), Summary("!lenny")]
        public async Task Lenny()
        {
            await Delete();

            await ReplyAsync("( ͡° ͜ʖ ͡°)");
        }

        [Command("shrug"), Alias("dunno"), Summary("!shrug")]
        public async Task Shrug()
        {
            await Delete();

            await ReplyAsync("¯\\_(ツ)_/¯");
        }

        [Command("disapprove"), Alias("disapproval", "why"), Summary("!disapprove")]
        public async Task Disapproval()
        {
            await Delete();

            await ReplyAsync("(ಠ_ಠ)");
        }

        [Command("poshdisapprove"), Alias("poshdisapproval", "poshwhy"), Summary("!disapprove")]
        public async Task PoshDisapproval()
        {
            await Delete();

            await ReplyAsync("(ಠ_ರೃ)");
        }

        [Command("tableflip"), Alias("flip", "table"), Summary("!tableflip")]
        public async Task TableFlip()
        {
            await Delete();

            await ReplyAsync("(╯°□°）╯︵ ┻━┻");
        }

        [Command("supertableflip"), Alias("superflip", "supertable"), Summary("!supertableflip")]
        public async Task SuperTableFlip()
        {
            await Delete();

            await ReplyAsync("┻━┻ ︵ ＼( °□° )／ ︵ ┻━┻");
        }

        [Command("tablefix"), Alias("fixtable"), Summary("!tablefix")]
        public async Task TableFix()
        {
            await Delete();

            await ReplyAsync("┬──┬ ¯\\_(ツ)");
        }

        [Command("highfive"), Summary("!highfive")]
        public async Task HighFive()
        {
            await Delete();

            await ReplyAsync("ヘ( ^o^)ノ＼(^_^ )");
        }

        [Command("shock"), Summary("!shock")]
        public async Task Shock()
        {
            await Delete();

            await ReplyAsync("( ☉_☉ )");
        }

        [Command("stare"), Summary("!stare")]
        public async Task Stare()
        {
            await Delete();

            await ReplyAsync("OwO");
        }

        [Command("dealwithit"), Alias("deal"), Summary("!dealwithit")]
        public async Task DealWithIt()
        {
            const int delay = 2000;

            await Delete();

            var a = await ReplyAsync("(•_•)");

            await Task.Delay(delay);
            await a.DeleteAsync();

            var b = await ReplyAsync("( •_•)>⌐■-■");

            await Task.Delay(delay);
            await b.DeleteAsync();

            await ReplyAsync("(⌐■_■)");
        }

        [Command("excited"), Summary("!excited")]
        public async Task Excitement()
        {
            await Delete();

            await ReplyAsync("Ｏ(≧▽≦)Ｏ");
        }

        [Command("kawaii"), Summary("!kawaii")]
        public async Task Kawaii()
        {
            await Delete();

            await ReplyAsync("≧◡≦");

        }
        [Command("happy"), Summary("!happy")]
        public async Task Happy()
        {
            await Delete();

            await ReplyAsync("●﹏●");
        }

        [Command("sad"), Summary("!sad")]
        public async Task Sad()
        {
            await Delete();

            await ReplyAsync("●︿●");
        }

        [Command("cry"), Summary("!cry")]
        public async Task Cry()
        {
            await Delete();

            await ReplyAsync("o(;△;)o");
        }

        [Command("happycry"), Alias("cryhappy"), Summary("!happycry")]
        public async Task SadHappy()
        {
            await Delete();

            await ReplyAsync("o(╥﹏╥)o");
        }

        [Command("bird"), Summary("!bird")]
        public async Task Bird()
        {
            await Delete();

            await ReplyAsync("╭∩╮(︶︿︶)╭∩╮");
        }

        [Command("fightme"), Summary("!fightme")]
        public async Task FightMe()
        {
            await Delete();

            await ReplyAsync("୧(๑•̀ᗝ•́)૭");
        }

        [Command("money"), Alias("dollar"), Summary("!money")]
        public async Task Money()
        {
            await Delete();

            await ReplyAsync("[̲̅$̲̅(̲̅5̲̅)̲̅$̲̅]");
        }

        public async Task Delete() 
        {
            await Context.Message.DeleteAsync();
        }
    }
}
