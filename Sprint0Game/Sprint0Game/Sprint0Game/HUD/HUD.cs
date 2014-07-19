using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class HUD
    {
        private Texture2D Coin;
        private String World;
        private SpriteFont Font;
        private Game Game;
        public bool visible{get; set;}

        public HUD(Game game, String world)
        {
            this.Coin = SpriteHolder.SingleCoin;
            this.World = world;
            this.Font = SpriteHolder.HUDFont;
            this.Game = game;
            visible = true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                this.DrawPoints(spriteBatch);
                this.DrawCoins(spriteBatch);
                if (!IsPlayingHorde())
                {
                    this.DrawWorld(spriteBatch);
                    this.DrawTime(spriteBatch);
                }
            }
        }

        private bool IsPlayingHorde()
        {
            return this.Game.CurrentLevel is HordeLevel && !this.Game.MenuDisplaying;
        }

        private void DrawPoints(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.Font, "MARIO", HUDConfig.MARIOLoc, Color.White);
            spriteBatch.DrawString(this.Font, GameStats.Points.ToString("D7"),
                HUDConfig.PointsLoc, Color.White);
        }

        private void DrawCoins(SpriteBatch spriteBatch)
        {
            int coinWidth = this.Coin.Width;
            int height = this.Coin.Height;
            Rectangle sourceRectangle = new Rectangle(0, 0, coinWidth, height);
            Rectangle drawnRectangle = new Rectangle((int)HUDConfig.CoinImgLoc.X, (int)HUDConfig.CoinImgLoc.Y,
                coinWidth, height);

            spriteBatch.Draw(this.Coin, drawnRectangle, sourceRectangle, Color.White);
            spriteBatch.DrawString(this.Font, "x", HUDConfig.CoinXLoc, Color.White);
            spriteBatch.DrawString(this.Font, GameStats.Coins.ToString("D2"),
                HUDConfig.CoinCountLoc, Color.White);
        }

        private void DrawWorld(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.Font, "World", HUDConfig.WORLDLoc, Color.White);
            spriteBatch.DrawString(this.Font, this.World, CalculateWorldValueLoc(this.World), Color.White);
        }

        private Vector2 CalculateWorldValueLoc(String world)
        {
            int middleOfWorld = (int)HUDConfig.WORLDLoc.X + StringWidth("World")/2;
            return new Vector2(middleOfWorld - StringWidth(world) / 2, HUDConfig.WorldValueY);
        }

        private int StringWidth(String st)
        {
            return (int)this.Font.MeasureString(st).X;
        }

        private void DrawTime(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(this.Font, "TIME", HUDConfig.TIMELoc, Color.White);
            int time = this.Game.CurrentLevel != null ? (int)this.Game.CurrentLevel.Time : 0;
            spriteBatch.DrawString(this.Font, time.ToString(),
                HUDConfig.TimeValueLoc, Color.White);
        }
    }
}
