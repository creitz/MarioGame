
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class LakituSprite : IAnimatedSprite
    {
        private Texture2D Texture;
        private int PeriodCounter = 0;
        private SpriteEffects Flip;
        private int CurrentFrame = 0;
        private Lakitu Lakitu;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public LakituSprite(Lakitu lakitu)
        {
            this.Lakitu = lakitu;
            this.CurrentFrame = SpriteHolder.LakituStartFrame;
            this.Texture = SpriteHolder.Lakitu;

            this.Width = SpriteHolder.LakituWidth;
            this.Height = this.Texture.Height;
        }

        public void Update()
        {
            this.Lakitu.CurrentPosition = new Vector2(this.Lakitu.CurrentPosition.X + this.Lakitu.CurrentVelocity.X,
                 this.Lakitu.CurrentPosition.Y + this.Lakitu.CurrentVelocity.Y);
            if (!this.Lakitu.IsDead)
            {
                if (this.Lakitu.IsRightFacing)
                {
                    this.Flip = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    this.Flip = SpriteEffects.None;
                }
            }

            if (PeriodCounter == EnemyConfig.LakituOutOfCloudPeriod && this.CurrentFrame == SpriteHolder.LakituStartFrame)
            {
                this.CurrentFrame++;
                this.PeriodCounter = 0;
            }
            else if (PeriodCounter == EnemyConfig.LakituInCloudPeriod && this.CurrentFrame == (SpriteHolder.LakituStartFrame + 1))
            {
                this.CurrentFrame = SpriteHolder.LakituStartFrame;
                this.Lakitu.ShouldThrowEgg = true;
                this.PeriodCounter = 0;
            }
            this.PeriodCounter++;
        }

        public void Draw(SpriteBatch SpriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(this.Width * this.CurrentFrame, 0, this.Width, this.Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.Lakitu.CurrentPosition.X - camera.CurrentPosition.X),
                (int)this.Lakitu.CurrentPosition.Y - this.Height, Width, Height);

            SpriteBatch.Draw(this.Texture, drawnRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, this.Flip, 0);
        }

        public void SetDead()
        {
            this.Lakitu.CurrentVelocity = new Vector2(0, EnemyConfig.DeathUpVelocity);
            this.Flip = SpriteEffects.FlipVertically;
            this.Lakitu.IsDead = true;
        }
    }
}