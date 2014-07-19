using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class FireFlowerCollisionResponder
    {
        private FireFlower FireFlower;

        public FireFlowerCollisionResponder(FireFlower fireFlower)
        {
            this.FireFlower = fireFlower;
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            if (obj is IMario)
            {
                RespondToCollisionWithMario();
            }
            else if (obj is Block || obj is IPipe)
            {
                RespondToCollisionWithBlock(intersectRect);
            }
        }

        private void RespondToCollisionWithBlock(Rectangle intersectRectangle)
        {
            this.FireFlower.CurrentPosition = new Vector2(this.FireFlower.CurrentPosition.X, this.FireFlower.CurrentPosition.Y - intersectRectangle.Height);
            this.FireFlower.CurrentVelocity = new Vector2(this.FireFlower.CurrentVelocity.X, 0f);
        }

        private void RespondToCollisionWithMario()
        {
            if (!this.FireFlower.Spawning)
                this.FireFlower.Collect();
        }
    }
}
