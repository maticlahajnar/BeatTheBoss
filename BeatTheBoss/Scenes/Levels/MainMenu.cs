﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Scenes.Levels
{
    class MainMenu : Level
    {
        public MainMenu()
        {
            items = new List<object>();
            player = null;
            items.Add(new Models.Room(TextureManager.background_closed));

            UIContainers = new Stack<UI.Container>();
            UIContainers.Push(new UI.Containers.MainMenuContainer());

            SoundManager.PlaySong(SoundManager.mainMenuSong);
        }

        public override void Update(GameTime gameTime)
        {
            if(UIContainers.Count > 0)
            {
                UIContainers.Peek().Update(gameTime);
            }
        }

        public override void Unload()
        {
            MediaPlayer.Stop();
        }
    }
}
