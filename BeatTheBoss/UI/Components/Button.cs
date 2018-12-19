using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BeatTheBoss.UI.Components
{
    class Button : Component
    {
        private Texture2D texture;
        private Rectangle spriteLocationNormal;
        private Rectangle spriteLocationHover;
        private Color color;
        private string text;
        private SpriteFont font;

        private Image image;
        private Label label;

        bool isClicked;

        public Button(Rectangle boundingBox, Texture2D texture, Rectangle spriteLocationNormal, Rectangle spriteLocationHover, Color color, Container parentContainer, string text, SpriteFont font, Color fontColor)
        {
            this.boundingBox = boundingBox;
            this.texture = texture;
            this.spriteLocationNormal = spriteLocationNormal;
            this.spriteLocationHover = spriteLocationHover;
            this.color = color;
            this.parentContainer = parentContainer;
            this.text = text;
            this.font = font;
            isClicked = false;

            Rectangle area = new Rectangle(boundingBox.X, boundingBox.Y, spriteLocationNormal.Width, spriteLocationNormal.Height);
            Vector2 move = Vector2.Subtract(boundingBox.Center.ToVector2(), area.Center.ToVector2());

            this.boundingBox.X += (int)move.X;
            this.boundingBox.Y += (int)move.Y;
            this.boundingBox.Width = spriteLocationNormal.Width;
            this.boundingBox.Height = spriteLocationNormal.Height;

            this.image = new Image(this.boundingBox, this.texture, this.spriteLocationNormal, this.color, this.parentContainer);
            this.label = new Label(this.text, this.boundingBox, this.parentContainer, fontColor, this.font);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.image.Draw(spriteBatch);
            this.label.Draw(spriteBatch);

            this.image.color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            Point mouseCoord = Vector2.Subtract(Mouse.GetState().Position.ToVector2(), this.parentContainer.boundingBox.Location.ToVector2()).ToPoint();

            if(this.boundingBox.Contains(mouseCoord))
            {
                this.image.color = Color.LightGray;

                if(Mouse.GetState().LeftButton == ButtonState.Pressed && isClicked == false)
                {
                    OnClick();
                    isClicked = true;
                }

                if (Mouse.GetState().LeftButton == ButtonState.Released && isClicked == true)
                {
                    isClicked = false;
                }
            }
        }

        public virtual void OnClick()
        {
            System.Diagnostics.Debug.WriteLine("Button with text " + text + " has been clicked");
        }
    }
}
