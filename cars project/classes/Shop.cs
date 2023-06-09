using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using cars_project.classes;
using cars_project.classes.Components;
using System.Threading.Tasks.Sources;
using System.Linq;

namespace cars_project.classes
{
    internal class Shop
    {
        int money = 0;
        private List<Label> buttonList = new List<Label>();
        private List<Label> CarList = new List<Label>();
        private Label notBought;
        private Label noMoney;
        private Label alreadyBought;
        private Label car1;
        private Label car2;
        private Label car3;
        private int selected;
        private int selectedCar;
        private KeyboardState keyboardState;        // нынешнее состояние клавиатуры
        private KeyboardState prevKeyboardState;    // предыдущее состояние клавиатуры

        private Vector2 position = new Vector2(400, 200);

        public Shop()
        {
            selected = 0;
            selectedCar = 0;
            StreamReader sr = new StreamReader("D:\\Money.txt");
            int prevMoney = int.Parse(sr.ReadLine());
            money = prevMoney + money;

            buttonList.Add(new Label("Use", position, Color.White));
            buttonList.Add(new Label("Buy", new Vector2(position.X, position.Y + 40),
            Color.White));
            buttonList.Add(new Label("Exit", new Vector2(position.X, position.Y + 80),
                Color.White));
            CarList.Add( new Label("standard car", new Vector2(200, 100), Color.White));
            CarList.Add(new Label("premium car", new Vector2(350, 100), Color.White));
            CarList.Add(new Label("business car", new Vector2(500, 100), Color.White));
        }

        public void LoadContent(ContentManager content)
        {
            foreach (var item in buttonList)
            {
                item.LoadContent(content);
            }
            foreach (var label in buttonList)
            {
                label.Position = new Vector2(label.Position.X - label.Width / 2,
                    label.Position.Y);
            }

            // magic line = position
            foreach (var item in CarList)
            {
                item.LoadContent(content);
            }
            foreach (var label in CarList)
            {
                label.Position = new Vector2(label.Position.X - label.Width / 2,
                    label.Position.Y);
            }

            // magic line = position
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
            if (prevKeyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyUp(Keys.D))
            {
                if (selectedCar < CarList.Count - 1)
                {
                    selectedCar++;
                }
            }
            // нажал клавишу W
            if (prevKeyboardState.IsKeyUp(Keys.A) && keyboardState.IsKeyDown(Keys.A))
            {
                if (selectedCar > 0)
                {
                    selectedCar--;
                }
            }

            prevKeyboardState = keyboardState;


            // Event Click
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                if (selected == 0)
                {
                    CheckBuy();
                }
                else if (selected == 1)
                {
                    Buy();
                }
                else if (selected == 2)
                {
                    Game1.gameMode = GameMode.Menu;
                }
            }
        }
        public void CheckBuy()
        {
            StreamReader sr = new StreamReader("D:\\PremiumCar.txt");
            int line1 = int.Parse(sr.ReadLine());
            sr.Close();
            StreamReader sr1 = new StreamReader("D:\\ProCar.txt");
            int line2 = int.Parse(sr1.ReadLine());
            sr.Close();
            if (selectedCar == 0)
            {
                Player.playertype = 1;
            }
            if (line1 == 1 && selectedCar == 1)
            {
                Player.playertype = 2;
            }
            if (line2 == 1 && selectedCar == 2)
            {
                Player.playertype = 3;
            }
        }
        public void Buy()
        {
            StreamReader sr = new StreamReader("D:\\PremiumCar.txt");
            int line1 = int.Parse(sr.ReadLine());
            sr.Close();
            StreamReader sr1 = new StreamReader("D:\\ProCar.txt");
            int line2 = int.Parse(sr1.ReadLine());
            sr.Close();
            if (line1 == 0 && selectedCar == 1 && money == 100)
            {
                money = money - 100;
                StreamWriter sw = new StreamWriter("D:\\PremiumCar.txt");
                sw.Write("1");
                sw.Close();

            }
            if (line2 == 0 && selectedCar == 1 && money == 500)
            {
                money = money - 500;
                StreamWriter sw = new StreamWriter("D:\\ProCar.txt");
                sw.Write("1");
                sw.Close();

            }
        }

        internal void Draw(SpriteBatch spriteBatch)
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
            for (int i = 0; i < CarList.Count; i++)
            {
                if (selectedCar == i)
                {
                    colorSelected = Color.Yellow;
                }
                else
                {
                    colorSelected = Color.White;
                }

                CarList[i].Color = colorSelected;
                CarList[i].Draw(spriteBatch);
            }
        }
    }
}
