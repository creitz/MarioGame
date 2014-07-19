using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class FlyingKoopaCollisionResponder
    {
        private FlyingKoopa FlyingKoopa;

        public FlyingKoopaCollisionResponder(FlyingKoopa flyingKoopa)
        {
            this.FlyingKoopa = flyingKoopa;
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
                this.FlyingKoopa.GoRight();
            }
            else if (SideGeneralizer.IsRight(side))
            {
                this.FlyingKoopa.GoLeft();
            }
            else if (SideGeneralizer.IsBottom(side))
            {
                this.FlyingKoopa.CurrentPosition = new Vector2(this.FlyingKoopa.CurrentPosition.X, this.FlyingKoopa.CurrentPosition.Y - intersectRectangle.Height);
                this.FlyingKoopa.CurrentVelocity = new Vector2(this.FlyingKoopa.CurrentVelocity.X, EnemyConfig.FlyingKoopaBounceVelocity);
            }
        }

        private void RespondToCollisionWithEnemy(Side side, IEnemy enemy)
        {
            if (!enemy.IsDead)
            {
                if (SideGeneralizer.IsLeft(side))
                {
                    this.FlyingKoopa.GoRight();
                }
                else if (SideGeneralizer.IsRight(side))
                {
                    this.FlyingKoopa.GoLeft();
                }
            }
        }

        private void RespondToCollisionWithShell(Shell shell, Side side)
        {
            if (shell.IsMovingHorizontally())
            {
                this.FlyingKoopa.SetDead();
                GameStats.Points += PointsConfig.EnemyKillWithShell;
            }
            else
            {
                if (SideGeneralizer.IsLeft(side))
                {
                    this.FlyingKoopa.GoRight();
                }
                else if (SideGeneralizer.IsRight(side))
                {
                    this.FlyingKoopa.GoLeft();
                }
            }
        }

        private void RespondToCollisionWithFireball()
        {
            this.FlyingKoopa.SetDead();
            GameStats.Points += PointsConfig.EnemyKillWithFireball;
        }

        private void RespondToCollisionWithMario(Side side, IObject obj)
        {
            Mario mario = (Mario)obj;
            if (mario.IsStar || mario.PowerLevel() == MarioPowerLevel.Metal)
            {
                this.FlyingKoopa.SetDead();
            }
            else if (SideGeneralizer.IsTop(side))
            {
                this.FlyingKoopa.WillBecomeKoopa = true;
                this.FlyingKoopa.SetStomped();
            }
        }
    }
}
