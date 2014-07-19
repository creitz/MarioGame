
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class Lakitu : IEnemy
    {
        public Vector2 CurrentPosition { get; set; }  //bottom left
        public bool IsRightFacing { get; private set; }
        public Vector2 CurrentVelocity { get; set; }
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        public bool IsDead { get; set; }
        public bool ShouldThrowEgg { get; set; }
        public int Height { get { return this.Sprite.Height; } }
        public int Width { get { return this.Sprite.Width; } }
        private LakituSprite Sprite;
        private LakituCollisionResponder CollisionResponder;
        private ILevel Level;

        public Lakitu(Vector2 startPosition, ILevel level)
        {
            this.CurrentPosition = startPosition;
            this.Sprite = new LakituSprite(this);
            this.CollisionResponder = new LakituCollisionResponder(this);
            this.Level = level;
        }

        public void Update()
        {
            if (!this.Level.Mario.IsUnderground) //don't want lakitu to follow him to undergound level
            {
                this.Sprite.Update();
                if (this.IsDead)
                {
                    this.Fall();
                    if (this.CurrentPosition.Y > this.Level.Window.Height)
                        this.ShouldBeRemoved = true;
                }
                else
                {
                    if (this.ShouldThrowEgg)
                    {
                        this.Level.Projectiles.Add(new SpinyEgg(this.CurrentPosition, this.Level.XDistanceFromMario(this)));
                        this.ShouldThrowEgg = false;
                    }
                    UpdateRightFacing();
                    this.CurrentVelocity = new Vector2(-EnemyConfig.LakituHorizontalVelocity * this.Level.XDistanceFromMario(this), this.CurrentVelocity.Y);
                }
            }
        }

        private void UpdateRightFacing()
        {
            if (this.Level.XDistanceFromMario(this) < 0)
            {
                this.IsRightFacing = true;
            }
            else
            {
                this.IsRightFacing = false;
            }
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
                this.CurrentVelocity = new Vector2(this.CurrentVelocity.X, 0f);
        }

        public void Fall()
        {
            this.CurrentVelocity = new Vector2(this.CurrentVelocity.X, this.CurrentVelocity.Y + PhysicsConfig.Gravity);
        }

        public void Rise()
        {
            this.CurrentVelocity = new Vector2(this.CurrentVelocity.X, this.CurrentVelocity.Y - EnemyConfig.LakituVerticalVelocity);
        }

        public void Kill()
        {
            this.Sprite.SetDead();
            SoundBoard.Stomp.Play();
        }
    }
}