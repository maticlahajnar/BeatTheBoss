using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeatTheBoss.UI.Containers
{
    class NewOrContinue : Container
    {
        public NewOrContinue()
        {
            boundingBox = new Rectangle(394, 100, 492, 220);
            components = new List<Component>();
            color = new Color(58, 64, 73) * 0.8f;
        }

        public override void Update(GameTime gameTime)
        {
            if (components.Count == 0)
            {
                components.Add(new Components.Label("Start fresh game or continue?", new Rectangle(0, 0, 492, 30), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Models.StartGame(new Rectangle(0, 40, 492, 80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 77, 207, 60), Color.White, this, TextureManager.fontRegular12, Color.Black,1));
                components.Add(new Models.StartGame(new Rectangle(0, 120, 492, 80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 77, 207, 60), Color.White, this, TextureManager.fontRegular12, Color.Black,2));
                
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
