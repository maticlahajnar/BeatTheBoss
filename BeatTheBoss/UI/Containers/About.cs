using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeatTheBoss.UI.Containers
{
    class About : Container
    {
        public About()
        {
            boundingBox = new Rectangle(290, 75, 700, 570);
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
                components.Add(new Components.Label("About", new Rectangle(0, 0, 700, 100), this, Color.White, TextureManager.fontBold30));

                components.Add(new Components.Label("Gameteam presents", new Rectangle(0, 100, 700, 20), this, Color.White, TextureManager.fontRegular12));

                components.Add(new Components.Label("Boss Run", new Rectangle(0, 130, 700, 20), this, Color.White, TextureManager.fontRegular12));

                components.Add(new Components.Label("Featuring Matic Lahajnar et al", new Rectangle(0, 160, 700, 20), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Components.Label("Done with Monogame framework", new Rectangle(0, 180, 700, 20), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Components.Label("Done within TINR course taught by Bojan Klemenc & Peter Peer", new Rectangle(0, 210, 700, 20), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Components.Label("Computer Vision Lab", new Rectangle(0, 230, 700, 20), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Components.Label("Faculty of Computer and Information Science", new Rectangle(0, 250, 700, 20), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Components.Label("University of Ljubljana", new Rectangle(0, 270, 700, 20), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Components.Label("Slovenia", new Rectangle(0, 310, 700, 20), this, Color.White, TextureManager.fontRegular12));
                components.Add(new Components.Label("2019", new Rectangle(0, 330, 700, 20), this, Color.White, TextureManager.fontRegular12));

                components.Add(new Models.BackButton(new Rectangle(0, 465, 700, 80), TextureManager.uiSpriteSheet, new Rectangle(4, 4, 206, 68), new Rectangle(4, 77, 207, 60), Color.White, this, "Back", TextureManager.fontRegular12, Color.Black));
            }

            foreach (Component component in components)
                component.Update(gameTime);

        }
    }
}
