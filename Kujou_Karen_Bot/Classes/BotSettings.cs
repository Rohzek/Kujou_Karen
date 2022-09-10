using Newtonsoft.Json;
using System;

namespace Kujou_Karen_Bot
{
    [Serializable]
    public class BotSettings
    {
        // Discord
        [JsonProperty("DiscordName")]
        public string DiscordName { get; set; }
        [JsonProperty("DiscordNumber")]
        public string DiscordNumber { get; set;}
        [JsonProperty("DiscordAppID")]
        public string DiscordAppID { get; set; }
        [JsonProperty("DiscordPublicKey")]
        public string DiscordPublicKey { get; set; }
        [JsonProperty("DiscordToken")]
        public string DiscordToken { get; set;}
        [JsonProperty("DiscordGame")]
        public string DiscordGame{ get; set;}

        // Twitch
        [JsonProperty("TwitchUser")]
        public string TwitchUser { get; set;}
        [JsonProperty("TwitchClientID")]
        public string TwitchClientID { get; set;}
        [JsonProperty("TwitchClientSecret")]
        public string TwitchClientSecret { get; set;}
        [JsonProperty("TwitchToken")]
        public string TwitchToken { get; set; }

        // Global
        [JsonProperty("Prefix")]
        public string Prefix { get; set; } = "!";
    }
}