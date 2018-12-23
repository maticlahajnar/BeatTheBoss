using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BeatTheBoss.UI.Components
{
    class Label : Component
    {
        public string text;
        public Color color;
        public SpriteFont font;
        public Rectangle location;

        public Label(string text, Rectangle boundingBox, Container parentContainer, Color color, SpriteFont font)
        {
            this.text = text;
            this.boundingBox = boundingBox;
            this.parentContainer = parentContainer;
            this.color = color;
            this.font = font;
            this.location = boundingBox;

            Realign();
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, this.text, Vector2.Add(boundingBox.Location.ToVector2(), parentContainer.boundingBox.Location.ToVector2()), this.color);
        }

        public void Realign()
        {
            Vector2 size = font.MeasureString(text);
            Rectangle area = new Rectangle((int)boundingBox.X, (int)boundingBox.Y, (int)size.X, (int)size.Y);
            Vector2 move = Vector2.Subtract(location.Center.ToVector2(), area.Center.ToVector2());

            this.boundingBox.X += (int)move.X;
            this.boundingBox.Y += (int)move.Y;
        }
    }
}
