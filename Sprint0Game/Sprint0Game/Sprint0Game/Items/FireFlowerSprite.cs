
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class FireFlowerSprite : IAnimatedSprite
    {
        private Texture2D Texture;
        private FireFlower FireFlower;
        private int CurrentFrame;
        private int PeriodCounter = 0;
        public int Width { get; private set; }
        public int Height { get; private set; }
        private int SpawningCounter = 0;

        public FireFlowerSprite(FireFlower fireFlower)
        {
            this.FireFlower = fireFlower;
            this.Texture = SpriteHolder.PowerUps;
            this.CurrentFrame = (int)SpriteHolder.FireFlowerStartCoordinates[0];

            this.Width = SpriteHolder.PowerUpWidth;
            this.Height = SpriteHolder.PowerUpHeight;
        }

        public void Update()
        {
            this.FireFlower.CurrentPosition = new Vector2(this.FireFlower.CurrentPosition.X, 
                this.FireFlower.CurrentPosition.Y + this.FireFlower.CurrentVelocity.Y);
            
            if (this.PeriodCounter % ItemConfig.FireFlowerFramePeriod == 0)
            {
                if (this.FireFlower.Spawning)
                {
                    SpawningCounter++;
                    if (SpawningCounter == ItemConfig.TotalSpawningFrames)
                        this.FireFlower.Spawning = false;
                }
                this.CurrentFrame = (++this.CurrentFrame) % SpriteHolder.FireFlowerFrames;
                this.PeriodCounter = 0;
            }
            this.PeriodCounter++;
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(Width * this.CurrentFrame, this.Height * 
                (int)SpriteHolder.FireFlowerStartCoordinates[1], this.Width, this.Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.FireFlower.CurrentPosition.X - camera.CurrentPosition.X), 
                (int)this.FireFlower.CurrentPosition.Y - this.Height, Width, Height);


            if (this.FireFlower.Spawning)
            {
                int SpawningHeight = this.SpawningCounter * this.Height / ItemConfig.TotalSpawningFrames;
                sourceRectangle = new Rectangle(Width * this.CurrentFrame, this.Height *
                    (int)SpriteHolder.FireFlowerStartCoordinates[1], this.Width, SpawningHeight);
                drawnRectangle = new Rectangle((int)(this.FireFlower.CurrentPosition.X - camera.CurrentPosition.X),
                    (int)this.FireFlower.CurrentPosition.Y + (1 - SpawningHeight), Width, SpawningHeight);
            }
            spriteBatch.Draw(Texture, drawnRectangle, sourceRectangle, Color.White);
        }

        public void SetCollected()
        {
            Width = 0;
            Height = 0;
        }
    }
}
