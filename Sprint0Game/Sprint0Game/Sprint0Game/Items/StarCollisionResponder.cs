using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class StarCollisionResponder
    {
        private Star Star;

        public StarCollisionResponder(Star star)
        {
            this.Star = star;
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            if (!this.Star.Spawning)
            {
                if (obj is IMario)
                {
                    RespondToCollisionWithMario();
                }
                else if (obj is Block || obj is IPipe)
                {
                    RespondToCollisionWithBlock(obj, side, intersectRect);
                }
                else
                {
                    this.Star.Fall();
                }
            }
        }

        private void RespondToCollisionWithBlock(IObject obj, Side side, Rectangle intersectRect)
        {
            if (!((obj as Block) != null && (obj as Block).CurrentState is HiddenBlockState))
            {
                if (SideGeneralizer.IsLeft(side))
                {
                    this.Star.CurrentPosition = new Vector2(this.Star.CurrentPosition.X + (intersectRect.Width),
                                        this.Star.CurrentPosition.Y);
                    this.Star.CurrentVelocity = new Vector2(ItemConfig.StarVelocity, this.Star.CurrentVelocity.Y);
                }
                else if (SideGeneralizer.IsRight(side))
                {
                    this.Star.CurrentPosition = new Vector2(this.Star.CurrentPosition.X - (intersectRect.Width),
                                        this.Star.CurrentPosition.Y);
                    this.Star.CurrentVelocity = new Vector2(-ItemConfig.StarVelocity, this.Star.CurrentVelocity.Y);
                }
                else if (SideGeneralizer.IsBottom(side))
                {
                    this.Star.CurrentPosition = new Vector2(this.Star.CurrentPosition.X, this.Star.CurrentPosition.Y - intersectRect.Height);
                    if (!this.Star.Spawning)
                        this.Star.CurrentVelocity = new Vector2(this.Star.CurrentVelocity.X, ItemConfig.StarBounceVelocity);
                }
                else if (SideGeneralizer.IsTop(side))
                {
                    this.Star.CurrentPosition = new Vector2(this.Star.CurrentPosition.X, this.Star.CurrentPosition.Y + intersectRect.Height);
                    this.Star.CurrentVelocity = new Vector2(ItemConfig.StarVelocity, 0);
                }
            }
        }

        private void RespondToCollisionWithMario()
        {
            if (!this.Star.Spawning)
            {
                SoundBoard.PowerUp.Play();
                this.Star.Collect();
            }
        }
    }
}
