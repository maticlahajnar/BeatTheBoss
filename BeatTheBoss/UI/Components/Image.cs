using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeatTheBoss.UI.Components
{
    class Image : Component
    {
        private Texture2D texture;
        public Rectangle spriteLocation;
        public Color color;

        public Image(Rectangle boundingBox, Texture2D texture, Rectangle spriteLocation, Color color, Container parentContainer)
        {
            this.boundingBox = boundingBox;
            this.texture = texture;
            this.spriteLocation = spriteLocation;
            this.color = color;
            this.parentContainer = parentContainer;

            Rectangle area = new Rectangle((int)boundingBox.X, (int)boundingBox.Y, (int)spriteLocation.Width, (int)spriteLocation.Height);
            Vector2 move = Vector2.Subtract(boundingBox.Center.ToVector2(), area.Center.ToVector2());

            this.boundingBox.X += (int)move.X;
            this.boundingBox.Y += (int)move.Y;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Vector2.Add(parentContainer.boundingBox.Location.ToVector2(), this.boundingBox.Location.ToVector2()), spriteLocation, color);
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
