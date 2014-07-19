
using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class LakituCollisionResponder
    {
        private Lakitu Lakitu;

        public LakituCollisionResponder(Lakitu lakitu)
        {
            this.Lakitu = lakitu;
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            if (obj is IMario)
            {
                RespondToCollisionWithMario(side, (IMario)obj);
            }
            else if (obj is Shell)
            {
                RespondToCollisionWithShell(obj as Shell);
            }
            else if (obj is Fireball)
            {
                RespondToCollisionWithFireball();
            }
            else if (obj is Block || obj is IPipe)
            {
                RespondToCollisionWithPipeBlock(side, intersectRect);
            }
            else if (obj is IEnemy)
            {
                RespondToCollisionWithEnemy(side, obj as IEnemy, intersectRect);
            }
        }

        private void RespondToCollisionWithPipeBlock(Side side, Rectangle intersectRectangle)
        {
            if (SideGeneralizer.IsBottom(side))
            {
                this.Lakitu.CurrentPosition = new Vector2(this.Lakitu.CurrentPosition.X, this.Lakitu.CurrentPosition.Y - intersectRectangle.Height);
                this.Lakitu.CurrentVelocity = new Vector2(this.Lakitu.CurrentVelocity.X, 0f);
            }
            else if (SideGeneralizer.IsRight(side))
            {
                this.Lakitu.CurrentPosition = new Vector2(this.Lakitu.CurrentPosition.X - intersectRectangle.Width, this.Lakitu.CurrentPosition.Y);
                this.Lakitu.Rise();
            }
            else if (SideGeneralizer.IsLeft(side))
            {
                this.Lakitu.CurrentPosition = new Vector2(this.Lakitu.CurrentPosition.X + intersectRectangle.Width, this.Lakitu.CurrentPosition.Y);
                this.Lakitu.Rise();
            }
            else
            {
                this.Lakitu.CurrentPosition = new Vector2(this.Lakitu.CurrentPosition.X, this.Lakitu.CurrentPosition.Y + intersectRectangle.Height);
                this.Lakitu.CurrentVelocity = new Vector2(this.Lakitu.CurrentVelocity.X, 0f);
            }
        }

        private void RespondToCollisionWithEnemy(Side side, IEnemy enemy, Rectangle intersectRectangle)
        {
            if (!enemy.IsDead)
            {
                if (SideGeneralizer.IsLeft(side))
                {
                    this.Lakitu.CurrentPosition = new Vector2(this.Lakitu.CurrentPosition.X + intersectRectangle.Width, this.Lakitu.CurrentPosition.Y);
                    this.Lakitu.Rise();
                }
                else if (SideGeneralizer.IsRight(side))
                {
                    this.Lakitu.CurrentPosition = new Vector2(this.Lakitu.CurrentPosition.X - intersectRectangle.Width, this.Lakitu.CurrentPosition.Y);
                    this.Lakitu.Rise();
                }
                else
                {
                    this.Lakitu.Rise();
                }
            }
        }

        private void RespondToCollisionWithShell(Shell shell)
        {
            if (shell.IsMovingHorizontally())
            {
                this.Lakitu.Kill();
                GameStats.Points += PointsConfig.EnemyKillWithShell;
            }
        }

        private void RespondToCollisionWithFireball()
        {
            this.Lakitu.Kill();
            GameStats.Points += PointsConfig.EnemyKillWithFireball;
        }

        private void RespondToCollisionWithMario(Side side, IMario mario)
        {
            if (SideGeneralizer.IsTop(side) && mario.IsFalling())
            {
                SoundBoard.Stomp.Play();
                this.Lakitu.Kill();
            }
            else if (ShouldDie(mario))
            {
                this.Lakitu.Kill();
            }
        }

        private static bool ShouldDie(IMario mario)
        {
            return mario.IsStar || mario.PowerLevel() == MarioPowerLevel.Metal;
        }
    }
}