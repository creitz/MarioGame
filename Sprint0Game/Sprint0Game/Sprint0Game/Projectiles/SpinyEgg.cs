
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class SpinyEgg : IProjectile
    {
        public Vector2 CurrentPosition { get; set; } //bottom left
        public Vector2 CurrentVelocity { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        public bool WillBecomeSpiny { get; set; }
        private SpinyEggSprite Sprite;
        private SpinyEggCollisionResponder CollisionResponder;

        public SpinyEgg(Vector2 startPosition, float distanceFromMario)
        {

            this.CurrentVelocity = new Vector2(distanceFromMario * ProjectileConfig.EggXVelocityMultiplier, ProjectileConfig.EggUpwardsVelocity);
            this.CurrentPosition = startPosition;
            this.Sprite = new SpinyEggSprite(this);
            this.CollisionResponder = new SpinyEggCollisionResponder(this);
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

        public void SetDead()
        {
            this.Sprite.SetDead();
            this.ShouldBeRemoved = true;
        }
    }
}