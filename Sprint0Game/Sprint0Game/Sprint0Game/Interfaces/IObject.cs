
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public interface IObject
    {
        Vector2 CurrentPosition { get; set; }
        Vector2 CurrentVelocity { get; set; }
        bool ShouldBeRemoved { get; set; }
        bool HasBeenReached { get; set; }
        int Height { get; }
        int Width { get; }
       
        void Update();
        void Draw(SpriteBatch spriteBatch, ICamera camera);

        void RespondToCollision(Side side, IObject obj, Rectangle intersectRect);
        void RespondToNoCollision();
        
    }
}
