
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class Mushroom : IItem
    {
        public Vector2 CurrentPosition { get; set; } //bottom left
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        public Vector2 CurrentVelocity { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        public bool Spawning { get; set; }
        public MushroomType Type { get; private set; }
        private MushroomSprite Sprite;
        private MushroomCollisionResponder CollisionResponder;

        public Mushroom(Vector2 startPosition, bool Spawning, MushroomType type)
        {
            this.Type = type;
            this.CurrentVelocity = new Vector2(ItemConfig.MushroomVelocity, 0);
            this.CurrentPosition = startPosition;
            this.Sprite = new MushroomSprite(this, type);
            this.CollisionResponder = new MushroomCollisionResponder(this);
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
