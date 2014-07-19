
using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class GoombaCollisionResponder
    {
        private Goomba Goomba;

        public GoombaCollisionResponder(Goomba goomba)
        {
            this.Goomba = goomba;
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
                RespondToCollisionWithEnemy(side, obj as IEnemy);
            }
        }

        private void RespondToCollisionWithPipeBlock(Side side, Rectangle intersectRectangle)
        {
            if (SideGeneralizer.IsBottom(side))
            {
                this.Goomba.CurrentPosition = new Vector2(this.Goomba.CurrentPosition.X, this.Goomba.CurrentPosition.Y - intersectRectangle.Height);
                this.Goomba.CurrentVelocity = new Vector2(this.Goomba.CurrentVelocity.X, 0f);
            }
            else if (SideGeneralizer.IsRight(side))
            {
                this.Goomba.GoLeft();
            }
            else if (SideGeneralizer.IsLeft(side))
            {
                this.Goomba.GoRight();
            }
        }

        private void RespondToCollisionWithEnemy(Side side, IEnemy enemy)
        {
            if (!enemy.IsDead)
            {
                if (SideGeneralizer.IsLeft(side))
                {
                    this.Goomba.GoRight();
                }
                else if (SideGeneralizer.IsRight(side))
                {
                    this.Goomba.GoLeft();
                }
                else
                {
                    this.Goomba.Fall();
                }
            }
        }

        private void RespondToCollisionWithShell(Shell shell)
        {
            if (shell.IsMovingHorizontally())
            {
                this.Goomba.Kill();
                GameStats.Points += PointsConfig.EnemyKillWithShell;
            }
        }

        private void RespondToCollisionWithFireball()
        {
            this.Goomba.Kill();
            GameStats.Points += PointsConfig.EnemyKillWithFireball;
        } 

        private void RespondToCollisionWithMario(Side side, IMario mario)
        {
            if (SideGeneralizer.IsTop(side) && mario.IsFalling())
            {
                SoundBoard.Stomp.Play();
                this.Goomba.SetStomped();
            } 
            else if (ShouldDie(mario)) 
            {
                this.Goomba.Kill();
            }
        }

        private static bool ShouldDie(IMario mario)
        {
            return mario.IsStar || mario.PowerLevel() == MarioPowerLevel.Metal;
        }
    }
}
