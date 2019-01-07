using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.UI.Containers
{
    class GameGui : Container
    {
        Components.Label foodLabel;

        public GameGui()
        {
            boundingBox = new Rectangle(0, 0, 1280, 720);
            components = new List<Component>();
            color = Color.White * 0f;
        }

        public override void Update(GameTime gameTime)
        {
            if (components.Count == 0)
            {
                foodLabel = new Components.Label("100", new Rectangle(1150, 666, 120, 32), this, Color.White, TextureManager.fontRegular30);
                components.Add(new Models.PauseButton(new Rectangle(1215, 20, 45, 45), TextureManager.uiSpriteSheet, new Rectangle(240, 4, 45, 45), new Rectangle(240, 4, 45, 45), Color.White, this, "II", TextureManager.fontRegular30, Color.Black));
                components.Add(new Components.Image(new Rectangle(1110, 658, 32, 42), TextureManager.uiSpriteSheet, new Rectangle(305, 5, 32, 42), Color.White, this));
                components.Add(new Components.Label("Room " + GameplayManager.self.currLevelNumber, new Rectangle(20, 20, 200, 40), this, Color.White, TextureManager.fontRegular30));
                components.Add(foodLabel);
            }

            foodLabel.text = ((int)GameplayManager.self.CurrLevel.player.hp).ToString();
            foodLabel.Realign();

            foreach (Component component in components)
                component.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.pixel, boundingBox, color);
            foreach (UI.Component component in components)
            {
                component.Draw(spriteBatch);
            }
        }

        public void AddLevelClearLabel(int lvlNumber)
        {
            components.Add(new Components.Label("Room " + lvlNumber + " Clear! Proceed to ladder!", new Rectangle(0, 100, 1280, 50), this, Color.White, TextureManager.fontRegular30));
        }
    }
}
