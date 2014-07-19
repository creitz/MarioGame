
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class RunningMario : IAnimatedMario
    {
        public Texture2D Texture { get; private set; }
        private int EndFrame;
        private int StartFrame;
        private int CurrentFrame;
        private int counter;
        private SpriteEffects Flip;
        private float PositionIncrement;
        private Color Color = Color.White;
        private Color OriginalColor = Color.White;
        private IMario Mario;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public RunningMario(IMario mario, bool rightFacing, MarioPowerLevel powerLevel)
        {
            switch (powerLevel)
            {
                case MarioPowerLevel.Fire:
                case MarioPowerLevel.Metal:
                    this.Texture = SpriteHolder.FireMario;
                    break;
                case MarioPowerLevel.Big:
                    this.Texture = SpriteHolder.BigMario;
                    break;
                case MarioPowerLevel.Small:
                    this.Texture = SpriteHolder.SmallMario;
                    break;
            }

            if (powerLevel == MarioPowerLevel.Metal)
            {
                this.Color = MarioConfig.MetalMarioColor;
                OriginalColor = this.Color;
            }

            this.StartFrame = SpriteHolder.TraversingMarioStartFrame;
            this.CurrentFrame = this.StartFrame;
            this.EndFrame = SpriteHolder.TraversingMarioEndFrame;
            this.Mario = mario;
            this.Mario.CurrentVelocity = new Vector2(this.Mario.CurrentVelocity.X, 0);
            this.counter = 0;

            if (rightFacing) {
                this.Flip = SpriteEffects.None;
                this.PositionIncrement = MarioConfig.SideSpeed;
            } else {
                this.Flip = SpriteEffects.FlipHorizontally;
                this.PositionIncrement = -1 * MarioConfig.SideSpeed;
            }

            if (MarioPowerLevelGeneralizer.IsBig(powerLevel))
                this.Width = SpriteHolder.BigMarioWidth;
            else
                this.Width = SpriteHolder.SmallMarioWidth;
            this.Height = this.Texture.Height;
        }

        public void Update()
        {
            this.counter++;
            this.Mario.CurrentPosition = new Vector2(this.Mario.CurrentPosition.X + this.PositionIncrement, this.Mario.CurrentPosition.Y);

            if (counter == MarioConfig.RunningFrameStepPeriod)
            {
                this.CurrentFrame++;
                if (this.CurrentFrame == this.EndFrame)
                {
                    this.CurrentFrame = this.StartFrame;
                }
                this.counter = 0;
            }

            if (this.Mario.IsStar)
            {
                this.Color = Color.Yellow;
            }
            else
            {
                this.Color = OriginalColor;
            }
            if (this.Mario.IsTransitioningFromDamage)
            {
                this.Flash(this.Mario.TransitionFromDamageTimer);
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
                this.Color = OriginalColor;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(this.Width * this.CurrentFrame, 0, this.Width, this.Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.Mario.CurrentPosition.X - camera.CurrentPosition.X),
                (int)this.Mario.CurrentPosition.Y - this.Height, this.Width, this.Height);

            spriteBatch.Draw(this.Texture, drawnRectangle, sourceRectangle, this.Color, 0, Vector2.Zero, this.Flip, 0);   
        }

    }
}
