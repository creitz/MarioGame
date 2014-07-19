using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class CastleSprite : IAnimatedSprite
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private Texture2D CastleImg = SpriteHolder.Castle;
        private Castle Castle;

        public CastleSprite(Castle castle)
        {
            this.Castle = castle;
            this.Width = this.CastleImg.Width;
            this.Height = this.CastleImg.Height;
        }

        public void Update() { }

        public void Draw(SpriteBatch SpriteBatch, ICamera camera)
        {
           Rectangle flagSourceRectangle = new Rectangle(0, 0, Width, Height);
           Rectangle flagDrawnRectangle = new Rectangle((int)(this.Castle.CurrentPosition.X - camera.CurrentPosition.X),
               (int)this.Castle.CurrentPosition.Y - this.Height, Width, Height);
           SpriteBatch.Draw(this.CastleImg, flagDrawnRectangle, flagSourceRectangle, Color.White);
        }
    }
}
