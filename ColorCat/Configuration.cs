using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ColorCat
{
    public class Configuration
    {
        public List<ColorMapping> Mappings { get; set; }

        private static string DefaultLocation
        {
            get
            {
                var dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                var path = Path.Combine(dir, "ColorCat", "ColorCat.config");
                return path;
            }
        }

        public static Configuration Load()
        {
            var location = DefaultLocation;

            if (!File.Exists(location)) SaveDefaults();

            string configText;
            using (var r = new StreamReader(location))
            {
                configText = r.ReadToEnd();
            }

            var config = JsonConvert.DeserializeObject<Configuration>(configText);

            return config;
        }

        public static string SaveDefaults()
        {
            var conf = new Configuration
            {
                Mappings = new List<ColorMapping>
                {
                    new ColorMapping { IsRegex = false, IgnoreCase = true, Pattern = "fatal", Color = ConsoleColor.DarkRed },
                    new ColorMapping { IsRegex = false, IgnoreCase = true, Pattern = "error", Color = ConsoleColor.Red },
                    new ColorMapping { IsRegex = false, IgnoreCase = true, Pattern = "warn", Color = ConsoleColor.Yellow },
                    new ColorMapping { IsRegex = false, IgnoreCase = true, Pattern = "debug", Color = ConsoleColor.DarkGray },
                }
            };

            return conf.Save();
        }

        public string Save()
        {
            var location = DefaultLocation;

            var file = new FileInfo(location);
            if (!file.Directory.Exists) file.Directory.Create();

            var configText = JsonConvert.SerializeObject(this, Formatting.Indented);
            using (var w = new StreamWriter(location))
            {
                w.WriteLine(configText);
            }

            return configText;
        }
    }
}