using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeatTheBoss.UI.Containers
{
    class GameOverContainer : Container
    {

        public GameOverContainer()
        {
            boundingBox = new Rectangle(440, 220, 400, 280);
            components = new List<Component>();
            color = new Color(58, 64, 73) * 0.8f;
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
                components.Add(new Components.Label("Game Over", new Rectangle(0, 20, 400, 50), this, Color.White, TextureManager.fontRegular30));
                components.Add(new Components.Label("You died", new Rectangle(0, 70, 400, 20), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Components.Label("Score", new Rectangle(0, 120, 400, 20), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Components.Label(GameplayManager.self.score.ToString(), new Rectangle(0, 140, 400, 20), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Models.ToMainMenuButton(new Rectangle(0, 180, 400, 80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 77, 207, 60), Color.White, this, "Back to Main Menu", TextureManager.fontRegular12, Color.Black));
            }

            foreach (Component component in components)
                component.Update(gameTime);
        }
    }
}
