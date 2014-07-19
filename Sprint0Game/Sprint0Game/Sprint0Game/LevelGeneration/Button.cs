using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint0Game
{
    public class Button
    {
        public Vector2 Position { get; set; }
        public Rectangle Bounds { get; set; }
        public String Text { get; set; }
        public Texture2D Image { get; set; }
        public bool Pressed { get; set; }
        public bool Visible { get; set; }
        private Color ActiveColor;
        private SpriteFont Font;
        private int Xoffset, Yoffset;
        private bool canBePressed;
        private int pressResetCount;
        private bool isTextButton;
        

        public Button(Vector2 StartingPosition, String text, SpriteFont font, bool visible)
        {
            this.Position = StartingPosition;
            this.Bounds = new Rectangle((int)Position.X, (int)Position.Y, SpriteHolder.DialogBox.Width, SpriteHolder.DialogBox.Height);
            this.Text = text;
            this.Pressed = false;
            this.ActiveColor = Color.White;
            this.Font = font;

            this.Xoffset = (SpriteHolder.DialogBox.Width - (int)Font.MeasureString(this.Text).X) / 2;
            this.Yoffset = (SpriteHolder.DialogBox.Height - (int)Font.MeasureString(this.Text).Y) / 2;
            isTextButton = true;

            pressResetCount = 0;
            canBePressed = true;
            this.Visible = visible;
        }

        public Button(Vector2 StartingPosition, Texture2D Image, bool visible)
        {
            this.Position = StartingPosition;
            this.Bounds = new Rectangle((int)Position.X, (int)Position.Y, 
                SpriteHolder.ImageBox.Width, SpriteHolder.ImageBox.Height);
            //this.Text = text;
            this.Image = Image;
            this.Pressed = false;
            this.ActiveColor = Color.White;
            //this.Font = font;

            this.Xoffset = (SpriteHolder.ImageBox.Width - this.Image.Width) / 2;
            this.Yoffset = (SpriteHolder.ImageBox.Height - this.Image.Height) / 2;

            isTextButton = false;

            pressResetCount = 0;
            canBePressed = true;
            this.Visible = visible;
        }

        public void Press()
        {
            Rectangle newRect = this.Bounds;
            if (Pressed)
            {
             
                newRect.Inflate(-10, -10);
                this.Bounds = newRect;
                Pressed = false;
            }
            else
            {
                newRect.Inflate(10, 10);
                this.Bounds = newRect;
                Pressed = true;
            }
            canBePressed = false;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (Visible)
            {
                Vector2 TextVector = new Vector2(this.Position.X + Xoffset, this.Position.Y + Yoffset);
                if (isTextButton)
                {
                    spriteBatch.Draw(SpriteHolder.DialogBox, Bounds, ActiveColor);
                    spriteBatch.DrawString(this.Font, this.Text, TextVector, Color.Black);
                }
                else
                {
                    spriteBatch.Draw(SpriteHolder.ImageBox, Bounds, ActiveColor);
                    spriteBatch.Draw(this.Image, new Rectangle((int)this.Position.X + Xoffset, (int)this.Position.Y + Yoffset, this.Image.Width, this.Image.Height), Color.White);
                }
            }
        }

        public void Update(MouseState mouse)
        {
            if (Visible)
            {
                Point MouseLoc = new Point(mouse.X, mouse.Y);

                if (this.Bounds.Contains(MouseLoc))
                    ActiveColor = Color.Yellow;

                else
                    ActiveColor = Color.White;

                if (this.Bounds.Contains(MouseLoc) && mouse.LeftButton == ButtonState.Pressed && canBePressed)
                {
                    this.Press();
                }

                if (!canBePressed)
                {
                    pressResetCount++;
                    if (pressResetCount == GeneratorConfig.pressResetTime)
                    {
                        canBePressed = true;
                        pressResetCount = 0;
                    }
                }
            }

        }
    }
}
