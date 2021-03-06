﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatTheBoss.Physics;
using Microsoft.Xna.Framework;

namespace BeatTheBoss.Models.Enemies
{
    class SmallTroll : Enemy
    {
        public Rectangle[] spriteLocations;
        private Player playerInstance;

        public float timer;
        public int frame;


        public SmallTroll(Vector2 position, Player playerInstance)
        {
            this.position = position;
            spriteSource = new Rectangle(1486, 86, 35, 39);
            spriteLocations = new Rectangle[] { new Rectangle(1486, 86, 35, 39), new Rectangle(1550, 90, 35, 35), new Rectangle(1614, 94, 35, 31), new Rectangle(1678, 90, 35, 35) };

            this.playerInstance = playerInstance;

            this.area = new Rectangle(0, 0, 35,35);

            this.direction = new Vector2(0, 0);
            this.dir = 1;
            this.speed = 0.15f;
            this.color = Color.White;

            this.hp = 100f;
            this.dmg = 5f;
            this.isAlive = true;

            this.timer = 0;
            this.frame = 0;
        }

        public override void Update(GameTime gameTime)
        {
            timeFromLastAttack += gameTime.ElapsedGameTime.Milliseconds;

            if (!isAlive)
            {
                return;
            }

            this.position = Vector2.Add(Vector2.Multiply(Vector2.Multiply(this.direction, this.speed), gameTime.ElapsedGameTime.Milliseconds), position);
            this.area.X = (int)position.X;
            this.area.Y = (int)position.Y;

            this.direction = Vector2.Normalize(Vector2.Subtract(playerInstance.area.Center.ToVector2(), this.area.Center.ToVector2()));

            if (float.IsNaN(this.direction.X))
                this.direction.X = 0;

            if (float.IsNaN(this.direction.Y))
                this.direction.Y = 0;

            if (this.direction.X >= 0)
            {
                this.dir = 1;
            }
            else
            {
                this.dir = -1;
            }


            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (timer >= 170f)
            {
                frame++;
                if (frame >= spriteLocations.Length)
                    frame = 0;
                timer = 0f;
                spriteSource = spriteLocations[frame];
            }

            if (isAlive && hp <= 0)
            {
                double chanceToDropFood = SoundManager.rnd.NextDouble();

                if (chanceToDropFood <= 0.3)
                {
                    float percentage = SoundManager.rnd.Next(20, 30) / 100f;
                    double howMuchFood = percentage * dmgDelt;

                    if (howMuchFood >= 1)
                    {
                        GameplayManager.self.CurrLevel.items.Add(new Food(position, (int)Math.Ceiling(howMuchFood)));
                    }
                }

                isAlive = false;
                this.spriteSource = TextureManager.skull;
                this.area = Rectangle.Empty;
            }
        }

        public override void ApplyCollision(BoxCollider other)
        {
            if (!isAlive || other.isTrigger)
            {
                return;
            }

            //TODO: apply damage
            if (other is Models.Player)
            {
                if(timeFromLastAttack > 1000)
                {
                    ((Player)other).TakeDamage(dmg);
                    dmgDelt += dmg;
                    timeFromLastAttack = 0;
                }
            }

            Vector2 directionToOther = Vector2.Normalize(Vector2.Subtract(other.area.Center.ToVector2(), this.area.Center.ToVector2()));

            if ((directionToOther.X < 0 && direction.X < 0) || (directionToOther.X > 0 && direction.X > 0))
                direction.X = 0;

            if ((directionToOther.Y < 0 && direction.Y < 0) || (directionToOther.Y > 0 && direction.Y > 0))
                direction.Y = 0;
        }

        public override void ApplyCollision(PollygonCollider other)
        {
            Vector2 directionToOther = Vector2.Normalize(Vector2.Subtract(other.center, this.area.Center.ToVector2()));

            if ((directionToOther.X < 0 && direction.X < 0) || (directionToOther.X > 0 && direction.X > 0))
                direction.X = 0;

            if ((directionToOther.Y < 0 && direction.Y < 0) || (directionToOther.Y > 0 && direction.Y > 0))
                direction.Y = 0;
        }

        public override float TakeDamage(float damage)
        {
            if (isAlive)
            {
                float dmgToDo;

                if (hp >= damage)
                    dmgToDo = damage;
                else
                    dmgToDo = hp;

                this.hp -= dmgToDo;
                Game1.self.bloodParticleEngine.GenerateNewParticles(new Vector2(position.X + spriteSource.Width / 2, position.Y + spriteSource.Height / 2), SoundManager.rnd.Next(30, 50));

                return dmgToDo;
            }
            return 0;
        }
    }
}
