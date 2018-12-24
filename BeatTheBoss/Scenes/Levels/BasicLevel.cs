using BeatTheBoss.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheBoss.Scenes.Levels
{
    class BasicLevel : Level
    {
        bool isEscPressed;

        public BasicLevel(int roomNumber)
        {
            items = new List<object>();
            player = new Models.Player(TextureManager.KnightTexture);

            #region items.add static elements in this room, start updating at index 6
            items.Add( new Models.Room(TextureManager.background));
            items.Add( new Models.ColliderOnlyObject(0, 0, 1280, 20, false));
            items.Add( new Models.ColliderOnlyObject(0, 640, 1280, 80, false));
            items.Add( new Models.ColliderOnlyObject(0, 0, 20, 720, false));
            items.Add( new Models.ColliderOnlyObject(1260, 0, 20, 720, false));
            items.Add(player);
            #endregion

            int enemyCount = 2 + roomNumber / 4;

            for(int i = 0; i < enemyCount; i++)
            {
                int enemyType = SoundManager.rnd.Next(0, 3);
                int x = SoundManager.rnd.Next(120, 1160);
                int y = SoundManager.rnd.Next(120, 540);
                Vector2 enemyPosition = new Vector2(x,y);

                switch(enemyType)
                {
                    case 0:
                        items.Add(new Models.Enemies.SmallOgre(enemyPosition, player));
                        break;
                    case 1:
                        items.Add(new Models.Enemies.SmallTroll(enemyPosition, player));
                        break;
                    case 2:
                        items.Add(new Models.Enemies.Wizard(enemyPosition, player));
                        break;
                }
            }

            items.Add(new Models.Weapons.Sword(player));

            UIContainers = new Stack<UI.Container>();

            SoundManager.PlaySong(SoundManager.basicLevelSong);

            isEscPressed = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (UIContainers.Count > 0)
            {
                UIContainers.Peek().Update(gameTime);
                return;
            }

            player.Update(gameTime);

            for(int i = 6; i < items.Count; i++)
            {
                if(items[i] is Enemy)
                    ((Enemy)items[i]).Update(gameTime);
                else if(items[i] is Weapon)
                    ((Weapon)items[i]).Update(gameTime);
            }

            if (!isEscPressed && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                isEscPressed = true;
            }

            if(isEscPressed && Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                isEscPressed = false;
                UIContainers.Push(new UI.Containers.PauseContainer());
            }
        }

        public override void Unload()
        {
            MediaPlayer.Stop();
        }
    }
}
