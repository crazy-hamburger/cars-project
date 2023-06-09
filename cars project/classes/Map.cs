using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Reflection.Metadata;

namespace cars_project.classes
{
    public class Map
    {
        private Vector2 position1;
        private Vector2 position2;
        private Texture2D texture;
        private float speed;

        public float Speed
        {
            set { speed = value; }
        }

        public Map()
        {
            texture = null;
            position1 = Vector2.Zero;  // 0,0
            position2 = new Vector2(0, -950);
            speed = 15;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("road");
        }

        public void Update()
        {
            position1.Y += speed;
            position2.Y += speed;

            if (position2.Y >= 0)
            {
                position1.Y = 0;
                position2.Y = -950;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position1, Color.White);
            spriteBatch.Draw(texture, position2, Color.White);
        }
    }
}