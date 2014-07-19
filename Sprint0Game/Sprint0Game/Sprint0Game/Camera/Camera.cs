
using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class Camera : ICamera
    {
        public Vector2 CurrentPosition { get; set; }
        public float LargestAchievedXPosition { get; private set; }
        public float MinXPosition { get; set; }
        public float MaxXPosition { get; set; }
        private ILevel Level;

        public Camera(ILevel level)
        {
            this.CurrentPosition = new Vector2(MinXPosition, 0);
            this.Level = level;
        }

        public void Update()
        {
            Vector2 PotentialPos = new Vector2();
            bool changed = false;
            if ((int)(this.Level.Mario.CurrentPosition.X - this.CurrentPosition.X) >= CameraConfig.MarioToCameraRightDifferenceThreshold)
            {
                if (this.CurrentPosition.X < MaxXPosition)
                {
                    PotentialPos = new Vector2((this.CurrentPosition.X + MarioConfig.SideSpeed), this.CurrentPosition.Y);
                    changed = true;
                }
            }
            else if ((int)(this.Level.Mario.CurrentPosition.X - this.CurrentPosition.X) <= CameraConfig.MarioToCameraLeftDifferenceThreshold)
            {
                if (this.CurrentPosition.X > MinXPosition)
                {
                    PotentialPos = new Vector2((this.CurrentPosition.X - MarioConfig.SideSpeed), this.CurrentPosition.Y);
                    changed = true;
                }
            }
            if (changed && !this.Level.Mario.IsUnderground)
            {
                if (PotentialPos.X > MinXPosition)
                    this.CurrentPosition = PotentialPos;
                else
                    this.CurrentPosition = new Vector2(MinXPosition, this.CurrentPosition.Y);
            }

            if (this.CurrentPosition.X > this.LargestAchievedXPosition)
            {
                this.LargestAchievedXPosition = this.CurrentPosition.X;
            }
        }
    }
}
