using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BeatTheBoss.Models
{
    class Player
    {
        public Rectangle spriteSource;

        public Vector2 position;
        public Vector2 direction;
        public Physics.BoxCollider collider;

        public float speed;
        public int dir; //For sprite direction

        public Models.Weapon weapon = null;

        private Rectangle[] spritePositions;
        private float timer = 0f;
        private int frame = 0;

        public Player(Rectangle spriteRect)
        {
            this.spriteSource = spriteRect;
            this.position = new Vector2(100, 200);
            this.spritePositions = new Rectangle[] { new Rectangle(510, 302, 59, 79), new Rectangle(574, 302, 59, 79), new Rectangle(638, 302, 59, 79), new Rectangle(702, 306, 59, 75) };
            this.direction = new Vector2(0, 0);
            this.speed = 0.2f;
            this.dir = 1;
            this.collider = new Physics.BoxCollider(spriteSource.Height, spriteSource.Width, false);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();

            if(state.IsKeyDown(Keys.W) || state.IsKeyDown(Keys.Up))
            {
                direction.Y = -1;
            }
            else if(state.IsKeyDown(Keys.S) || state.IsKeyDown(Keys.Down))
            {
                direction.Y = 1;
            }
            else
            {
                direction.Y = 0;
            }

            

            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                direction.X = 1;
                dir = 1;
            }
            else if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                direction.X = -1;
                dir = -1;
            }
            else
            {
                direction.X = 0;
            }            

            Vector2 temp_pos = Vector2.Add(Vector2.Multiply(Vector2.Multiply(direction, speed), gameTime.ElapsedGameTime.Milliseconds), position);
            position.X = Math.Max(20, temp_pos.X);
            position.X = Math.Min(1204, position.X);

            position.Y = Math.Max(20, temp_pos.Y);
            position.Y = Math.Min(566, position.Y);


            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(timer >= 250f)
            {
                frame++;
                if (frame >= spritePositions.Length)
                    frame = 0;
                timer = 0f;
                spriteSource = spritePositions[frame];

                if(direction.X != 0 || direction.Y != 0)
                    SoundManager.PlayWalkingSound();
            }


            if (weapon != null && state.IsKeyDown(Keys.Space) && !weapon.inattack)
            {
                SoundManager.swordSound.Play();
                weapon.attacking = true;
                weapon.inattack = true;
            }

            if(weapon != null)
            {
                weapon.Update(gameTime);
            }

            // Temporary for display purpose
            if(state.IsKeyDown(Keys.O))
            {
                if (this.weapon == null)
                    this.weapon = new Models.Weapons.Sword();
                else
                    this.weapon = null;
            }
        }
    }
}
