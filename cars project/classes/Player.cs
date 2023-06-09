using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;

namespace cars_project.classes
{
    public class Player
    {
        // fields
        private Texture2D texture1;
        private Texture2D texture2;
        private Texture2D texture3;
        private Vector2 position;
        private float speed1;
        private float speed2;
        private float speed3;


        // data
        int score = 0;
        private int health = 10;

        private Rectangle collision;

        // weapon

        // time
        private int time = 0;
        private int maxTime = 30;

        // events
        public event Action<int> TakeDamage;
        public event Action<int> ScoreUpdated;

        // properties

        public int Score { get { return score; } }

        public int Health { get { return health; } }

        public Vector2 Position { get { return position; } }

        public Rectangle Collision { get { return collision; } }
        public static int playertype = 1;


        // constructor
        public Player()
        {

            texture1 = null;
            texture2 = null;
            texture3 = null;
            position = new Vector2(50, 50);
            speed1 = 3;
            speed2 = 5;
            speed3 = 8;
        }

        // methods
        public void LoadContent(ContentManager content)
        {
            texture1 = content.Load<Texture2D>("DefaultCar");
            texture2 = content.Load<Texture2D>("shrek");
            texture3 = content.Load<Texture2D>("GOD");
        }

        public void Update(ContentManager content, GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            #region Movement
            if (keyboardState.IsKeyDown(Keys.D)&&playertype==1)
            {
                position.X += speed1;
            }

            if (keyboardState.IsKeyDown(Keys.A) && playertype == 1)
            {
                position.X -= speed1;
            }
            if (keyboardState.IsKeyDown(Keys.W) && playertype == 1)
            {
                position.Y -= speed1;
            }

            if (keyboardState.IsKeyDown(Keys.S) && playertype == 1)
            {
                position.Y += speed1;
            }
            if (keyboardState.IsKeyDown(Keys.LeftShift)&& playertype == 1 && speed1 < 15)
            {
                speed1 += 0.05F;
            }
            if (keyboardState.IsKeyDown(Keys.LeftControl)&& playertype == 1&&speed1 >=0)
            {
                speed1 -= 0.03F;
            }
            if (keyboardState.IsKeyDown(Keys.D) && playertype == 2)
            {
                position.X += speed2;
            }

            if (keyboardState.IsKeyDown(Keys.A) && playertype == 2)
            {
                position.X -= speed2;
            }
            if (keyboardState.IsKeyDown(Keys.W) && playertype == 2)
            {
                position.Y -= speed2;
            }

            if (keyboardState.IsKeyDown(Keys.S) && playertype == 2)
            {
                position.Y += speed2;
            }
            if (keyboardState.IsKeyDown(Keys.LeftShift) && playertype == 2 && speed2 < 25)
            {
                speed2 += 0.1F;
            }
            if (keyboardState.IsKeyDown(Keys.LeftControl) && playertype == 2 && speed2 >= 0)
            {
                speed1 -= 0.08F;
            }
            if (keyboardState.IsKeyDown(Keys.D) && playertype == 3)
            {
                position.X += speed3;
            }

            if (keyboardState.IsKeyDown(Keys.A) && playertype == 3)
            {
                position.X -= speed3;
            }
            if (keyboardState.IsKeyDown(Keys.W) && playertype == 3)
            {
                position.Y -= speed3;
            }

            if (keyboardState.IsKeyDown(Keys.S) && playertype == 3)
            {
                position.Y += speed3;
            }
            if (keyboardState.IsKeyDown(Keys.LeftShift) && playertype == 3 && speed3 < 100)
            {
                speed1 += 0.5F;
            }
            if (keyboardState.IsKeyDown(Keys.LeftControl) && playertype == 3 && speed3 >= 0)
            {
                speed1 -= 0.2F;
            }
            #endregion

            #region Bounds
            if (position.X < 0)
            {
                position.X = 0;
            }

            if (position.Y < 0)
            {
                position.Y = 0;
            }

            if (position.X + texture1.Width > 700)
            {
                position.X = 700 - texture1.Width;
            }
            if (position.X + texture1.Width < 200)
            {
                position.X = 200 - texture1.Width;
            }

            if (position.Y + texture1.Height > 600)
            {
                position.Y = 600 - texture1.Height;
            }
            #endregion

            // collision
            collision = new Rectangle((int)position.X, (int)position.Y,
                texture3.Width, texture3.Height);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if(playertype==1)
            {
                spriteBatch.Draw(texture1, position, Color.White);
            }
            if (playertype == 2)
            {
                spriteBatch.Draw(texture2, position, Color.White);
            }
            if (playertype == 3)
            {
                spriteBatch.Draw(texture3, position, Color.White);
            }

        }

        // buisness logic

        public void Damage()
        {
            health--;

            if (TakeDamage != null)
            {
                TakeDamage(health);
            }
        }

        public void AddScore()
        {
            score++;

            if (ScoreUpdated != null)
            {
                ScoreUpdated(score);
            }
        }

        public void Reset()
        {
            position = new Vector2(350, 400);
            health = 10;
        }

    }
}