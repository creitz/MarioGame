using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;

namespace Sprint0Game
{
    public class FlagCollisionResponder
    {
        private Flag Flag;

        public FlagCollisionResponder(Flag flag)
        {
            this.Flag = flag;
        }

        public void RespondToCollision(IObject obj, Rectangle intersectRect)
        {
            if (obj is IMario)
            {
                RespondToCollisionWithMario(intersectRect);
            }
        }

        private void RespondToCollisionWithMario(Rectangle intersectRect)
        {
            GameStats.Points += PointsConfig.FlagCollectionPoints - intersectRect.Y;
            MediaPlayer.Stop();
            SoundBoard.DownTheFlagpole.Play();
            this.Flag.LevelComplete = true;        
        }
    }
}
