using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BeatTheBoss
{
    class TextureManager
    {
        public static Texture2D spriteSheet;
        public static Texture2D uiSpriteSheet;

        public static Texture2D pixel;

        public static Texture2D background;
        public static Rectangle KnightTexture;

        public static SpriteFont fontRegular12;
        public static SpriteFont fontRegular30;
        public static SpriteFont fontBold30;

        public static Texture2D cursorTexture;
        public static Rectangle skull;

        public static void LoadTextures(ContentManager manager)
        {
            spriteSheet = manager.Load<Texture2D>("Textures\\tiles");
            uiSpriteSheet = manager.Load<Texture2D>("Textures\\UI");
            background = manager.Load<Texture2D>("Textures\\dungeonroom");

            KnightTexture = new Rectangle(510, 302, 59, 79);

            fontRegular12 = manager.Load<SpriteFont>("Fonts\\Pixel12");
            fontRegular30 = manager.Load<SpriteFont>("Fonts\\Pixel30");
            fontBold30 = manager.Load<SpriteFont>("Fonts\\Pixel30b");

            cursorTexture = manager.Load<Texture2D>("Textures\\CursorTexture");

            skull = new Rectangle(914, 922, 23, 23);

            pixel = new Texture2D(Renderer.graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

        }


    }
}
