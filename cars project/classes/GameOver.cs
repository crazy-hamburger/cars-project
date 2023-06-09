using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using cars_project.classes;
using cars_project.classes.Components;
using cars_project;
using System.IO;

namespace cars_project.classes
{
    public class GameOver
    {
        private Label label;
        private Label lblScore;
        private Label lblInstructions;

        private KeyboardState keyboardState;        // нынешнее состояние клавиатуры
        private KeyboardState prevKeyboardState;    // предыдущее состояние клавиатуры

        public GameOver()
        {
            label = new Label("GAME OVER", new Vector2(350, 200), Color.White);
            lblScore = new Label(null, new Vector2(350, 250), Color.White);

            lblInstructions = new Label("Press Enter to continue!",
                new Vector2(350, 300), Color.Orange);
        }

        public void LoadContent(ContentManager manager)
        {
            label.LoadContent(manager);
            lblScore.LoadContent(manager);
            lblInstructions.LoadContent(manager);
        }

        public void Update()
        {
            keyboardState = Keyboard.GetState();

            if (prevKeyboardState.IsKeyDown(Keys.Enter) &&
                keyboardState.IsKeyUp(Keys.Enter))
            {
                Game1.gameMode = GameMode.Menu;
            }

            prevKeyboardState = keyboardState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            label.Draw(spriteBatch);
            lblScore.Draw(spriteBatch);
            lblInstructions.Draw(spriteBatch);
        }

        // БИЗНЕС ЛОГИКА
        public void SetScore(int score)
        {
            lblScore.Text = $"Score: {score}";
            StreamReader sr = new StreamReader("D:\\Money.txt");
            int prevscore = int.Parse(sr.ReadLine());
            sr.Close();
            StreamWriter sw = new StreamWriter("D:\\Money.txt");
            sw.WriteLine(score+prevscore);
            sw.Close();
        }
    }
}
