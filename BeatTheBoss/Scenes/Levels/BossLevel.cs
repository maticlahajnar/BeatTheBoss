﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatTheBoss.Models;
using BeatTheBoss.Models.Enemies;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BeatTheBoss.Scenes.Levels
{
    class BossLevel : Level
    {
        bool isEscPressed;
        UI.Container gameGui;
        int roomNumber;

        public BossLevel(int roomNumber)
        {
            this.roomNumber = roomNumber;
            items = new List<object>();
            player = new Player();
            room = new Room(TextureManager.bossroom_closed);
            allDead = false;

            items.Add(room);
            items.Add(new Models.ColliderOnlyObject(0, 0, 1280, 20, false));
            items.Add(new Models.ColliderOnlyObject(0, 640, 1280, 80, false));
            items.Add(new Models.ColliderOnlyObject(0, 0, 20, 720, false));
            items.Add(new Models.ColliderOnlyObject(1260, 0, 20, 720, false));
            items.Add(new WaterFountain(new Vector2(540, 32)));
            items.Add(new WaterFountain(new Vector2(691, 32)));
            items.Add(player);

            int x = SoundManager.rnd.Next(120, 1160);
            int y = SoundManager.rnd.Next(120, 540);
            Vector2 enemyPosition = new Vector2(x, y);
            items.Add(new OgreBoss(enemyPosition, player));

            int enemyCount = 2 + roomNumber / 5;

            for (int i = 0; i < enemyCount; i++)
            {
                int enemyType = SoundManager.rnd.Next(0, 3);
                x = SoundManager.rnd.Next(120, 1160);
                y = SoundManager.rnd.Next(120, 540);
                enemyPosition = new Vector2(x, y);

                switch (enemyType)
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
            gameGui = new UI.Containers.GameGui();
            UIContainers.Push(gameGui);
            UIContainers.Push(new UI.Containers.FadeIn());

            SoundManager.PlaySong(SoundManager.bossLevelSong);

            isEscPressed = false;

            if (player.hp < 200f)
            {
                float amountToGive = 200f - player.hp;
                amountToGive = SoundManager.rnd.Next(70, 100) * amountToGive / 100;
                amountToGive = Math.Min(amountToGive, 50);
                Vector2 pos = new Vector2(SoundManager.rnd.Next(40, 1240), SoundManager.rnd.Next(40, 680));
                items.Add(new Food(pos, (int)amountToGive));
            }
        }

        public override void Unload()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (UIContainers.Count > 0 && UIContainers.Peek() is UI.Containers.GameGui)
            {
                UIContainers.Peek().Update(gameTime);
            }
            else if (UIContainers.Count > 0)
            {
                UIContainers.Peek().Update(gameTime);
                return;
            }

            player.Update(gameTime);
            allDead = true;

            for (int i = 5; i < items.Count; i++)
            {
                if (items[i] is Enemy)
                {
                    ((Enemy)items[i]).Update(gameTime);
                    if (((Enemy)items[i]).isAlive)
                        allDead = false;

                }
                else if (items[i] is Weapon)
                    ((Weapon)items[i]).Update(gameTime);
                else if (items[i] is WaterFountain)
                    ((WaterFountain)items[i]).Update(gameTime);
            }



            if (!isEscPressed && Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                isEscPressed = true;
            }

            if (isEscPressed && Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                isEscPressed = false;
                UIContainers.Push(new UI.Containers.PauseContainer());
            }

            if (room.texture != TextureManager.bossroom_clear && allDead)
            {
                room.texture = TextureManager.bossroom_clear;
                ((UI.Containers.GameGui)gameGui).AddLevelClearLabel(roomNumber);
                items.Add(new Models.LadderTrigger());
            }

            if (player.hp <= 0)
            {
                player.hp = 0;
                player.spriteSource = TextureManager.skull;
                player.position.X += 30;
                player.position.Y += 40;
                UIContainers.Push(new UI.Containers.GameOverContainer());
            }
        }
    }
}
