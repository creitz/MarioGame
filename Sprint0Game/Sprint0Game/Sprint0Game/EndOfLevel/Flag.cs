using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class Flag : IObject
    {
        public Vector2 CurrentPosition { get; set; }
        public Vector2 FlagCurrentPosition { get; set; }
        public Vector2 CurrentVelocity {get; set; }
        public bool ShouldBeRemoved { get; set; }
        public bool LevelComplete { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        public int FlagHeight { get { return this.Sprite.FlagHeight; } }
        public bool HasBeenReached { get; set; }
        private FlagSprite Sprite;
        private FlagCollisionResponder CollisionResponder;

        public Flag(Vector2 postion)
        {
            this.HasBeenReached = true;
            this.CurrentPosition = postion;
            this.CurrentVelocity= new Vector2(0,0);
            this.Sprite = new FlagSprite(this);
            this.CollisionResponder = new FlagCollisionResponder(this);
            this.FlagCurrentPosition = new Vector2(this.CurrentPosition.X - this.Sprite.FlagWidth / 2, 
                this.CurrentPosition.Y + SpriteHolder.DistanceFromTopOfPoleToFlag);
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
            this.CollisionResponder.RespondToCollision(obj, intersectRect);
        }

        public void RespondToNoCollision() { }

        public void Remove()
        {
            this.Sprite.RemoveFlag();
        }
    }
}
