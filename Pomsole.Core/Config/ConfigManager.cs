using System.IO;
using Newtonsoft.Json;

namespace Pomsole.Core.Config
{
    public class ConfigManager
    {
        protected string FilePath;
        public AppConfig Config;

        public void Init(string filePath)
        {
            FilePath = filePath;
            Config = Load();
        }

        public void Save()
        {
            var serializer = new JsonSerializer();
            using (StreamWriter file = File.CreateText(FilePath))
                using(JsonTextWriter writer = new JsonTextWriter(file))
                   serializer.Serialize(writer, Config);
        }

        protected AppConfig Load()
        {
            if (!File.Exists(FilePath))
                return new AppConfig();
            var serializer = new JsonSerializer();
            using (var reader = new StreamReader(FilePath))
                using (var jsonReader = new JsonTextReader(reader))
                    return serializer.Deserialize<AppConfig>(jsonReader);
        }
    }
}
