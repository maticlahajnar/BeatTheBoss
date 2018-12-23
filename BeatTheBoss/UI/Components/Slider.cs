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
    class Slider : Component
    {
        public Button sliderButton;
        public Image sliderImage;

        private Rectangle spriteLocationImage;
        private Rectangle spriteLocationButton;

        public float value;

        public Slider(Rectangle boundingBox, Texture2D texture, Rectangle spriteLocationImage, Rectangle spriteLocationButton, Color color, Container parentContainer, float initialValue)
        {
            this.parentContainer = parentContainer;
            this.spriteLocationImage = spriteLocationImage;
            this.spriteLocationButton = spriteLocationButton;
            this.boundingBox = boundingBox;
            value = initialValue;

            this.sliderImage = new Image(boundingBox, texture, spriteLocationImage, Color.White, parentContainer);
            this.sliderButton = new Button(new Rectangle(getXFromValue(), boundingBox.Y - 1, spriteLocationButton.Width, spriteLocationButton.Height), texture, this.spriteLocationButton, this.spriteLocationImage, Color.White, parentContainer, "", TextureManager.fontRegular12, Color.White);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.sliderImage.Draw(spriteBatch);
            this.sliderButton.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            sliderButton.Update(gameTime);
            if(sliderButton.isClicked)
            {
                int newX = getMouseCoordInLimit();
                sliderButton.boundingBox.X = newX;
                sliderButton.image.boundingBox.X = newX;
                sliderButton.image.color = Color.LightGray;
                value = getValue();
            }
        }

        private int getMouseCoordInLimit()
        {
            Point mouseCoord = Vector2.Subtract(Mouse.GetState().Position.ToVector2(), this.parentContainer.boundingBox.Location.ToVector2()).ToPoint();
            int minmax;

            minmax = Math.Max(mouseCoord.X , this.boundingBox.X - spriteLocationButton.Width / 2);
            minmax = Math.Min(minmax, boundingBox.X + boundingBox.Width - spriteLocationButton.Width / 2);

            return minmax;

        }

        public float getValue()
        {
            int offset = this.boundingBox.X - spriteLocationButton.Width / 2;
            int max = boundingBox.Width;
            int currPos = sliderButton.boundingBox.X - offset;

            return currPos / (float)max;
        }

        public int getXFromValue()
        {
            return (int)(value * boundingBox.Width + this.boundingBox.X - spriteLocationButton.Width / 2);
        }
    }
}
