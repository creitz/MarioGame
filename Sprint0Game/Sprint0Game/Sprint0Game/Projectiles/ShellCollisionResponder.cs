using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class ShellCollisionResponder
    {
        private Shell Shell;

        public ShellCollisionResponder(Shell shell)
        {
            this.Shell = shell;
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            if (obj is IMario)
            {
                IMario mario = (IMario)obj;
                RespondToCollisionWithMario(side,mario);
            }
            else if (obj is IProjectile)
            {
                RespondToCollisionWithProjectile(side);
            }
            else if (obj is Block || obj is IPipe)
            {
                RespondToCollisionWithBlock(obj, side, intersectRect);
            }
        }

        private void RespondToCollisionWithProjectile(Side side)
        {
            if (SideGeneralizer.IsRight(side) || side == Side.RightOfTop || side == Side.RightOfBottom)
            {
                this.Shell.GoLeft();
            }
            else if (SideGeneralizer.IsLeft(side) || side == Side.LeftOfTop || side == Side.LeftOfBottom)
            {
                this.Shell.GoRight();
            }
        }

        private void RespondToCollisionWithBlock(IObject obj, Side side, Rectangle intersectRectangle)
        {
            if (!((obj as Block) != null && (obj as Block).CurrentState is HiddenBlockState))
            {
                if (SideGeneralizer.IsRight(side))
                {
                    this.Shell.GoLeft();
                }
                else if (SideGeneralizer.IsLeft(side))
                {
                    this.Shell.GoRight();
                }
                else if (SideGeneralizer.IsBottom(side))
                {
                    this.Shell.CurrentPosition = new Vector2(this.Shell.CurrentPosition.X, this.Shell.CurrentPosition.Y - intersectRectangle.Height);
                    this.Shell.CurrentVelocity = new Vector2(this.Shell.CurrentVelocity.X, 0);
                }
            }
        }

        private void RespondToCollisionWithMario(Side side, IMario mario)
        {
            if(mario.IsStar)
            {
                this.Shell.SetDead();
            }
            else if (this.Shell.IsMovingHorizontally() && !mario.IsTransitioningFromDamage)
            {
                if (!SideGeneralizer.IsTop(side) || mario.IsStar)
                {
                    this.Shell.SetDead();
                }
                else
                {
                    this.Shell.CurrentVelocity = new Vector2(0f, this.Shell.CurrentVelocity.Y);
                }
            }
            else if (!this.Shell.IsMovingHorizontally())
            {
                if (side == Side.RightOfTop || SideGeneralizer.IsRight(side))
                {
                    this.Shell.GoLeft();
                }
                else if (side == Side.LeftOfTop || SideGeneralizer.IsLeft(side))
                {
                    this.Shell.GoRight();
                }
            }
        }
    }
}
