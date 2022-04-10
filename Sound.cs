using System;
using System.Media;
using System.Threading;

namespace CatHacks8
{
    class Sound
    {
        public void Run(int sound)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            if (sound == 1)                
                player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\yakety.wav";
            else
                player.SoundLocation = AppDomain.CurrentDomain.BaseDirectory + "\\default.wav";
            player.PlayLooping();
        }
    }
}