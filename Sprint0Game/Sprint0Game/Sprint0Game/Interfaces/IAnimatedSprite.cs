
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public interface IAnimatedSprite
    {
        void Update();
        void Draw(SpriteBatch spriteBatch, ICamera camera);

        int Height { get; }
        int Width { get; }
    }
}
