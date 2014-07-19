using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class SpinyEggCollisionResponder
    {
        private SpinyEgg SpinyEgg;

        public SpinyEggCollisionResponder(SpinyEgg spinyEgg)
        {
            this.SpinyEgg = spinyEgg;
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            if (obj is IMario)
            {
                IMario mario = (IMario)obj;
                RespondToCollisionWithMario(mario);
            }
            else if (obj is IProjectile)
            {
                RespondToCollisionWithProjectile((IProjectile)obj);
            }
            else if (obj is Block || obj is IPipe)
            {
                RespondToCollisionWithBlock(obj, side, intersectRect);
            }
        }

        private void RespondToCollisionWithProjectile(IProjectile obj)
        {
            if (obj is Fireball)
            {
                this.SpinyEgg.SetDead();
            }
        }

        private void RespondToCollisionWithBlock(IObject obj, Side side, Rectangle intersectRectangle)
        {
            if (!((obj as Block) != null && (obj as Block).CurrentState is HiddenBlockState))
            {
                if (SideGeneralizer.IsRight(side))
                {
                    this.SpinyEgg.CurrentPosition = new Vector2(this.SpinyEgg.CurrentPosition.X - intersectRectangle.Width, this.SpinyEgg.CurrentPosition.Y);
                    this.SpinyEgg.CurrentVelocity = new Vector2(-this.SpinyEgg.CurrentVelocity.X, this.SpinyEgg.CurrentVelocity.Y);
                }
                else if (SideGeneralizer.IsLeft(side))
                {
                    this.SpinyEgg.CurrentPosition = new Vector2(this.SpinyEgg.CurrentPosition.X + intersectRectangle.Width, this.SpinyEgg.CurrentPosition.Y);
                    this.SpinyEgg.CurrentVelocity = new Vector2(-this.SpinyEgg.CurrentVelocity.X, this.SpinyEgg.CurrentVelocity.Y);
                }
                else if (SideGeneralizer.IsTop(side))
                {
                    this.SpinyEgg.CurrentPosition = new Vector2(this.SpinyEgg.CurrentPosition.X, this.SpinyEgg.CurrentPosition.Y + intersectRectangle.Height);
                    this.SpinyEgg.CurrentVelocity = new Vector2(this.SpinyEgg.CurrentVelocity.X, 0f);
                }
                else if (SideGeneralizer.IsBottom(side))
                {
                    this.SpinyEgg.WillBecomeSpiny = true;
                    this.SpinyEgg.CurrentPosition = new Vector2(this.SpinyEgg.CurrentPosition.X, this.SpinyEgg.CurrentPosition.Y - intersectRectangle.Height);
                    this.SpinyEgg.SetDead();
                }
            }
        }

        private void RespondToCollisionWithMario(IMario mario)
        {
            if (mario.IsStar || mario.PowerLevel() == MarioPowerLevel.Metal)
            {
                this.SpinyEgg.SetDead();
            }
        }
    }
}