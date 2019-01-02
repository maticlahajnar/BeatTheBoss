using BeatTheBoss.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss
{
    class GameplayManager
    {
        public static GameplayManager self;

        public Level CurrLevel = null;
        public int currLevelNumber = 0;

        public GameplayManager()
        {
            self = this;
            CurrLevel = new Scenes.Levels.MainMenu();
        }

        public void Update(GameTime gameTime)
        {
            if (CurrLevel != null)
                CurrLevel.Update(gameTime);
        }

        public void StartGame()
        {
            CurrLevel = new Scenes.Levels.BasicLevel(++currLevelNumber);
        }

        public void NextLevel()
        {
            CurrLevel = new Scenes.Levels.BasicLevel(++currLevelNumber);
        }
    }
}
