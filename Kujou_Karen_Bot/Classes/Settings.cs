using Newtonsoft.Json;
using System;
using System.IO;

namespace Kujou_Karen_Bot
{
    public static class Settings
    {
        public static string fileBot = "BotSettings.json", version = "0.0.2";

        public static BotSettings settings;

        public static void Load()
        {
            using (StreamReader reader = new StreamReader(fileBot))
            {
                string json = reader.ReadToEnd();
                settings = JsonConvert.DeserializeObject<BotSettings>(json);
                reader.Close();
            }
        }

        public static void Create()
        {
            settings = new BotSettings();
            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText(fileBot, json);
        }

        public static BotSettings GetSettings()
        {
            if (File.Exists(fileBot))
            {
                Load();
            }
            else
            {
                Create();
            }

            return settings;
        }
    }
}