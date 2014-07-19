
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class Goomba : IEnemy
    {
        public Vector2 CurrentPosition { get; set; } //bottom left
        public Vector2 CurrentVelocity { get; set; }
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        public bool IsDead { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        private int GoombaDeathTimer = 0;
        private GoombaSprite Sprite;
        private GoombaCollisionResponder CollisionResponder;

        public Goomba(Vector2 startPostion, bool rightFacing)
        {
            if (rightFacing)
            {
                this.CurrentVelocity = new Vector2(EnemyConfig.GoombaRunVelocity, 0);
            }
            else
            {
                this.CurrentVelocity = new Vector2(-EnemyConfig.GoombaRunVelocity, 0);
            }
            this.CurrentPosition = startPostion;
            this.Sprite = new GoombaSprite(this);
            this.CollisionResponder = new GoombaCollisionResponder(this);
        }

        public void Update()
        {
            this.Sprite.Update();
            if (GoombaDeathTimer > 0)
            {
                GoombaDeathTimer--;
                if (GoombaDeathTimer == 0)
                    this.ShouldBeRemoved = true;
            }
            if (this.CurrentPosition.Y > 500)
                this.ShouldBeRemoved = true;
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
            if(!this.IsDead)
                Fall();
        }

        public void Fall()
        {
            this.CurrentVelocity = new Vector2(this.CurrentVelocity.X, this.CurrentVelocity.Y + PhysicsConfig.Gravity);
        }

        public void SetStomped()
        {
            this.Sprite.SetStomped();
            this.GoombaDeathTimer = EnemyConfig.GoombaDeathTimer;
        }

        public void Kill()
        {
            this.Sprite.SetDead();
            SoundBoard.Stomp.Play();
        }

        public void GoLeft()
        {
            this.Sprite.GoLeft();
        }

        public void GoRight()
        {
            this.Sprite.GoRight();
        }
    }
}
