using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class MushroomCollisionResponder
    {
        private Mushroom Mushroom;

        public MushroomCollisionResponder(Mushroom mushroom)
        {
            this.Mushroom = mushroom;
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            if (!this.Mushroom.Spawning)
            {
                if (obj is IMario)
                {
                    RespondToCollisionWithMario();
                }
                else if (obj is Block || obj is IPipe)
                {
                    RespondToCollisionWithBlock(obj, side, intersectRect);
                }
            }
        }

        private void RespondToCollisionWithBlock(IObject obj, Side side, Rectangle intersectRectangle)
        {
            if (!((obj as Block) != null && (obj as Block).CurrentState is HiddenBlockState))
            {
                if (SideGeneralizer.IsRight(side))
                {
                    this.Mushroom.CurrentVelocity = new Vector2(-ItemConfig.MushroomVelocity, this.Mushroom.CurrentVelocity.Y);
                }
                else if (SideGeneralizer.IsLeft(side))
                {
                    this.Mushroom.CurrentVelocity = new Vector2(ItemConfig.MushroomVelocity, this.Mushroom.CurrentVelocity.Y);
                }
                else if (SideGeneralizer.IsBottom(side))
                {
                    this.Mushroom.CurrentPosition = new Vector2(this.Mushroom.CurrentPosition.X, this.Mushroom.CurrentPosition.Y - intersectRectangle.Height);
                    this.Mushroom.CurrentVelocity = new Vector2(this.Mushroom.CurrentVelocity.X, 0);
                }
            }
        }

        private void RespondToCollisionWithMario()
        {
            if (!this.Mushroom.Spawning)
            {
                if (this.Mushroom.Type == MushroomType.Red)
                    SoundBoard.PowerUp.Play();
                else if (this.Mushroom.Type == MushroomType.Green)
                    SoundBoard.OneUp.Play();
                this.Mushroom.Collect();
            }
        }
    }
}
