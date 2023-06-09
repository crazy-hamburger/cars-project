using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace cars_project.classes.Components
{
    public class HealthBar
    {
        private Texture2D texture;
        private Vector2 position;

        private int height;
        private int numParts;
        private int partWidth;

        public int NumParts
        {
            get
            {
                return numParts;
            }
            set
            {
                numParts = value;
            }
        }

        public Rectangle DestinationRectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y,
                    partWidth * numParts, height);
            }
        }

        public HealthBar(Vector2 position, int numParts, int width, int height)
        {
            this.position = position;

            this.height = height;
            this.numParts = numParts;

            partWidth = width / numParts;
        }

        public void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("healthbar");
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, DestinationRectangle, Color.White);
        }
    }
}