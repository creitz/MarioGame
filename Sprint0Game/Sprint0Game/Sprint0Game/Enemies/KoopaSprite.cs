
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class KoopaSprite : IAnimatedSprite
    {
        private Texture2D Texture;
        private int CurrentFrame;
        private int LastFrame;
        private int PeriodCounter = 0;
        private SpriteEffects Flip;
        private Koopa Koopa;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public KoopaSprite(Koopa koopa)
        {
            this.Koopa = koopa;
            this.Texture = SpriteHolder.Koopa;
            this.CurrentFrame = SpriteHolder.WalkingKoopaStartFrame;
            this.LastFrame = SpriteHolder.WalkingKoopaStartFrame + SpriteHolder.WalkingKoopaFrames;
            this.Width = SpriteHolder.KoopaWidth;
            this.Height = this.Texture.Height;
        }

        public void Update()
        {
            this.Koopa.CurrentPosition = new Vector2(this.Koopa.CurrentPosition.X + this.Koopa.CurrentVelocity.X,
                 this.Koopa.CurrentPosition.Y + this.Koopa.CurrentVelocity.Y);
            if (!this.Koopa.IsDead)
            {
                if (this.Koopa.IsRightFacing)
                {
                    this.Flip = SpriteEffects.FlipHorizontally;
                }
                else
                {
                    this.Flip = SpriteEffects.None;
                }
            }
            else
            {
                this.Koopa.Fall();
            }
            
            if (PeriodCounter == EnemyConfig.KoopaFramePeriod)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame == this.LastFrame)
                    this.CurrentFrame = SpriteHolder.WalkingKoopaStartFrame;
                this.PeriodCounter = 0;
            }
            this.PeriodCounter++;
        }

        public void Draw(SpriteBatch SpriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(Width * CurrentFrame, 0, Width, Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.Koopa.CurrentPosition.X - camera.CurrentPosition.X), 
                (int)this.Koopa.CurrentPosition.Y - this.Height, Width, Height);
            SpriteBatch.Draw(this.Texture, drawnRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, this.Flip, 0);
        }

        public void SetStomped()
        {
            this.Width = 0;
            this.Height = 0;
        }

        public void SetDead()
        {
            this.Koopa.CurrentVelocity = new Vector2(0, EnemyConfig.DeathUpVelocity);
            this.Flip = SpriteEffects.FlipVertically;
            this.Koopa.IsDead = true;
        }
    }
}
