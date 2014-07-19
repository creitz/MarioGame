
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class Fireball : IProjectile
    {
        public Vector2 CurrentPosition { get; set; } //bottom left
        public Vector2 CurrentVelocity { get; set; }
        public bool IsAffectedByGravity { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        private FireballSprite Sprite;
        private FireballCollisionResponder CollisionResponder;
        
        public Fireball(Vector2 startPosition, bool Right)
        {
            float xVelocity;
            if (Right)
                xVelocity = ProjectileConfig.FireballVelocity;
            else
                xVelocity = -ProjectileConfig.FireballVelocity;
            this.CurrentVelocity = new Vector2(xVelocity, ProjectileConfig.FireballVelocity);
            this.CurrentPosition = startPosition;
            this.IsAffectedByGravity = false;
            this.Sprite = new FireballSprite(this);
            this.CollisionResponder = new FireballCollisionResponder(this);
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
            if (this.IsAffectedByGravity)
                this.Fall();
        }

        public void Fall()
        {
            this.CurrentVelocity = new Vector2(this.CurrentVelocity.X, this.CurrentVelocity.Y + PhysicsConfig.Gravity);
        }

        public void SetGone()
        {
            this.Sprite.SetGone();
        }
    }
}
