using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeatTheBoss.UI.Containers
{
    class MainMenuContainer : Container
    {
        public MainMenuContainer()
        {
            boundingBox = new Rectangle(394, 100, 492, 520);
            components = new List<Component>();
            color = new Color(58, 64, 73) * 0.8f;
        }

        public override void Update(GameTime gameTime)
        {
            if(components.Count == 0)
            {
                components.Add(new Components.Label("Beat The Boss", new Rectangle(0, 0, 492, 150), this, Color.White, TextureManager.fontBold30));
                components.Add(new Models.StartGame(new Rectangle(0,150,492,80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 77, 207, 60), Color.White, this, "Start game", TextureManager.fontRegular12, Color.Black));
                components.Add(new Components.Button(new Rectangle(0, 230, 492, 80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 77, 207, 60), Color.White, this, "Options", TextureManager.fontRegular12, Color.Black));
                components.Add(new Components.Button(new Rectangle(0, 310, 492, 80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 77, 207, 60), Color.White, this, "About", TextureManager.fontRegular12, Color.Black));
                components.Add(new Models.QuitButton(new Rectangle(0, 390, 492, 80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 77, 207, 60), Color.White, this, "Quit", TextureManager.fontRegular12, Color.Black));
            }

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
    }
}
