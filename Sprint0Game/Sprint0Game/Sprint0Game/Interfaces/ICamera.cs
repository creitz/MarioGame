
using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public interface ICamera
    {
        Vector2 CurrentPosition { get; set; }
        float LargestAchievedXPosition { get; }
        float MinXPosition { get; set; }
        float MaxXPosition { get; set; }

        void Update();
    }
}
