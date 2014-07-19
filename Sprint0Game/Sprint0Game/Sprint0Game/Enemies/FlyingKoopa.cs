
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class FlyingKoopa : IEnemy
    {
        public Vector2 CurrentPosition { get; set; }  //bottom left
        public bool IsRightFacing { get; private set; }
        public Vector2 CurrentVelocity { get; set; }
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        public bool WillBecomeKoopa { get; set; }
        public bool IsDead { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        private FlyingKoopaSprite Sprite;
        private FlyingKoopaCollisionResponder CollisionResponder;

        public FlyingKoopa(Vector2 startPosition, bool rightFacing)
        {
            if (rightFacing)
            {
                this.GoRight();
            }
            else
            {
                this.GoLeft();
            }
            this.CurrentPosition = startPosition;
            this.Sprite = new FlyingKoopaSprite(this);
            this.CollisionResponder = new FlyingKoopaCollisionResponder(this);
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
            if (!this.IsDead)
                this.CollisionResponder.RespondToCollision(side, obj, intersectRect);
        }

        public void RespondToNoCollision()
        {
            if (!this.IsDead)
                Fall();
        }

        public void Fall()
        {
            this.CurrentVelocity = new Vector2(this.CurrentVelocity.X, this.CurrentVelocity.Y + PhysicsConfig.Gravity);
        }

        public void GoLeft()
        {
            this.IsRightFacing = false;
            this.CurrentVelocity = new Vector2(-EnemyConfig.FlyingKoopaRunVelocity, this.CurrentVelocity.Y);
        }

        public void GoRight()
        {
            this.IsRightFacing = true;
            this.CurrentVelocity = new Vector2(EnemyConfig.FlyingKoopaRunVelocity, this.CurrentVelocity.Y);
        }

        public void SetStomped()
        {
            this.Sprite.SetStomped();
            this.ShouldBeRemoved = true;
        }

        public void SetDead()
        {
            this.Sprite.SetDead();
            SoundBoard.Stomp.Play();
        }
    }
}