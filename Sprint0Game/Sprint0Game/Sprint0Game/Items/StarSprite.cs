
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class StarSprite : IAnimatedSprite
    {
        private Texture2D Texture;
        private Star Star;
        private int CurrentFrame, TotalFrames, PeriodCounter;
        public int Width { get; private set; }
        public int Height { get; private set; }
        private int SpawningCounter;

        public StarSprite(Star star)
        {
            this.Star = star;
            this.Texture = SpriteHolder.PowerUps;
            this.CurrentFrame = (int)SpriteHolder.StarCoordinates[0];
            this.TotalFrames = (int)SpriteHolder.StarCoordinates[0] + SpriteHolder.StarFrames;
            this.PeriodCounter = 1;
            this.Width = SpriteHolder.PowerUpWidth;
            this.Height = SpriteHolder.PowerUpHeight;

            SpawningCounter = 0;
        }

        public void Update()
        {
            if (!this.Star.Spawning)
            {
                this.Star.CurrentPosition = new Vector2(this.Star.CurrentPosition.X + this.Star.CurrentVelocity.X,
                    this.Star.CurrentPosition.Y + this.Star.CurrentVelocity.Y);
            }

            this.PeriodCounter++;
            if (this.PeriodCounter == ItemConfig.StarFramePeriod)
            {
                 if(this.Star.Spawning)
                 {
                     this.Star.CurrentVelocity = new Vector2(0, 0);
                    SpawningCounter++;
                    if (SpawningCounter % ItemConfig.TotalSpawningFrames == 0)
                    {
                        this.Star.CurrentVelocity = new Vector2(ItemConfig.StarVelocity,0);
                        this.Star.Spawning = false;
                    }
                }
                
                this.CurrentFrame = ++this.CurrentFrame % this.TotalFrames;
                this.PeriodCounter = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(this.CurrentFrame * Width, (int)SpriteHolder.StarCoordinates[1] * 
                Height, Width, Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.Star.CurrentPosition.X - camera.CurrentPosition.X), 
                (int)this.Star.CurrentPosition.Y - this.Height, Width, Height);

            if (this.Star.Spawning)
            {
                int spawningHeight = this.SpawningCounter * this.Height / ItemConfig.TotalSpawningFrames;
                sourceRectangle = new Rectangle(this.CurrentFrame * Width, (int)SpriteHolder.StarCoordinates[1] * 
                    Height, Width, spawningHeight);
                drawnRectangle = new Rectangle((int)(this.Star.CurrentPosition.X - camera.CurrentPosition.X),
                    (int)this.Star.CurrentPosition.Y - spawningHeight, Width, spawningHeight);
            }

            spriteBatch.Draw(Texture, drawnRectangle, sourceRectangle, Color.White);
        }

        public void SetCollected()
        {
            this.Width = 0;
            this.Height = 0;
        }
    }
}
