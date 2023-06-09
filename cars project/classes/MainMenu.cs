using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using cars_project.classes.Components;
using Microsoft.Xna.Framework.Media;
using System.Reflection.Metadata;

namespace cars_project.classes
{
    internal class MainMenu
    {
        private List<Label> buttonList = new List<Label>();
        private int selected;
        private KeyboardState keyboardState;        // нынешнее состояние клавиатуры
        private KeyboardState prevKeyboardState; // предыдущее состояние клавиатуры
        private Song menuSong;

        public event Action OnPlayingStarted;

        private Vector2 position = new Vector2(400, 200);

        public MainMenu()
        {
            selected = 0;

            buttonList.Add(new Label("Play", position, Color.White));
            buttonList.Add(new Label("Shop", new Vector2(position.X, position.Y + 40),
            Color.White));
            buttonList.Add(new Label("Exit", new Vector2(position.X, position.Y + 80),
                Color.White));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (var item in buttonList)
            {
                item.LoadContent(content);
            }

            // magic line = position

            foreach (var label in buttonList)
            {
                label.Position = new Vector2(label.Position.X - label.Width / 2,
                    label.Position.Y);
            }
            menuSong = content.Load<Song>("MenuMusic");
            MediaPlayer.Play(menuSong);
        }

        public void Update()
        {
            keyboardState = Keyboard.GetState();

            // отпустил клавишу S
            if (prevKeyboardState.IsKeyDown(Keys.S) && keyboardState.IsKeyUp(Keys.S))
            {
                if (selected < buttonList.Count - 1)
                {
                    selected++;
                }
            }
            // нажал клавишу W
            if (prevKeyboardState.IsKeyUp(Keys.W) && keyboardState.IsKeyDown(Keys.W))
            {
                if (selected > 0)
                {
                    selected--;
                }
            }

            prevKeyboardState = keyboardState;


            // Event Click
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                if (selected == 0)
                {
                    // Game1.gameMode = GameMode.Playing;

                    if (OnPlayingStarted != null)
                    {
                        OnPlayingStarted();
                    }
                }
                else if (selected == 1)
                {
                    Game1.gameMode = GameMode.Shop;
                }
                else if (selected == 2)
                {
                    Game1.gameMode = GameMode.Exit;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color colorSelected;
            for (int i = 0; i < buttonList.Count; i++)
            {
                if (selected == i)
                {
                    colorSelected = Color.Yellow;
                }
                else
                {
                    colorSelected = Color.White;
                }

                buttonList[i].Color = colorSelected;
                buttonList[i].Draw(spriteBatch);
            }
        }
    }
}
