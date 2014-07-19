using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class FireballCollisionResponder
    {

        private Fireball Fireball;

        public FireballCollisionResponder(Fireball fireball)
        {
            this.Fireball = fireball;
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            if (obj is IEnemy)
            {
                RespondToCollisionWithEnemy((IEnemy)obj);
            }
            else if (obj is IProjectile)
            {
                RespondToCollisionWithProjectile(obj);
            }
            else if (obj is Block) 
            {
                Block block = (Block)obj;
                if (!(block.CurrentState is HiddenBlockState)) 
                {
                    RespondToCollisionWithBlockOrPipe(side, intersectRect);
                }
            } 
            else if (obj is IPipe) 
            {
                RespondToCollisionWithBlockOrPipe(side, intersectRect);
            }
            else
            {
                this.Fireball.Fall();
            }
        }

        private void RespondToCollisionWithEnemy(IEnemy enemy)
        {
            if (enemy.CurrentVelocity.Y < ProjectileConfig.FireballDisappearanceVelocity)
            {
                this.Fireball.SetGone();
            }
        }

        private void RespondToCollisionWithProjectile(IObject obj)
        {
            if (!(obj is Fireball))
            {
                this.Fireball.SetGone();
            }
            else if (this.Fireball.IsAffectedByGravity)
                this.Fireball.CurrentVelocity = new Vector2(this.Fireball.CurrentVelocity.X, 
                    this.Fireball.CurrentVelocity.Y + PhysicsConfig.Gravity);
        }

        private void RespondToCollisionWithBlockOrPipe(Side side, Rectangle intersectRect)
        {
            if (SideGeneralizer.IsBottom(side))
            {
                this.Fireball.CurrentPosition = new Vector2(this.Fireball.CurrentPosition.X, this.Fireball.CurrentPosition.Y - intersectRect.Height);
                this.Fireball.CurrentVelocity = new Vector2(this.Fireball.CurrentVelocity.X, ProjectileConfig.FireballBounceVelocity);
                this.Fireball.IsAffectedByGravity = true;
            }
            else if (SideGeneralizer.IsLeft(side))
            {
                this.Fireball.CurrentPosition = new Vector2(this.Fireball.CurrentPosition.X + (intersectRect.Width),
                    this.Fireball.CurrentPosition.Y);
                this.Fireball.CurrentVelocity = new Vector2(ProjectileConfig.FireballVelocity, this.Fireball.CurrentVelocity.Y);
            }
            else if (SideGeneralizer.IsRight(side))
            {
                this.Fireball.CurrentPosition = new Vector2(this.Fireball.CurrentPosition.X - (intersectRect.Width),
                    this.Fireball.CurrentPosition.Y);
                this.Fireball.CurrentVelocity = new Vector2(-ProjectileConfig.FireballVelocity, this.Fireball.CurrentVelocity.Y);
            }
            else if (SideGeneralizer.IsTop(side))
            {
                if (Fireball.IsAffectedByGravity)
                {
                    this.Fireball.CurrentPosition = new Vector2(this.Fireball.CurrentPosition.X, this.Fireball.CurrentPosition.Y + intersectRect.Height);
                    this.Fireball.CurrentVelocity = new Vector2(this.Fireball.CurrentVelocity.X, 0f);
                }
            }
        }
    }
}
