using System.IO;
using Pomsole.Core.IO;

namespace Pomsole.Core.Config
{
    public class ConfigManager
    {
        protected string FilePath;
        public AppConfig Config;

        private readonly IFileManager FileManager;

        public ConfigManager(IFileManager fileManager)
        {
            FileManager = fileManager;
        }

        public void Init(string filePath)
        {
            FilePath = filePath;
            Config = Load();
        }

        public void Save()
        {
            FileManager.Write(Config, FilePath);
        }

        protected AppConfig Load()
        {
            if (!File.Exists(FilePath))
                return new AppConfig();
            return FileManager.Read<AppConfig>(FilePath);
        }
    }
}
