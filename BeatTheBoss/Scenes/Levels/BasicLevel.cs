﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Scenes.Levels
{
    class BasicLevel : Level
    {

        public BasicLevel()
        {
            items = new Object[2];
            items[0] = new Models.Room(TextureManager.background);
            items[1] = new Models.Player(TextureManager.KnightTexture);

            MediaPlayer.Play(SoundManager.basicLevelSong);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
        }

        public override void Update(GameTime gameTime)
        {
            ((Models.Player)items[1]).Update(gameTime);
        }

        public override void Unload()
        {
            MediaPlayer.Stop();
        }
    }
}