﻿using BeatTheBoss.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace BeatTheBoss.Models
{
    class Player : Physics.BoxCollider
    {
        public Rectangle spriteSource;

        public Vector2 position;
        public Vector2 direction;

        public float speed;
        public int dir; //For sprite direction

        public Models.Weapon weapon = null;

        private Rectangle[] spritePositions;
        private float timer = 0f;
        private int frame = 0;

        public Player(Rectangle spriteRect)
        {

            this.spriteSource = spriteRect;
            this.position = new Vector2(640, 360);
            this.spritePositions = new Rectangle[] { new Rectangle(510, 302, 59, 79), new Rectangle(574, 302, 59, 79), new Rectangle(638, 302, 59, 79), new Rectangle(702, 306, 59, 75) };
            this.direction = new Vector2(0, 0);
            this.speed = 0.2f;
            this.dir = 1;
            this.area = new Rectangle(0, 0, 59, 79);
        }

        public override void ApplyCollision(BoxCollider other)
        {
            if (other is Enemy)
                return;

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
            #endregion


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
