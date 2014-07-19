
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class DyingMario : IAnimatedMario
    {
        public Texture2D Texture { get; private set; }
        private IMario Mario;
        private int Frame;
        private Color Color = Color.White;
        public int Width { get; private set; }
        public int Height { get; private set; }
        private int WaitBeforeFlyingUpTimer = MarioConfig.WaitBeforeFlyingUpTimer;

        public DyingMario(IMario mario)
        {
            this.Texture = SpriteHolder.SmallMario;
            this.Frame = SpriteHolder.DeadMarioColumn;
            this.Mario = mario;

            this.Width = SpriteHolder.SmallMarioWidth;
            this.Height = this.Texture.Height;
        }

        public void Update()
        {
            WaitBeforeFlyingUpTimer--;
            if (WaitBeforeFlyingUpTimer == 0)
            {
                this.Mario.CurrentVelocity = new Vector2(0, MarioConfig.DeadVelocity);
            }
            else if (WaitBeforeFlyingUpTimer < 0)
            {

                this.Mario.CurrentPosition = new Vector2(this.Mario.CurrentPosition.X, this.Mario.CurrentPosition.Y + this.Mario.CurrentVelocity.Y);
                this.Mario.CurrentVelocity = new Vector2(this.Mario.CurrentVelocity.X, this.Mario.CurrentVelocity.Y + PhysicsConfig.Gravity);

                if (this.Mario.CurrentPosition.Y > this.Mario.Level.Window.Bounds.Height + Level1Config.MarioFallingDeathYThreshold)
                {
                    this.Mario.CurrentState = new DeadMarioState();
                }
            }
        }

        public void Flash(int timer)
        {
            if (timer % MarioConfig.FlashStepPeriod == 1)
            {
                this.Color = Color.Black;
            }
            else
            {
                this.Color = Color.Wheat;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(this.Frame * this.Width, 0, this.Width, this.Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.Mario.CurrentPosition.X - camera.CurrentPosition.X),
                (int)this.Mario.CurrentPosition.Y - this.Height, this.Width, this.Height);

            spriteBatch.Draw(this.Texture, drawnRectangle, sourceRectangle, this.Color, 0, Vector2.Zero, SpriteEffects.None, 0);    
        }

    }
}
