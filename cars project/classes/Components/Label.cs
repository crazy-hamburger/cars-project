using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace cars_project.classes.Components
{
    internal class Label
    {
        private SpriteFont spriteFont;
        private Vector2 position;
        private Color color;
        private string text;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public Color Color
        {
            set { color = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public float Width
        {
            get { return spriteFont.MeasureString(text).X; }
        }

        public float Height
        {
            get { return spriteFont.MeasureString(text).Y; }
        }

        public Label(string text, Vector2 position, Color color)
        {

            spriteFont = null;

            this.color = color;
            this.position = position;
            this.text = text;
        }

        public void LoadContent(ContentManager manager)
        {
            spriteFont = manager.Load<SpriteFont>("GameFont");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, text, position, color);
        }
    }
}
