using System;
using System.Media;
using Pomsole.Core.Audio;
using Pomsole.Core.Config;

namespace Pomsole
{
    public class AudioPlayer : IAudioPlayer
    {
        protected SoundPlayer player;

        protected readonly ConfigManager ConfigManager;

        public AudioPlayer(ConfigManager configManager)
        {
            ConfigManager = configManager;
            player = new SoundPlayer(ConfigManager.Config.AlertFilePath);
        }

        public void PlayAudio()
        {
            player.PlaySync();
        }
    }
}
