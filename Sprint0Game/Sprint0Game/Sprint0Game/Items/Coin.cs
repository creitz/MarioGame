
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class Coin : IItem
    {
        public Vector2 CurrentPosition { get; set; } //bottom left
        public Vector2 CurrentVelocity { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        public bool Spawning {get; set;}
        private CoinSprite Sprite;
        private CoinCollisionResponder CollisionResponder;

        public Coin(Vector2 startPosition, bool Spawning)
        {
            this.CurrentPosition = startPosition;
            this.Sprite = new CoinSprite(this, startPosition);
            this.CollisionResponder = new CoinCollisionResponder(this);
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
            this.CollisionResponder.RespondToCollision(obj);
        }

        public void RespondToNoCollision()
        {
            // Should remain stationary at all times until collected by Mario
        }

        public void Collect()
        {
            this.Sprite.SetCollected();
            this.ShouldBeRemoved = true;
        }
    }
}
