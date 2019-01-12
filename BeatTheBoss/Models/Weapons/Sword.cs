using BeatTheBoss.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Models.Weapons
{
    class Sword : Weapon
    {
        List<Enemy> enemiesHit;
        public Sword(Player playerInstance)
        {
            this.locationR = new Vector2(60, -30);
            this.locationL = new Vector2(-20, -30);

            this.locationForRendererR = new Vector2(60, 85);
            this.locationForRendererL = new Vector2(-20, 85);

            enemiesHit = new List<Enemy>();


            this.damage = 10f;

            this.spriteSource = new Rectangle(1171, 326, 22, 115);

            this.rotation = 0f;
            this.attacking = false;

            this.playerInstance = playerInstance;

            this.position = Vector2.Add(playerInstance.position, (playerInstance.dir == 1) ? locationR : locationL );
            this.Points = new Vector2[] { new Vector2(0,0), new Vector2(22,0), new Vector2(22,115), new Vector2(0, 115) };
            this.center = Vector2.Add(playerInstance.position, new Vector2((playerInstance.dir == 1) ? locationR.X / 2 : locationL.X / 2, (playerInstance.dir == 1) ? locationR.Y / 2 : locationL.X/2));
        }

        public override void ApplyCollision(PollygonCollider other)
        {
            // IF i ever implement enemies with pollygon colliders, implement here / now it throws error
        }

        public override void ApplyCollision(BoxCollider other)
        {
            if (other is Enemy && attacking)
            {
                if(!enemiesHit.Contains((Enemy)other))
                {
                    enemiesHit.Add((Enemy)other);
                    
                    GameplayManager.self.score += ((int)((Enemy)other).TakeDamage(damage)); ;
                }
            }
        }

        public override Vector2 GetActualPointPosition(int index)
        {
            Vector2 currpoint = Vector2.Add(position, Points[index]);
            Vector2 origin = Vector2.Add(position, Points[3]);

            float tempRotation = (playerInstance.dir == 1) ? -rotation : rotation;

            currpoint = Vector2.Transform(currpoint - origin, Matrix.CreateRotationZ(tempRotation)) + origin;
            return currpoint;
        }

        public override void Update(GameTime gameTime)
        {
            this.position = Vector2.Add(playerInstance.position, (playerInstance.dir == 1) ? locationR : locationL);
            //this.center = Vector2.Add(playerInstance.position, new Vector2((playerInstance.dir == 1) ? locationR.X / 2 : locationL.X / 2, (playerInstance.dir == 1) ? locationR.Y / 2 : locationL.X / 2));

            if (attacking == true)
            {
                rotation -= 0.2f;
                if (rotation < -2f)
                {
                    attacking = false;
                }
            }
            else
            {
                if (rotation >= -0.1f)
                {
                    inattack = false;
                    enemiesHit.Clear();
                }
                else
                {
                    rotation += 0.2f;
                }
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Space) && inattack == false)
            {
                inattack = true;
                attacking = true;
                SoundManager.PlaySound(SoundManager.swordSound);
            }

        }

        public override Vector2 getRenderPosition()
        {
            return Vector2.Add(playerInstance.position, (playerInstance.dir == 1) ? locationForRendererR : locationForRendererL);
        }
    }
}
