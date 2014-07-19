
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class ShellSprite : IAnimatedSprite
    {
        private Texture2D Texture;
        private int CurrentFrame;
        private Shell Shell;
        public int Width { get; private set; }
        public int Height { get; private set; }

        public ShellSprite(Shell shell)
        {
            this.Shell = shell;
            this.Texture = SpriteHolder.Koopa;
            this.CurrentFrame = SpriteHolder.KoopaShell;

            this.Width = SpriteHolder.KoopaWidth;
            this.Height = this.Texture.Height;
        }

        public void Update()
        {
            this.Shell.CurrentPosition = new Vector2(this.Shell.CurrentPosition.X + this.Shell.CurrentVelocity.X,
                this.Shell.CurrentPosition.Y + this.Shell.CurrentVelocity.Y);
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            Rectangle sourceRectangle = new Rectangle(Width * CurrentFrame, 0, Width, Height);
            Rectangle drawnRectangle = new Rectangle((int)(this.Shell.CurrentPosition.X - camera.CurrentPosition.X), 
                (int)this.Shell.CurrentPosition.Y - this.Height, Width, Height);

            spriteBatch.Draw(Texture, drawnRectangle, sourceRectangle, Color.White);
        }

        public void SetDead()
        {
            this.Width = 0;
            this.Height = 0;
        }
    }
}
