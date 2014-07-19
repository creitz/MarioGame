
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class Shell : IProjectile
    {
        public Vector2 CurrentPosition { get; set; } //bottom left
        public Vector2 CurrentVelocity { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        private ShellSprite Sprite;
        private ShellCollisionResponder CollisionResponder;

        public Shell(Vector2 startPosition)
        {
            this.CurrentVelocity = new Vector2(0, 0);
            this.CurrentPosition = startPosition;
            this.Sprite = new ShellSprite(this);
            this.CollisionResponder = new ShellCollisionResponder(this);
        }

        public void Update()
        {
            this.Sprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            this.Sprite.Draw(spriteBatch, camera);
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            this.CollisionResponder.RespondToCollision(side, obj, intersectRect);
        }

        public void RespondToNoCollision()
        {
            this.CurrentVelocity = new Vector2(this.CurrentVelocity.X, this.CurrentVelocity.Y + PhysicsConfig.Gravity);
        }

        public void GoLeft()
        {
            this.CurrentVelocity = new Vector2(-ProjectileConfig.ShellVelocity, this.CurrentVelocity.Y);
            this.CurrentPosition = new Vector2(this.CurrentPosition.X + this.CurrentVelocity.X,
                this.CurrentPosition.Y + this.CurrentVelocity.Y);
        }

        public void GoRight() 
        {
            this.CurrentVelocity = new Vector2(ProjectileConfig.ShellVelocity, this.CurrentVelocity.Y);
            this.CurrentPosition = new Vector2(this.CurrentPosition.X + this.CurrentVelocity.X,
                this.CurrentPosition.Y + this.CurrentVelocity.Y);
        }

        public bool IsMovingHorizontally()
        {
            return this.CurrentVelocity.X != 0;
        }

        public void SetDead()
        {
            this.Sprite.SetDead();
            this.ShouldBeRemoved = true;
        }
    }
}
