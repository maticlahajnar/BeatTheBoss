using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.UI.Containers
{
    class PauseContainer : Container
    {
        bool isEscPressed;
        public PauseContainer()
        {
            boundingBox = new Rectangle(394, 100, 492, 520);
            components = new List<Component>();
            color = new Color(58, 64, 73) * 0.8f;

            isEscPressed = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.pixel, boundingBox, color);
            foreach (UI.Component component in components)
            {
                component.Draw(spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (components.Count == 0)
            {
                components.Add(new Components.Label("Pause", new Rectangle(0, 0, 492, 150), this, Color.White, TextureManager.fontBold30));
                components.Add(new Models.ContinueButton(new Rectangle(0, 150, 492, 80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 77, 207, 60), Color.White, this, "Continue", TextureManager.fontRegular12, Color.Black));
                components.Add(new Models.OptionsButton(new Rectangle(0, 230, 492, 80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 77, 207, 60), Color.White, this, "Options", TextureManager.fontRegular12, Color.Black));
                components.Add(new Models.ToMainMenuButton(new Rectangle(0, 390, 492, 80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 77, 207, 60), Color.White, this, "Back to Main Menu", TextureManager.fontRegular12, Color.Black));
            }

            foreach (Component component in components)
                component.Update(gameTime);

            if (!isEscPressed && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                isEscPressed = true;
            }

            if (isEscPressed && Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                isEscPressed = false;
                GameplayManager.self.CurrLevel.UIContainers.Pop();
            }

        }
    }
}
