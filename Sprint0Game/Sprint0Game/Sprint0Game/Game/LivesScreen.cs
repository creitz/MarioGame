using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;

namespace Sprint0Game
{
    public class LivesScreen
    {
        private SpriteFont Font;
        private Texture2D MarioImg;
        private Texture2D background;
        private int WindowWidth, WindowHeight;
 
        public LivesScreen(int windowWidth, int windowHeight)
        {
            this.WindowWidth = windowWidth;
            this.WindowHeight = windowHeight;
            this.Font = SpriteHolder.HUDFont;
            this.MarioImg = SpriteHolder.SmallMario;
            this.background = SpriteHolder.BlackBackground;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw the lives screen
            int width = SpriteHolder.SmallMarioWidth;
            int height = this.MarioImg.Height;
            int frame = SpriteHolder.IdleMarioFrame;
            Rectangle MarioSourceRectangle = new Rectangle(frame * width, 0, width, height);
            Rectangle MarioDrawnRectangle = new Rectangle(LivesScreenConfig.MarioX, LivesScreenConfig.MarioY, width, height);
            spriteBatch.Draw(background, new Rectangle(0, 0, this.WindowWidth, 
                this.WindowHeight), Color.White);
            spriteBatch.Draw(this.MarioImg, MarioDrawnRectangle, MarioSourceRectangle, Color.White);
            spriteBatch.DrawString(Font, "x " + GameStats.Lives, LivesScreenConfig.XLoc, Color.White);
        }
    }
}
