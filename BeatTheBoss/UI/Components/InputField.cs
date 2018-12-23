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
    class InputField : Button
    {
        private bool isActive;
        StringBuilder textBuilder;

        public InputField(Rectangle boundingBox, Texture2D texture, Rectangle spriteLocationNormal, Rectangle spriteLocationHover, Color color, Container parentContainer, string text, SpriteFont font, Color fontColor) : base(boundingBox, texture, spriteLocationNormal, spriteLocationHover, color, parentContainer, text, font, fontColor)
        {
            isActive = false;
            Game1.self.Window.TextInput += UpdateText;
            textBuilder = new StringBuilder();
        }

        public override void OnClick()
        {
            base.OnClick();
            isActive = true;
        }

        public override void Update(GameTime gameTime)
        {
            if (isActive)
                this.image.color = Color.LightGray;
            else
                this.image.color = Color.White;

            base.Update(gameTime);

            Point mouseCoord = Vector2.Subtract(Mouse.GetState().Position.ToVector2(), this.parentContainer.boundingBox.Location.ToVector2()).ToPoint();

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (!this.boundingBox.Contains(mouseCoord))
                {
                    isActive = false;
                }
            }

            if (isActive && Keyboard.GetState().IsKeyDown(Keys.Enter))
                isActive = false;
        }

        public void UpdateText(object sender, TextInputEventArgs e)
        {
            if(isActive)
            {
                if (e.Character == '\b')
                {
                    if (textBuilder.Length > 0)
                        textBuilder.Remove(textBuilder.Length - 1, 1);
                }
                else
                {
                    if(textBuilder.Length < 13 && label.font.Characters.Contains(e.Character))
                        textBuilder.Append(e.Character);
                }
                this.label.text = textBuilder.ToString();
                label.Realign();
            }
            
        }


    }
}
