
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public interface IMarioState
    {
        IAnimatedMario Sprite { get; set; }
        void RespondToRequest(MarioActionRequest change);
        MarioPowerLevel PowerLevel();

        bool IsRightFacing();

        void Update();
        void Draw(SpriteBatch spriteBatch, ICamera camera);

    }
}
