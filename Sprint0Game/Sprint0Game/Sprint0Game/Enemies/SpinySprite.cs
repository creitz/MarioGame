
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class SpinySprite : IAnimatedSprite
    {
        private Texture2D Texture;
        private int TotalFrames;
        private int PeriodCounter = 0;
        private SpriteEffects Flip;
        private int CurrentFrame = 0;
        private Spiny Spiny;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public SpinySprite(Spiny spiny)
        {
            this.Spiny = spiny;
            this.CurrentFrame = SpriteHolder.SpinyStartFrame;
            this.TotalFrames = SpriteHolder.SpinyFrames;
            this.Texture = SpriteHolder.Spiny;

            this.Width = SpriteHolder.SpinyWidth;
            this.Height = this.Texture.Height;
        }

        public void Update()
        {
            this.Spiny.CurrentPosition = new Vector2(this.Spiny.CurrentPosition.X + this.Spiny.CurrentVelocity.X,
                 this.Spiny.CurrentPosition.Y + this.Spiny.CurrentVelocity.Y);
            if (!this.Spiny.IsDead)
            {
                if (this.Spiny.IsRightFacing)
                {
                    this.Flip = SpriteEffects.None;
                }
                else
                {
                    this.Flip = SpriteEffects.FlipHorizontally;
                }
            }
            else
            {
                this.Spiny.Fall();
            }

            if (PeriodCounter == EnemyConfig.SpinyFramePeriod)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame == this.TotalFrames)
                    this.CurrentFrame = SpriteHolder.SpinyStartFrame;
                this.PeriodCounter = 0;
            }
            this.PeriodCounter++;
        }

        public void Draw(SpriteBatch SpriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(this.Width * this.CurrentFrame, 0, this.Width, this.Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.Spiny.CurrentPosition.X - camera.CurrentPosition.X),
                (int)this.Spiny.CurrentPosition.Y - this.Height, Width, Height);

            SpriteBatch.Draw(this.Texture, drawnRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, this.Flip, 0);
        }

        public void SetDead()
        {
            this.Spiny.CurrentVelocity = new Vector2(0, EnemyConfig.DeathUpVelocity);
            this.Flip = SpriteEffects.FlipVertically;
            this.Spiny.IsDead = true;
        }
    }
}