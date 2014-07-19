
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class FireFlower : IItem
    {
        public Vector2 CurrentPosition { get; set; } //bottom left
        public Vector2 CurrentVelocity { get; set; }
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        public bool Spawning { get; set; }
        private FireFlowerSprite Sprite;
        private FireFlowerCollisionResponder CollisionResponder;

        public FireFlower(Vector2 startPosition, bool Spawning)
        {
            this.CurrentPosition = startPosition;
            this.Sprite = new FireFlowerSprite(this);
            this.CollisionResponder = new FireFlowerCollisionResponder(this);
            this.Spawning = Spawning;
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

        public void Collect() 
        {
            this.Sprite.SetCollected();
            this.ShouldBeRemoved = true;
        }
    }
}
