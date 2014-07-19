
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public interface IAnimatedMario : IAnimatedSprite 
    {
        void Flash(int timer);
    }
}
