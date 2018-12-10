using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace BeatTheBoss
{
    class SoundManager
    {
        public static SoundEffect swordSound;

        public static Song basicLevelSong;
        public static Song mainMenuSong;

        public static Random rnd;

        private static List<SoundEffect> walking;
        private static int stepIndex = 0;

        public static void LoadContent(ContentManager manager)
        {
            swordSound = manager.Load<SoundEffect>("Sounds\\melee_sound");

            basicLevelSong = manager.Load<Song>("Sounds\\Dungeon");
            mainMenuSong = manager.Load<Song>("Sounds\\Opening");

            walking = new List<SoundEffect>();
            walking.Add(manager.Load<SoundEffect>("Sounds\\stepstone_1"));
            walking.Add(manager.Load<SoundEffect>("Sounds\\stepstone_2"));
            walking.Add(manager.Load<SoundEffect>("Sounds\\stepstone_3"));
            walking.Add(manager.Load<SoundEffect>("Sounds\\stepstone_4"));
            walking.Add(manager.Load<SoundEffect>("Sounds\\stepstone_5"));
            walking.Add(manager.Load<SoundEffect>("Sounds\\stepstone_6"));
            walking.Add(manager.Load<SoundEffect>("Sounds\\stepstone_7"));
            walking.Add(manager.Load<SoundEffect>("Sounds\\stepstone_8"));

            rnd = new Random();
        }

        public static void PlayWalkingSound()
        {
            walking[stepIndex++].Play();
            if (stepIndex >= walking.Count)
                stepIndex = 0;
        }
    }
}
