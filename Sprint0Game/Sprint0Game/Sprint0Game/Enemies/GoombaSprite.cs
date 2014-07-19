
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class GoombaSprite : IAnimatedSprite
    {
        private Texture2D Texture;
        private int TotalFrames;
        private int PeriodCounter = 0;
        private SpriteEffects Flip;
        private int CurrentFrame = 0;
        private Goomba Goomba;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public GoombaSprite(Goomba goomba)
        {
            this.Goomba = goomba;
            this.CurrentFrame = SpriteHolder.WalkingGoombaStartFrame;
            this.TotalFrames = SpriteHolder.WalkingGoombaFrames + SpriteHolder.WalkingGoombaStartFrame;
            this.Texture = SpriteHolder.WalkingGoomba;

            this.Width = SpriteHolder.GoombaWidth;
            this.Height = this.Texture.Height;
        }

        public void Update()
        {
            if (!(CurrentFrame == SpriteHolder.SquashedGoombaFrame))
            {
                if (this.Goomba.IsDead)
                    this.Goomba.Fall();

                this.Goomba.CurrentPosition = new Vector2(this.Goomba.CurrentPosition.X + this.Goomba.CurrentVelocity.X,
                    this.Goomba.CurrentPosition.Y + this.Goomba.CurrentVelocity.Y);

                if (this.PeriodCounter == EnemyConfig.GoombaFramePeriod)
                {
                    this.CurrentFrame++;
                    if (this.CurrentFrame > this.TotalFrames)
                    {
                        this.CurrentFrame = SpriteHolder.WalkingGoombaStartFrame;
                    }
                    this.PeriodCounter = 0;
                }
                this.PeriodCounter++;
            }
        }

        public void GoLeft()
        {
            this.Goomba.CurrentVelocity = new Vector2(-EnemyConfig.GoombaRunVelocity, this.Goomba.CurrentVelocity.Y);
            this.Flip = SpriteEffects.None;
        }

        public void GoRight()
        {
            this.Goomba.CurrentVelocity = new Vector2(EnemyConfig.GoombaRunVelocity, this.Goomba.CurrentVelocity.Y);
            this.Flip = SpriteEffects.FlipHorizontally;
        }

        public void Draw(SpriteBatch SpriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(this.Width * this.CurrentFrame, 0, this.Width, this.Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.Goomba.CurrentPosition.X - camera.CurrentPosition.X), 
                (int)this.Goomba.CurrentPosition.Y - this.Height, Width, Height);

            SpriteBatch.Draw(this.Texture, drawnRectangle, sourceRectangle, Color.White, 0, Vector2.Zero, this.Flip, 0);
        }      

        public void SetStomped()
        {
            this.CurrentFrame = SpriteHolder.SquashedGoombaFrame;
        }

        public void SetDead()
        {
            this.Goomba.CurrentVelocity = new Vector2(0, EnemyConfig.DeathUpVelocity);
            this.Flip = SpriteEffects.FlipVertically;
            this.Goomba.IsDead = true;
        }
    }
}
