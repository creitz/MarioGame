
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class Background
    {
        public Texture2D Texture { get; set; }

        public Background()
        {
            this.Texture = SpriteHolder.Background;
        }

        public void Draw(Rectangle window, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle (0, 0, window.Width, window.Height), Color.White);
        }
    }
}
