using BeatTheBoss.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BeatTheBoss.Models
{
    class Player : Physics.BoxCollider
    {
        public static Player self = null;

        public Rectangle spriteSource;

        public Vector2 position;
        public Vector2 direction;

        public float speed;
        public int dir; //For sprite direction

        public float hp;

        private Rectangle[] spritePositions;
        private float timer = 0f;
        private int frame = 0;

        public Player(Rectangle spriteRect)
        {
            if (self != null)
            {
                this.spriteSource = self.spriteSource;
                this.position = self.position;
                this.spritePositions = new Rectangle[] { new Rectangle(510, 302, 59, 79), new Rectangle(574, 302, 59, 79), new Rectangle(638, 302, 59, 79), new Rectangle(702, 306, 59, 75) };
                this.direction = new Vector2(0, 0);
                this.speed = self.speed;
                this.dir = 1;
                this.area = self.area;

                hp = self.hp;
            } else
            {
                this.spriteSource = spriteRect;
                this.position = new Vector2(640, 360);
                this.spritePositions = new Rectangle[] { new Rectangle(510, 302, 59, 79), new Rectangle(574, 302, 59, 79), new Rectangle(638, 302, 59, 79), new Rectangle(702, 306, 59, 75) };
                this.direction = new Vector2(0, 0);
                this.speed = 0.2f;
                this.dir = 1;
                this.area = new Rectangle(0, 0, 59, 79);

                hp = 200;

                self = this;
            }
        }

        public override void ApplyCollision(BoxCollider other)
        {
            if (other is Enemy)
                return;

            if(other is Food)
            {
                this.hp += ((Food)other).value;
                GameplayManager.self.CurrLevel.items.Remove((Food)other);
                System.Diagnostics.Debug.WriteLine("Hit food");
                return;
            }

            if (area.Left < other.area.Left && direction.X > 0)
                direction.X = 0;

            if (area.Right > other.area.Right && direction.X < 0)
                direction.X = 0;

            if (area.Top < other.area.Top && direction.Y > 0)
                direction.Y = 0;

            if (area.Bottom > other.area.Bottom && direction.Y < 0)
                direction.Y = 0;
        }

        public override void ApplyCollision(PollygonCollider other)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            position = Vector2.Add(Vector2.Multiply(Vector2.Multiply(direction, speed), gameTime.ElapsedGameTime.Milliseconds), position);

            area.X = (int)position.X;
            area.Y = (int)position.Y;

            

            KeyboardState state = Keyboard.GetState();

            #region YMovementDetection
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
            #endregion

            #region XMovementDetection
            if (state.IsKeyDown(Keys.D) || state.IsKeyDown(Keys.Right))
            {
                direction.X = 1;
            }
            else if (state.IsKeyDown(Keys.A) || state.IsKeyDown(Keys.Left))
            {
                direction.X = -1;
            }
            else
            {
                direction.X = 0;
            }
            #endregion

            Point mouseCoord = Mouse.GetState().Position;
            if(mouseCoord.X > position.X)
            {
                dir = 1;
            } else
            {
                dir = -1;
            }


            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(timer >= 250f)
            {
                frame++;
                if (frame >= spritePositions.Length)
                {
                    frame = 0;
                }
                timer = 0f;
                spriteSource = spritePositions[frame];

                if (frame % 2 == 0 && (direction.X != 0 || direction.Y != 0))
                    SoundManager.PlayWalkingSound();
            }
        }

        public void TakeDamage(float damage)
        {
            this.hp -= damage;
            System.Diagnostics.Debug.WriteLine(hp);
        }
    }
}
