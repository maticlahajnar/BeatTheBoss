using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatTheBoss.Physics;
using Microsoft.Xna.Framework;

namespace BeatTheBoss.Models.Enemies
{
    class OgreBoss : Enemy
    {
        public enum OgreBossStates { Normal, Spawning };
        public OgreBossStates currentState;

        public Rectangle[] spriteLocations;
        private Player playerInstance;

        public float frameTimer;
        public float frameLimitTime;
        public float stateTimer;
        public int frame;
        bool spawnedEnemies = false;

        public OgreBoss(Vector2 position, Player playerInstance)
        {
            this.position = position;
            spriteSource = new Rectangle(86, 1302, 80, 104);
            spriteLocations = new Rectangle[] { new Rectangle(86, 1302, 80, 104), new Rectangle(214, 1302, 80, 104), new Rectangle(338, 1306, 88, 100), new Rectangle(470, 1306, 80, 100) };

            this.playerInstance = playerInstance;

            this.area = new Rectangle(0, 0, 80, 104);

            this.direction = new Vector2(0, 0);
            this.dir = 1;
            this.speed = 0.1f;
            this.color = Color.White;

            this.hp = 150f;
            this.dmg = 10f;
            this.isAlive = true;

            this.frameTimer = 0;
            this.frameLimitTime = 170f;
            this.frame = 0;
        }

        public override void ApplyCollision(BoxCollider other)
        {
            if (!isAlive || other.isTrigger)
            {
                return;
            }

            if (other is Models.Player)
            {
                float dmgToDo = dmg;

                if (timeFromLastAttack > 1000)
                {
                    ((Player)other).TakeDamage(dmgToDo);
                    dmgDelt += dmgToDo;
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

            if (stateTimer >= 10000f && currentState == OgreBossStates.Normal)
            {
                currentState = OgreBossStates.Spawning;
                speed = 0.0f;
                stateTimer = 0f;
                spawnedEnemies = false;
            }
            else if (stateTimer >= 500f && stateTimer < 1000f && currentState == OgreBossStates.Spawning)
            {
                if (!spawnedEnemies)
                {
                    int enemyCount = GameplayManager.self.currLevelNumber / 5;

                    for (int i = 0; i < enemyCount; i++)
                    {
                        int enemyType = SoundManager.rnd.Next(0, 3);
                        int x = SoundManager.rnd.Next(120, 1160);
                        int y = SoundManager.rnd.Next(120, 540);
                        Vector2 enemyPosition = new Vector2(x, y);

                        switch (enemyType)
                        {
                            case 0:
                                GameplayManager.self.CurrLevel.items.Add(new Models.Enemies.SmallOgre(enemyPosition, GameplayManager.self.CurrLevel.player));
                                break;
                            case 1:
                                GameplayManager.self.CurrLevel.items.Add(new Models.Enemies.SmallTroll(enemyPosition, GameplayManager.self.CurrLevel.player));
                                break;
                            case 2:
                                GameplayManager.self.CurrLevel.items.Add(new Models.Enemies.Wizard(enemyPosition, GameplayManager.self.CurrLevel.player));
                                break;
                        }
                    }
                    spawnedEnemies = true;
                }
            }
            else if (stateTimer >= 1000f && currentState == OgreBossStates.Spawning)
            {
                currentState = OgreBossStates.Normal;
                speed = 0.1f;
                stateTimer = 0f;
            }

            if (isAlive && hp <= 0)
            {
                double chanceToDropFood = SoundManager.rnd.NextDouble();

                if (chanceToDropFood <= 1)
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
    }
}
