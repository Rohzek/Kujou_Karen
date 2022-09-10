namespace Kujou_Karen_Bot.Classes.Commands.Helpers
{
    public class Magic
    {
        public string question { get; set; }
        public string answer { get; set; }
        public string type { get; set; }
    }

    public class RootObject
    {
        public Magic magic { get; set; }
    }
}
