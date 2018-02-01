using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Pomsole.Core.Audio;

namespace Pomsole
{
    public class AudioPlayer : IAudioPlayer
    {
        protected SoundPlayer player;

        public AudioPlayer()
        {
            player = new SoundPlayer(@"C:\Files\sounds\ShipBell.wav");
        }

        public void PlayAudio()
        {
            player.PlaySync();
        }
    }
}
