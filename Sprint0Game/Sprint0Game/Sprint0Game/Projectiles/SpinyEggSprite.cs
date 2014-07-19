
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class SpinyEggSprite : IAnimatedSprite
    {
        private Texture2D Texture;
        private int CurrentFrame, TotalFrames, FrameStepper = 0;
        private SpinyEgg SpinyEgg;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public SpinyEggSprite(SpinyEgg spinyEgg)
        {
            this.SpinyEgg = spinyEgg;
            this.Texture = SpriteHolder.SpinyEgg;
            this.CurrentFrame = SpriteHolder.SpinyEggStartFrame;
            this.TotalFrames = SpriteHolder.SpinyEggFrames;
            this.Width = SpriteHolder.SpinyEggWidth;
            this.Height = this.Texture.Height;
        }

        public void Update()
        {
            this.SpinyEgg.CurrentPosition = new Vector2(this.SpinyEgg.CurrentPosition.X + this.SpinyEgg.CurrentVelocity.X,
                this.SpinyEgg.CurrentPosition.Y + this.SpinyEgg.CurrentVelocity.Y);

            if (FrameStepper % ProjectileConfig.FrameStepperPeriod == 0)
                this.CurrentFrame++;
            if (this.CurrentFrame % this.TotalFrames == 0)
                this.CurrentFrame = SpriteHolder.SpinyEggStartFrame;
            FrameStepper++;
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(Width * CurrentFrame, 0, Width, Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.SpinyEgg.CurrentPosition.X - camera.CurrentPosition.X),
                (int)this.SpinyEgg.CurrentPosition.Y - this.Height, Width, Height);

            spriteBatch.Draw(Texture, drawnRectangle, sourceRectangle, Color.White);
        }

        public void SetDead()
        {
            this.Width = 0;
            this.Height = 0;
        }
    }
}