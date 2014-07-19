
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class FlyingKoopaSprite : IAnimatedSprite
    {
        private Texture2D Texture;
        private int CurrentFrame;
        private int LastFrame;
        private int PeriodCounter = 0;
        private SpriteEffects Flip;
        private FlyingKoopa FlyingKoopa;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public FlyingKoopaSprite(FlyingKoopa flyingKoopa)
        {
            this.FlyingKoopa = flyingKoopa;
            this.Texture = SpriteHolder.Koopa;
            this.CurrentFrame = SpriteHolder.FlyingKoopaStartFrame;
            this.LastFrame = SpriteHolder.FlyingKoopaStartFrame + SpriteHolder.FlyingKoopaFrames;
            this.Width = SpriteHolder.KoopaWidth;
            this.Height = this.Texture.Height;
        }

        public void Update()
        {
            this.FlyingKoopa.CurrentPosition = new Vector2(this.FlyingKoopa.CurrentPosition.X + this.FlyingKoopa.CurrentVelocity.X,
                 this.FlyingKoopa.CurrentPosition.Y + this.FlyingKoopa.CurrentVelocity.Y);
            if (!this.FlyingKoopa.IsDead)
            {
                if (this.FlyingKoopa.IsRightFacing)
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
                this.FlyingKoopa.Fall();
            }

            if (PeriodCounter == EnemyConfig.FlyingKoopaFramePeriod)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame == this.LastFrame)
                    this.CurrentFrame = SpriteHolder.FlyingKoopaStartFrame;
                this.PeriodCounter = 0;
            }
            this.PeriodCounter++;
        }

        public void Draw(SpriteBatch SpriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(Width * CurrentFrame, 0, Width, Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.FlyingKoopa.CurrentPosition.X - camera.CurrentPosition.X),
                (int)this.FlyingKoopa.CurrentPosition.Y - this.Height, Width, Height);
            SpriteBatch.Draw(this.Texture, drawnRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, this.Flip, 0);
        }

        public void SetStomped()
        {
            this.Width = 0;
            this.Height = 0;
        }

        public void SetDead()
        {
            this.FlyingKoopa.CurrentVelocity = new Vector2(0, EnemyConfig.DeathUpVelocity);
            this.Flip = SpriteEffects.FlipVertically;
            this.FlyingKoopa.IsDead = true;
        }
    }
}
