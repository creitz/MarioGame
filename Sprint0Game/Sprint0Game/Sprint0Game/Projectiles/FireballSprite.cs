
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class FireballSprite : IAnimatedSprite
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private int TotalFrames, CurrentFrame, FrameStepper = 0;
        private Texture2D Texture;
        private int FireballTimer = ProjectileConfig.FireballLife;
        private Fireball Fireball;
        private int InitialFrame = 0;
        private int ImgHeight, ImgWidth;
        
        public FireballSprite(Fireball fireball)
        {
            this.Fireball = fireball;
            this.TotalFrames = SpriteHolder.FireballFrames;
            this.Texture = SpriteHolder.Fireball;
            this.CurrentFrame = InitialFrame;
            this.ImgWidth = this.Texture.Width / this.TotalFrames;
            this.ImgHeight = this.Texture.Height;
            this.Height = (int)(this.ImgHeight * ProjectileConfig.FireballHeightScale);
            this.Width = (int)(this.ImgWidth * ProjectileConfig.FireballWidthScale);
        }

        public void Update()
        {
            this.Fireball.CurrentPosition = new Vector2(this.Fireball.CurrentPosition.X + this.Fireball.CurrentVelocity.X,
                this.Fireball.CurrentPosition.Y + this.Fireball.CurrentVelocity.Y);

            if (FrameStepper % ProjectileConfig.FrameStepperPeriod == 0)
                this.CurrentFrame++;
            if (this.CurrentFrame % this.TotalFrames == 0)
                this.CurrentFrame = this.InitialFrame;
            FrameStepper++;

            if (this.FireballTimer < 0)
            {
                this.SetGone();
            }
            this.FireballTimer--;
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            int column = CurrentFrame % TotalFrames;

            Rectangle sourceRectangle = new Rectangle(this.ImgWidth * column, 0, this.ImgWidth, this.ImgHeight);
            Rectangle drawnRectangle = new Rectangle((int)this.Fireball.CurrentPosition.X - (int)camera.CurrentPosition.X,
                (int)this.Fireball.CurrentPosition.Y - this.Height, this.Width, this.Height);

            spriteBatch.Draw(Texture, drawnRectangle, sourceRectangle, Color.White);
        }

        public void SetGone() 
        {
            this.Width = 0;
            this.Height = 0;
            this.Fireball.ShouldBeRemoved = true;
        }

    }
}
