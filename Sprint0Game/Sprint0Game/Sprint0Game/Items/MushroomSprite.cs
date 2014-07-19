
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class MushroomSprite : IAnimatedSprite
    {
        private Texture2D Texture;
        private Mushroom Mushroom;
        private int Row, Column;
        public int Width { get; private set; }
        public int Height { get; private set; }
        private int SpawningCounter;
        private int SpawnDelay;

        public MushroomSprite(Mushroom redMushroom, MushroomType type)
        {
            this.Mushroom = redMushroom;
            this.Texture = SpriteHolder.PowerUps;
            switch (type)
            {
                case MushroomType.Green:
                    this.Row = (int)SpriteHolder.GreenMushroomCoordinates[0];
                    this.Column = (int)SpriteHolder.GreenMushroomCoordinates[1];
                    break;
                case MushroomType.Red:
                    this.Row = (int)SpriteHolder.RedMushroomCoordinates[0];
                    this.Column = (int)SpriteHolder.RedMushroomCoordinates[1];
                    break;
                case MushroomType.Metal:
                    this.Row = (int)SpriteHolder.MetalMushroomCoordinates[0];
                    this.Column = (int)SpriteHolder.MetalMushroomCoordinates[1];
                    break;
            }

            this.Width = SpriteHolder.PowerUpWidth;
            this.Height = SpriteHolder.PowerUpHeight;

            this.SpawningCounter = 0;
            this.SpawnDelay = 0;
        }

        public void Update()
        {
            this.Mushroom.CurrentPosition = new Vector2(this.Mushroom.CurrentPosition.X + this.Mushroom.CurrentVelocity.X,
                this.Mushroom.CurrentPosition.Y + this.Mushroom.CurrentVelocity.Y);

            if (this.Mushroom.Spawning)
            {
                this.Mushroom.CurrentVelocity = new Vector2(0, 0);
                if (SpawnDelay % ItemConfig.FireFlowerFramePeriod == 0)
                {
                    SpawningCounter++;
                    if (SpawningCounter % ItemConfig.TotalSpawningFrames == 0)
                    {
                        this.Mushroom.CurrentVelocity = new Vector2(ItemConfig.MushroomVelocity, 0);
                        this.Mushroom.Spawning = false;
                    }
                }
                SpawnDelay++;
            }

        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(Row * Width,Column * Height, Width, Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.Mushroom.CurrentPosition.X - camera.CurrentPosition.X),
                (int)this.Mushroom.CurrentPosition.Y - this.Height, Width, Height);

            if (this.Mushroom.Spawning)
            {
                int SpawningHeight = this.SpawningCounter * this.Height / ItemConfig.TotalSpawningFrames;
                sourceRectangle = new Rectangle(Row * Width, Column * Height, Width, SpawningHeight);
                drawnRectangle = new Rectangle((int)(this.Mushroom.CurrentPosition.X - camera.CurrentPosition.X),
                    (int)this.Mushroom.CurrentPosition.Y + (1 - SpawningHeight), Width, SpawningHeight);
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
