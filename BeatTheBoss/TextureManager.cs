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

        public static Texture2D background_closed;
        public static Texture2D background_clear;
        public static Texture2D bossroom_closed;
        public static Texture2D bossroom_clear;
        public static Rectangle KnightTexture;

        public static SpriteFont fontRegular12;
        public static SpriteFont fontRegular30;
        public static SpriteFont fontBold30;

        public static Texture2D cursorTexture;
        public static Rectangle skull;

        public static Rectangle[] bloodParticles;

        public static void LoadTextures(ContentManager manager)
        {
            spriteSheet = manager.Load<Texture2D>("Textures\\tiles");
            uiSpriteSheet = manager.Load<Texture2D>("Textures\\UI");
            background_closed = manager.Load<Texture2D>("Textures\\dungeonroom");
            background_clear = manager.Load<Texture2D>("Textures\\dungeonroom_clear");

            bossroom_closed = manager.Load<Texture2D>("Textures\\bossroom");
            bossroom_clear = manager.Load<Texture2D>("Textures\\bossroom_clear");

            KnightTexture = new Rectangle(510, 302, 59, 79);

            fontRegular12 = manager.Load<SpriteFont>("Fonts\\Pixel12");
            fontRegular30 = manager.Load<SpriteFont>("Fonts\\Pixel30");
            fontBold30 = manager.Load<SpriteFont>("Fonts\\Pixel30b");

            cursorTexture = manager.Load<Texture2D>("Textures\\CursorTexture");

            skull = new Rectangle(914, 922, 23, 23);

            pixel = new Texture2D(Renderer.graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            bloodParticles = new Rectangle[] { new Rectangle(105,1078, 8, 5), new Rectangle(118, 1077, 6, 7), new Rectangle(129, 1078, 8, 5), new Rectangle(141, 1077, 8, 8), new Rectangle(151, 1077, 9, 6), new Rectangle(164, 1077, 6, 7) };
        }


    }
}
