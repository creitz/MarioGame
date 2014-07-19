using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class SpinyCollisionResponder
    {
        private Spiny Spiny;

        public SpinyCollisionResponder(Spiny spiny)
        {
            this.Spiny = spiny;
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            if (obj is IMario)
            {
                RespondToCollisionWithMario(side, obj);
            }
            else if (obj is Shell)
            {
                RespondToCollisionWithShell((Shell)obj, side);
            }
            else if (obj is Fireball)
            {
                RespondToCollisionWithFireball();
            }
            else if (obj is Block || obj is IPipe)
            {
                RespondToCollisionWithBlockPipe(side, intersectRect);
            }
            else if (obj is IEnemy)
            {
                RespondToCollisionWithEnemy(side, obj as IEnemy);
            }
        }

        private void RespondToCollisionWithBlockPipe(Side side, Rectangle intersectRectangle)
        {
            if (SideGeneralizer.IsLeft(side))
            {
                this.Spiny.GoRight();
            }
            else if (SideGeneralizer.IsRight(side))
            {
                this.Spiny.GoLeft();
            }
            else if (SideGeneralizer.IsBottom(side))
            {
                this.Spiny.CurrentPosition = new Vector2(this.Spiny.CurrentPosition.X, this.Spiny.CurrentPosition.Y - intersectRectangle.Height);
                this.Spiny.CurrentVelocity = new Vector2(this.Spiny.CurrentVelocity.X, 0);
            }
        }

        private void RespondToCollisionWithEnemy(Side side, IEnemy enemy)
        {
            if (!enemy.IsDead)
            {
                if (SideGeneralizer.IsLeft(side))
                {
                    this.Spiny.GoRight();
                }
                else if (SideGeneralizer.IsRight(side))
                {
                    this.Spiny.GoLeft();
                }
            }
        }

        private void RespondToCollisionWithShell(Shell shell, Side side)
        {
            if (shell.IsMovingHorizontally())
            {
                this.Spiny.Kill();
                GameStats.Points += PointsConfig.EnemyKillWithShell;
            }
            else
            {
                if (SideGeneralizer.IsLeft(side))
                {
                    this.Spiny.GoRight();
                }
                else if (SideGeneralizer.IsRight(side))
                {
                    this.Spiny.GoLeft();
                }
            }
        }

        private void RespondToCollisionWithFireball()
        {
            this.Spiny.Kill();
            GameStats.Points += PointsConfig.EnemyKillWithFireball;
        }

        private void RespondToCollisionWithMario(Side side, IObject obj)
        {
            Mario mario = (Mario)obj;
            if (mario.IsStar || mario.PowerLevel() == MarioPowerLevel.Metal)
            {
                this.Spiny.Kill();
            }
            else if (SideGeneralizer.IsBottom(side))
            {
                this.Spiny.Kill();
            }
        }
    }
}
