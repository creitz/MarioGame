using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class KoopaCollisionResponder
    {
        private Koopa Koopa;

        public KoopaCollisionResponder(Koopa koopa)
        {
            this.Koopa = koopa;
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            if (obj is IMario)
            {
                RespondToCollisionWithMario(side, obj);
            }
            else if (obj is Shell)
            {
                RespondToCollisionWithShell((Shell)obj);
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
            if (SideGeneralizer.IsBottom(side))
            {
                this.Koopa.CurrentPosition = new Vector2(this.Koopa.CurrentPosition.X, this.Koopa.CurrentPosition.Y - intersectRectangle.Height);
                this.Koopa.CurrentVelocity = new Vector2(this.Koopa.CurrentVelocity.X, 0);
            }
            else if (SideGeneralizer.IsRight(side))
            {
                this.Koopa.GoLeft();
            }
            else if (SideGeneralizer.IsLeft(side))
            {
                this.Koopa.GoRight();
            }
        }

        private void RespondToCollisionWithEnemy(Side side, IEnemy enemy)
        {
            if (!enemy.IsDead)
            {
                if (SideGeneralizer.IsLeft(side))
                {
                    this.Koopa.GoRight();
                }
                else if (SideGeneralizer.IsRight(side))
                {
                    this.Koopa.GoLeft();
                }
                else
                {
                    this.Koopa.Fall();
                }
            }
        }

        private void RespondToCollisionWithShell(Shell shell)
        {
            if (shell.IsMovingHorizontally())
            {
                this.Koopa.SetDead();
                GameStats.Points += PointsConfig.EnemyKillWithShell;
            }
        }

        private void RespondToCollisionWithFireball()
        {
            this.Koopa.SetDead();
            GameStats.Points += PointsConfig.EnemyKillWithFireball;
        }     

        private void RespondToCollisionWithMario(Side side, IObject obj)
        {
            Mario mario = (Mario)obj;
            if (mario.IsStar || mario.PowerLevel() == MarioPowerLevel.Metal)
            {
                this.Koopa.SetDead();
            }
            else if (SideGeneralizer.IsTop(side))
            {
                this.Koopa.WillBecomeShell = true;
                this.Koopa.SetStomped();
            }
        }
    }
}
