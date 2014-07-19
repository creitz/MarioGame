
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class CoinSprite : IAnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        private int CoinSpriteRows;
        private int CoinSpriteColumns;
        private int CurrentFrame;
        private int TotalFrames;
        private int Count;
        private Coin Coin;
        private Vector2 InitialPosition;

        public CoinSprite(Coin coin, Vector2 initialPosition)
        {
            this.Coin = coin;
            this.CoinSpriteRows = SpriteHolder.CoinRows;
            this.CoinSpriteColumns = SpriteHolder.CoinColumns;
            this.Texture = SpriteHolder.Coin;
            this.InitialPosition = initialPosition;
            this.CurrentFrame = 0;
            this.TotalFrames = this.CoinSpriteRows * this.CoinSpriteColumns;

            this.Width = this.Texture.Width / this.CoinSpriteColumns;
            this.Height = Texture.Height / this.CoinSpriteRows;
        }

        public void Update()
        {
            if (this.Coin.Spawning)
            {
                this.Coin.CurrentPosition = new Vector2(this.Coin.CurrentPosition.X,
                    this.Coin.CurrentPosition.Y + ItemConfig.CoinSpawnVelocity);
                if (Math.Abs(this.Coin.CurrentPosition.Y - this.InitialPosition.Y) > ItemConfig.CoinSpawnHeight)
                {
                    this.SetCollected();
                    GameStats.Coins++;
                    this.Coin.Spawning = false;
                }
            }

            if (this.Count % ItemConfig.CoinFramePeriod == 0)
            {
                this.CurrentFrame++;
            }
            if (this.CurrentFrame == this.TotalFrames)
            {
                this.CurrentFrame = 0;
            }
            this.Count++;
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            int row = (int)((float)this.CurrentFrame / (float)this.CoinSpriteColumns);
            int column = this.CurrentFrame % this.CoinSpriteColumns;
            Rectangle sourceRectangle = new Rectangle(Width * column, Height * row, Width, Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.Coin.CurrentPosition.X - camera.CurrentPosition.X), 
                (int)this.Coin.CurrentPosition.Y - this.Height, Width, Height);

            spriteBatch.Draw(Texture, drawnRectangle, sourceRectangle, Color.White);
        }

        public void SetCollected()
        {
            this.Width = 0;
            this.Height = 0;
        }
    }
}
