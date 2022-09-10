using Discord.WebSocket;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kujou_Karen_Bot
{
    public class Conversions
    {
        public static string ChannelConvert(SocketMessage msg)
        {
            return msg.Channel.ToString();
        }

        public static string MessageConvert(SocketMessage msg)
        {
            return msg.Content;
        }

        public static string NameConvert(SocketMessage msg)
        {
            return $"{msg.Author.Username}#{msg.Author.Discriminator}";
        }

        public static string TimeConvert(SocketMessage msg)
        {
            return "[" + msg.Timestamp.ToLocalTime().Hour + ":" + msg.Timestamp.ToLocalTime().Minute + ":" + msg.Timestamp.ToLocalTime().Second + "]";
        }

        public static string JsonConvertBeautiful(string json)
        {
            JToken parsedJson = JToken.Parse(json);
            var beautified = parsedJson.ToString(Formatting.Indented);

            return beautified;
        }

        public static string JsonConvertMinified(string json)
        {
            JToken parsedJson = JToken.Parse(json);
            var minified = parsedJson.ToString(Formatting.None);

            return minified;
        }
    }
}