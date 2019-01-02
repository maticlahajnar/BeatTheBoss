using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatTheBoss.Physics;
using Microsoft.Xna.Framework;

namespace BeatTheBoss.Models.Enemies
{
    class SmallOgre : Enemy
    {
        public enum SmallOgreStates { Normal, Charge };

        public Rectangle[] spriteLocations;
        private Player playerInstance;

        public float frameTimer;
        public float frameLimitTime;
        public float stateTimer;
        public int frame;

        public SmallOgreStates currentState;


        public SmallOgre(Vector2 position, Player playerInstance)
        {
            this.position = position;
            spriteSource = new Rectangle(1486, 962, 43, 59);
            spriteLocations = new Rectangle[] { new Rectangle(1486, 962, 43, 59), new Rectangle(1550, 966, 43, 55), new Rectangle(1614, 970, 43, 51), new Rectangle(1678, 966, 43, 55) };

            this.playerInstance = playerInstance;

            this.area = new Rectangle(0, 0, 43, 55);

            this.direction = new Vector2(0, 0);
            this.dir = 1;
            this.speed = 0.15f;
            this.color = Color.White;

            this.hp = 100f;
            this.dmg = 8f;
            this.isAlive = true;

            this.frameTimer = 0;
            this.frameLimitTime = 170f;
            this.frame = 0;

            currentState = SmallOgreStates.Normal;
        }

        public override void ApplyCollision(BoxCollider other)
        {
            if(!isAlive)
            {
                return;
            }

            if (other is Models.Player)
            {
                if (timeFromLastAttack > 1000)
                {
                    ((Player)other).TakeDamage(dmg);
                    dmgDelt += dmg;
                    timeFromLastAttack = 0;
                }

                if (currentState == SmallOgreStates.Charge)
                {
                    currentState = SmallOgreStates.Normal;
                    speed = 0.15f;
                    frameLimitTime = 170f;
                    this.area = new Rectangle(0, 0, 43, 55);
                }
            } else if (currentState == SmallOgreStates.Charge)
            {
                return;
            }

            Vector2 directionToOther = Vector2.Normalize(Vector2.Subtract(other.area.Center.ToVector2(), this.area.Center.ToVector2()));

            if ((directionToOther.X < 0 && direction.X < 0) || (directionToOther.X > 0 && direction.X > 0))
                direction.X = 0;

            if ((directionToOther.Y < 0 && direction.Y < 0) || (directionToOther.Y > 0 && direction.Y > 0))
                direction.Y = 0;
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


            frameTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (frameTimer >= frameLimitTime)
            {
                frame++;
                if (frame >= spriteLocations.Length)
                    frame = 0;
                frameTimer = 0f;
                spriteSource = spriteLocations[frame];
            }


            stateTimer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (stateTimer >= 5000f && currentState == SmallOgreStates.Normal)
            {
                currentState = SmallOgreStates.Charge;
                speed = 0.4f;
                frameLimitTime = 80f;
                stateTimer = 0f;
            } else if (stateTimer >= 700f && currentState == SmallOgreStates.Charge)
            {
                currentState = SmallOgreStates.Normal;
                speed = 0.15f;
                frameLimitTime = 170f;
                stateTimer = 0f;
            }

            if (currentState == SmallOgreStates.Charge && speed >= 0.15f)
            {
                speed -= 0.10f * gameTime.ElapsedGameTime.Seconds;
            }

            if(isAlive && hp <= 0)
            {
                double chanceToDropFood = SoundManager.rnd.NextDouble();

                if (chanceToDropFood <= 0.8)
                {
                    float percentage = SoundManager.rnd.Next(20, 30) / 100f;
                    float howMuchFood = percentage * dmgDelt;

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

        public override void ApplyCollision(PollygonCollider other)
        {
            Vector2 directionToOther = Vector2.Normalize(Vector2.Subtract(other.center, this.area.Center.ToVector2()));

            if ((directionToOther.X < 0 && direction.X < 0) || (directionToOther.X > 0 && direction.X > 0))
                direction.X = 0;

            if ((directionToOther.Y < 0 && direction.Y < 0) || (directionToOther.Y > 0 && direction.Y > 0))
                direction.Y = 0;
        }

        public override void TakeDamage(float damage)
        {
            if (isAlive)
            {
                this.hp -= damage;
                Game1.self.bloodParticleEngine.GenerateNewParticles(new Vector2(position.X + spriteSource.Width / 2, position.Y + spriteSource.Height / 2), SoundManager.rnd.Next(30, 50));
            }
        }
    }
}
