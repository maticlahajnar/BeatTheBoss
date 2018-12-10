﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace BeatTheBoss
{
    class TextureManager
    {
        public static Texture2D spriteSheet;

        public static Texture2D background;
        public static Rectangle KnightTexture;
        public static SpriteFont font;

        public static void LoadTextures(ContentManager manager)
        {
            spriteSheet = manager.Load<Texture2D>("Textures\\tiles");
            background = manager.Load<Texture2D>("Textures\\dungeonroom");

            KnightTexture = new Rectangle(510, 302, 59, 79);
            font = manager.Load<SpriteFont>("Textures\\Coordinates");


        }


    }
}