
using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class NonMovingCamera : ICamera
    {
        public Vector2 CurrentPosition { get; set; }
        public float LargestAchievedXPosition { get; set; }
        public float MinXPosition { get; set; }
        public float MaxXPosition { get; set; }

        public NonMovingCamera()
        {
            this.CurrentPosition = new Vector2(MinXPosition, 0);
        }

        public void Update()
        {
            //Design choice to leave a camera that does nothing in the hord level.  Choice made because it is easier to change cameras than try to reintegrate it into solution if you want to add scrolling in the future.
        }
    }
}
