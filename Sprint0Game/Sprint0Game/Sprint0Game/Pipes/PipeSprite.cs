using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class PipeSprite : IAnimatedSprite
    {
        private Texture2D Texture;
        private int Frame;
        private IPipe Pipe;
        public int Width { get; set; }
        public int Height { get; set; }

        public PipeSprite(IPipe pipe)
        {
            this.Pipe = pipe;
            this.Frame = this.Pipe.Type;
            if (this.Pipe.Side)
            {
                this.Texture = SpriteHolder.SidePipes;
                this.Width = this.Texture.Width / SpriteHolder.SidePipeTotalFrames;
                this.Height = this.Texture.Height;
            }
            else
            {
                this.Texture = SpriteHolder.UpPipes;
                this.Width = this.Texture.Width;
                this.Height = this.Texture.Height / SpriteHolder.UpPipeTotalFrames;
            }
            
        }
        public void Update() { }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle;
            if(this.Pipe.Side)
                sourceRectangle = new Rectangle(Width*Frame, 0, Width, Height);
            else
                sourceRectangle = new Rectangle(0, Height*Frame, Width, Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.Pipe.CurrentPosition.X - camera.CurrentPosition.X), (int)this.Pipe.CurrentPosition.Y - Height, Width, Height);

            spriteBatch.Draw(this.Texture, drawnRectangle, sourceRectangle, Color.White);
        }
    }
}
