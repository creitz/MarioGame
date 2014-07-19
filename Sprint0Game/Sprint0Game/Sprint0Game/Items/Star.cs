
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class Star : IItem
    {
        public Vector2 CurrentPosition { get; set; } //bottom left
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        public Vector2 CurrentVelocity { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        public bool Spawning { get; set; }
        private StarSprite Sprite;
        private StarCollisionResponder CollisionResponder;

        public Star(Vector2 startPosition, bool Spawning)
        {
            this.Spawning = Spawning;
            this.CurrentVelocity = new Vector2(ItemConfig.StarVelocity, 0);
            this.CurrentPosition = startPosition;
            this.Sprite = new StarSprite(this);
            this.CollisionResponder = new StarCollisionResponder(this);
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
            this.Fall();
        }

        public void Fall()
        {
            this.CurrentVelocity = new Vector2(this.CurrentVelocity.X, this.CurrentVelocity.Y + PhysicsConfig.Gravity);
        }

        public void Collect()
        {
            this.Sprite.SetCollected();
            this.ShouldBeRemoved = true;
        }
    }
}
