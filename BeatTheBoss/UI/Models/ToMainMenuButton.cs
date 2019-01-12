using BeatTheBoss.UI.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.UI.Models
{
    class ToMainMenuButton : Button
    {
        public ToMainMenuButton(Rectangle boundingBox, Texture2D texture, Rectangle spriteLocationNormal, Rectangle spriteLocationHover, Color color, Container parentContainer, string text, SpriteFont font, Color fontColor) : base(boundingBox, texture, spriteLocationNormal, spriteLocationHover, color, parentContainer, text, font, fontColor)
        {

        }

        public override void OnClick()
        {
            if(GameplayManager.self.CurrLevel.player.hp <= 0)
            {
                SettingsManager.isGameSaved = false;
                SettingsManager.savedGameRoomNumber = 0;
                SettingsManager.savedGameScore = 0;
                SettingsManager.savedPlayerHp = 200f;
            }
            else
            {
                if (GameplayManager.self.CurrLevel.allDead)
                {
                    SettingsManager.isGameSaved = true;
                    SettingsManager.savedGameRoomNumber = GameplayManager.self.currLevelNumber + 1;
                    SettingsManager.savedGameScore = GameplayManager.self.score;
                    SettingsManager.savedPlayerHp = GameplayManager.self.CurrLevel.player.hp;
                }
                else
                {
                    SettingsManager.isGameSaved = true;
                    SettingsManager.savedGameRoomNumber = GameplayManager.self.currLevelNumber;
                }
            }

            SettingsManager.SaveSettings();

            GameplayManager.self.CurrLevel = new Scenes.Levels.MainMenu();
        }
    }
}
