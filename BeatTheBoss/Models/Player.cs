using Microsoft.Xna.Framework;

namespace BeatTheBoss.Models
{
    class Player
    {
        public Rectangle spriteSource;
        public Vector2 position;

        public Player(Rectangle spriteRect)
        {
            this.spriteSource = spriteRect;
            this.position = new Vector2(100, 200);
        }
    }
}
