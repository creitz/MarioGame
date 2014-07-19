using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class FlagSprite : IAnimatedSprite
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int FlagWidth { get; private set; }
        public int FlagHeight { get; private set; }
        private Texture2D PoleTexture;
        private Texture2D FlagTexture;
        private Flag Flag;

        public FlagSprite(Flag flag)
        {
            this.Flag = flag;
            this.PoleTexture = SpriteHolder.Pole;
            this.FlagTexture = SpriteHolder.Flag;
            this.Width = this.PoleTexture.Width;
            this.Height = this.PoleTexture.Height;
            this.FlagWidth = this.FlagTexture.Width;
            this.FlagHeight = this.FlagTexture.Height;
        }

        public void Update() { }

        public void Draw(SpriteBatch SpriteBatch, ICamera camera)
        {
            Rectangle poleSourceRectangle = new Rectangle(0, 0, this.Width, this.Height);
            Rectangle poleDrawnRectangle = new Rectangle((int)(this.Flag.CurrentPosition.X - camera.CurrentPosition.X), 
                (int)this.Flag.CurrentPosition.Y - this.Height, Width, Height);
           SpriteBatch.Draw(this.PoleTexture, poleDrawnRectangle, poleSourceRectangle, Color.White);
            
            Rectangle flagSourceRectangle = new Rectangle(0, 0, this.FlagWidth, this.FlagHeight);
            Rectangle flagDrawnRectangle = new Rectangle((int)(this.Flag.FlagCurrentPosition.X - camera.CurrentPosition.X),(int)this.Flag.FlagCurrentPosition.Y - this.Height, Width, FlagHeight);
           SpriteBatch.Draw(this.FlagTexture, flagDrawnRectangle, flagSourceRectangle, Color.White);
        }

        public void RemoveFlag()
        {
            this.FlagWidth = 0;
            this.FlagHeight = 0;
        }
    }
}
