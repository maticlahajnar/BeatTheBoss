using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatTheBoss.Physics;
using Microsoft.Xna.Framework;

namespace BeatTheBoss.Models.Enemies
{
    class Wizard : Enemy
    {
        public enum WizardStates { Normal, PhaseOut, PhaseIn };

        public Rectangle[] spriteLocations;
        private Player playerInstance;

        public float frameTimer;
        public float frameLimitTime;
        public float stateTimer;
        public int frame;

        public float alpha;
        public WizardStates currentState;


        public Wizard(Vector2 position, Player playerInstance)
        {
            this.position = position;
            spriteSource = new Rectangle(1474, 1090, 51, 59);
            spriteLocations = new Rectangle[] { new Rectangle(1474, 1090, 51, 59), new Rectangle(1542, 1082, 47, 67), new Rectangle(1602, 1086, 51, 63), new Rectangle(1662, 1094, 55, 55) };

            this.playerInstance = playerInstance;

            this.area = new Rectangle(0, 0, 51, 61);

            this.direction = new Vector2(0, 0);
            this.dir = 1;
            this.speed = 0.15f;
            this.color = Color.White;
            this.alpha = 1f;

            this.hp = 100f;
            this.dmg = 10f;
            timeFromLastAttack = 1000000;
            this.isAlive = true;

            this.frameTimer = 0;
            this.frameLimitTime = 170f;
            this.frame = 0;

            currentState = WizardStates.Normal;
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
                if (timeFromLastAttack > 1000)
                {
                    ((Player)other).TakeDamage(dmg);
                    dmgDelt += dmg;
                    timeFromLastAttack = 0;
                }

                stateTimer = 0f;
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

            switch (currentState)
            {
                case WizardStates.Normal:
                    NormalStateUpdate(gameTime);
                    break;

                case WizardStates.PhaseIn:
                    PhaseInUpdate(gameTime);
                    break;

                case WizardStates.PhaseOut:
                    PhaseOutUpdate(gameTime);
                    break;
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

            if(isAlive && hp <= 0)
            {
                double chanceToDropFood = SoundManager.rnd.NextDouble();

                if(chanceToDropFood <= 0.3f)
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

        private void PhaseInUpdate(GameTime gameTime)
        {
            color = Color.White * alpha;
            alpha += 0.05f;

            if(alpha > 1f)
            {
                alpha = 1f;
                currentState = WizardStates.Normal;
                stateTimer = 0f;
                speed = 0.15f;
            }
        }

        private void NormalStateUpdate(GameTime gameTime)
        {
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

            if(stateTimer >= 5000f)
            {
                speed = 0;
                direction = new Vector2(0, 0);
                currentState = WizardStates.PhaseOut;
                stateTimer = 0;
            }
        }

        private void PhaseOutUpdate(GameTime gameTime)
        {
            color = Color.White * alpha;
            alpha -= 0.05f;

            if(alpha < 0)
            {
                alpha = 0f;
                float distToPlayer = 0;
                do
                {
                    int x = SoundManager.rnd.Next(-250, +250);
                    int y = SoundManager.rnd.Next(-200, +200);

                    position = Vector2.Add(playerInstance.position, new Vector2(x, y));

                    position.X = Math.Min(1204 - area.Width - 2, position.X);
                    position.X = Math.Max(22, position.X);

                    position.Y = Math.Min(640 - area.Height - 2, position.Y);
                    position.Y = Math.Max(22, position.Y);

                    distToPlayer = Math.Abs(Vector2.Distance(position, playerInstance.position));

                } while (distToPlayer < 150);

                currentState = WizardStates.PhaseIn;
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
