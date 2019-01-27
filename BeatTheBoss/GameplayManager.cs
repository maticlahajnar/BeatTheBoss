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
        public int score = 0;

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

        public void StartGame(bool newGame)
        {
            if (!newGame)
            {
                currLevelNumber = SettingsManager.savedGameRoomNumber - 1;
                score = SettingsManager.savedGameScore;
            }

            if (++currLevelNumber % 5 == 0)
                CurrLevel = new Scenes.Levels.BossLevel(currLevelNumber);
            else
                CurrLevel = new Scenes.Levels.BasicLevel(currLevelNumber);

            if (!newGame)
            {
                CurrLevel.player.hp = SettingsManager.savedPlayerHp;
            } else
            {
                CurrLevel.player.Reset();
            }
        }

        public void TryNextLevel()
        {
            if (CurrLevel.allDead)
            {
                if(++currLevelNumber % 5 == 0)
                    CurrLevel = new Scenes.Levels.BossLevel(currLevelNumber);
                else
                    CurrLevel = new Scenes.Levels.BasicLevel(currLevelNumber);
                SettingsManager.savedGameScore = score;
                SettingsManager.savedPlayerHp = CurrLevel.player.hp;
                SettingsManager.savedGameRoomNumber = currLevelNumber;
            }
        }
    }
}
