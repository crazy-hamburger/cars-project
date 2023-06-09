using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace cars_project.classes
{
    public class Obstacle
    {
        private Texture2D texture;
        private Vector2 position;
        private bool isAlive = true;

        private Rectangle collision;

        public Rectangle Collision { get { return collision; } }

        public bool IsAlive { get { return isAlive; } set { isAlive = value; } }

        public int Width
        {
            get
            {
                return texture.Width;
            }
        }

        public int Height
        {
            get
            {
                return texture.Height;
            }
        }

        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
            }
        }

        public Obstacle()
        {
            texture = null;
            position = Vector2.Zero;
        }

        public Obstacle(Vector2 position)
        {
            texture = null;
            this.position = position;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }

        public void Update()
        {
            // move
            position.Y += 2;

            // update collision
            collision = new Rectangle((int)position.X, (int)position.Y,
                Width, Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
