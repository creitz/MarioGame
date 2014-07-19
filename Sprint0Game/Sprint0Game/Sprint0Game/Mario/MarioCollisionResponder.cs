
using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class MarioCollisionResponder
    {
        private Mario Mario;

        public MarioCollisionResponder(Mario mario)
        {
            this.Mario = mario;
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            if (obj is IEnemy)
            {
                this.RespondToCollisionWithEnemy(side, intersectRect, (IEnemy)obj);
            }
            else if (obj is IItem)
            {
                this.RespondToCollisionWithItem(obj);
            }
            else if (obj is Shell)
            {
                this.RespondToCollisionWithShell(side, obj, intersectRect);
            }
            else if (obj is SpinyEgg)
            {
                this.RespondToCollisionWithSpinyEgg(obj);
            }
            else if (obj is IPipe)
            {
                this.RespondToCollisionWithPipe(side, intersectRect);
            }
            else if (obj is Block)
            {
                this.RespondToCollisionWithBlock(side, (Block)obj, intersectRect);
            }
        }

        private void RespondToCollisionWithEnemy(Side side, Rectangle intersectRect, IEnemy enemy) 
        {
            if (this.ShouldTakeDamageFromEnemy(side, enemy))
            {
                SoundBoard.PowerDown.Play();
                this.Mario.RespondToRequest(MarioActionRequest.TakeDamage);
                this.Mario.IsTransitioningFromDamage = true;
            }
            else
            {
                if (!this.Mario.IsTransitioningFromDamage)
                    this.GivePointsForEnemy(side, enemy);

                this.AdjustPositionForEnemy(side, enemy, intersectRect);
            }
        }

        private void GivePointsForEnemy(Side side, IEnemy enemy)
        {
            GameStats.Points += PointsConfig.EnemyKill;
            if (this.ShouldBounceOffEnemy(side, enemy))
            {
                if (this.Mario.IsBouncing)
                {
                    GameStats.Points += PointsConfig.EnemyBounceKillExtra;
                }
                this.Mario.IsBouncing = true;
            }
        }

        private void AdjustPositionForEnemy(Side side, IEnemy enemy, Rectangle intersectRect)
        {
            if (this.ShouldBounceOffEnemy(side, enemy))
            {
                this.Mario.CurrentPosition = new Vector2(this.Mario.CurrentPosition.X, this.Mario.CurrentPosition.Y - intersectRect.Height);
                this.Mario.CurrentVelocity = new Vector2(this.Mario.CurrentVelocity.X, MarioConfig.BounceOffEnemiesVelocity);
            }
        }

        private bool ShouldTakeDamageFromEnemy(Side side, IEnemy enemy)
        {
            if (enemy is Spiny)
            {
                return !SideGeneralizer.IsTop(side) && !this.Mario.IsStar &&
                    this.Mario.PowerLevel() != MarioPowerLevel.Metal && !this.Mario.IsTransitioningFromDamage && !enemy.IsDead;
            }
            else if (enemy is Lakitu)
            {
                return false;
            }
            else
            {
                if (this.Mario.IsFalling())
                {
                    return !(SideGeneralizer.IsBottom(side) || side == Side.BottomOfLeft || side == Side.BottomOfRight) && !this.Mario.IsStar &&
                    this.Mario.PowerLevel() != MarioPowerLevel.Metal && !this.Mario.IsTransitioningFromDamage && !enemy.IsDead;
                }
                else
                {
                    return !SideGeneralizer.IsBottom(side) && !this.Mario.IsStar &&
                        this.Mario.PowerLevel() != MarioPowerLevel.Metal && !this.Mario.IsTransitioningFromDamage && !enemy.IsDead;
                }
            }

        }

        private bool ShouldBounceOffEnemy(Side side, IEnemy enemy)
        {
            return SideGeneralizer.IsBottom(side) && !this.Mario.IsStar && this.Mario.IsFalling() && !enemy.IsDead;
        }

        private void RespondToCollisionWithItem(IObject obj)
        {
            if ((obj as Mushroom)!=null && !(obj as Mushroom).Spawning)
            {
                if ((obj as Mushroom).Type == MushroomType.Green)
                {
                    GameStats.Lives++;
                    GameStats.Points += PointsConfig.GreenMushroom;
                }
                else if ((obj as Mushroom).Type == MushroomType.Red)
                {
                    if (!(this.Mario.IsFire() || this.Mario.IsBig()))
                        this.Mario.IsTransitioningFromItem = true;
                    this.Mario.RespondToRequest(MarioActionRequest.GoBig);
                    GameStats.Points += PointsConfig.RedMushroom;
                }
                else if ((obj as Mushroom).Type == MushroomType.Metal && !this.Mario.IsStar)
                {
                    this.Mario.MetalTimer = MarioConfig.MetalDuration;
                    this.Mario.IsTransitioningFromItem = true;
                    this.Mario.RespondToRequest(MarioActionRequest.GoMetal);
                }
            }
            else if (obj is Coin)
            {
                GameStats.Coins++;
                GameStats.Points += PointsConfig.Coin;
            }
            else if ((obj as FireFlower) != null && !(obj as FireFlower).Spawning)
            {
                if (!this.Mario.IsFire())
                {
                    SoundBoard.PowerUp.Play();
                    this.Mario.IsTransitioningFromItem = true;
                    this.Mario.RespondToRequest(MarioActionRequest.GoFire);
                }
                GameStats.Points += PointsConfig.FireFlower;
            }
            else if ((obj as Star) != null && ShouldGoStar((Star)obj))
            {
                this.Mario.IsStar = true;
                GameStats.Points += PointsConfig.Star;
            }
        }

        private bool ShouldGoStar(Star star)
        {
            return !star.Spawning && this.Mario.PowerLevel() != MarioPowerLevel.Metal;
        }

        private void RespondToCollisionWithShell(Side side, IObject obj, Rectangle intersectRectangle)
        {
            if (this.ShouldTakeDamageFromShell(side, obj))
            {
                SoundBoard.PowerDown.Play();
                this.Mario.RespondToRequest(MarioActionRequest.TakeDamage);
                
                this.Mario.IsTransitioningFromDamage = true;
            }
            else if (ShouldBounceOffShell(side))
            {
                this.Mario.CurrentPosition = new Vector2(this.Mario.CurrentPosition.X, this.Mario.CurrentPosition.Y - intersectRectangle.Height);
                this.Mario.CurrentVelocity = new Vector2(this.Mario.CurrentVelocity.X, MarioConfig.BounceOffEnemiesVelocity);
            }
        }

        private bool ShouldBounceOffShell(Side side)
        {
            return SideGeneralizer.IsBottom(side) && !this.Mario.IsStar;
        }

        private bool ShouldTakeDamageFromShell(Side side, IObject obj)
        {
            Shell shell = (Shell)obj;
            return shell.IsMovingHorizontally() && !(SideGeneralizer.IsBottom(side)) && !this.Mario.IsStar && !this.Mario.IsTransitioningFromDamage;
        }

        private void RespondToCollisionWithSpinyEgg(IObject obj)
        {
            if (ShouldTakeDamageFromSpinyEgg(obj))
            {
                this.Mario.RespondToRequest(MarioActionRequest.TakeDamage);
                this.Mario.IsTransitioningFromDamage = true;
            }
        }

        private bool ShouldTakeDamageFromSpinyEgg(IObject obj)
        {
            return !this.Mario.IsStar && !this.Mario.IsTransitioningFromDamage && this.Mario.PowerLevel() != MarioPowerLevel.Metal && obj.CurrentVelocity.Y >= 0;
        }

        private void RespondToCollisionWithBlock(Side side, Block block, Rectangle intersectRect)
        {
            if (BlockIsThere(block))
            {
                if (SideGeneralizer.IsTop(side) && !WillMoveThroughBlock(block))
                {
                    this.RespondToTop(intersectRect);
                }
                else if (ShouldRespondToSide(side, intersectRect) && !WillMoveThroughBlock(block))
                {
                    this.RespondToSide(side, intersectRect);
                }
                else if (SideGeneralizer.IsBottom(side))
                {
                    this.RespondToBottom(intersectRect);
                }
            }
        }

        private static bool BlockIsThere(Block block)
        {
            return !block.IsHidden() && !block.IsDestroyed();
        }

        private void RespondToCollisionWithPipe(Side side, Rectangle intersectRect)
        {
            if (ShouldRespondToSide(side, intersectRect))
            {
                this.RespondToSide(side, intersectRect);
            }

            else if (SideGeneralizer.IsBottom(side))
            {
                this.RespondToBottom(intersectRect);
            }

            else if (SideGeneralizer.IsTop(side))
            {
                this.RespondToTop(intersectRect);
            }
        }

        private bool WillMoveThroughBlock(Block block)
        {
            return (block.CurrentState is BrickBlockState) && 
                this.Mario.PowerLevel() == MarioPowerLevel.Metal;
        }

        private static bool ShouldRespondToSide(Side side, Rectangle intersectRect)
        {
            return (intersectRect.Width / MarioConfig.SideSpeed + MarioConfig.SideSpeed / 2 > intersectRect.Height ||
                SideGeneralizer.IsLeft(side) || SideGeneralizer.IsRight(side));
        }

        private void RespondToSide(Side side, Rectangle intersectRect)
        {
            if (SideGeneralizer.IsLeft(side))
            {
                this.RespondToLeft(intersectRect);
            }
            else if (SideGeneralizer.IsRight(side))
            {
                this.RespondToRight(intersectRect);
            }
        }

        private void RespondToLeft(Rectangle intersectRect)
        {
            this.Mario.CurrentPosition = new Vector2(this.Mario.CurrentPosition.X + (intersectRect.Width),
                    this.Mario.CurrentPosition.Y);
            if (this.Mario.IsFalling())
            {
                this.Mario.LeftInputEnabled = false;
            }
        }

        private void RespondToRight(Rectangle intersectRect)
        {
            this.Mario.CurrentPosition = new Vector2(this.Mario.CurrentPosition.X - intersectRect.Width,
                    this.Mario.CurrentPosition.Y);
            if (this.Mario.IsFalling())
            {
                this.Mario.RightInputEnabled = false;
            }
        }

        private void RespondToBottom(Rectangle intersectRect)
        {
            this.Mario.IsBouncing = false;
            this.Mario.CurrentPosition = new Vector2(this.Mario.CurrentPosition.X,
                this.Mario.CurrentPosition.Y - (intersectRect.Height - MarioConfig.BottomCollisionTolerance));
            if (this.Mario.IsFalling())
            {
                this.Mario.RespondToRequest(MarioActionRequest.GoIdle);
                this.Mario.EnableAllInput();
            }
        }

        private void RespondToTop(Rectangle intersectRect)
        {
            this.Mario.CurrentVelocity = new Vector2(this.Mario.CurrentVelocity.X, 0);
            this.Mario.CurrentPosition = new Vector2(this.Mario.CurrentPosition.X,
                this.Mario.CurrentPosition.Y + intersectRect.Height);
            this.Mario.RespondToRequest(MarioActionRequest.Fall);
        }
    }
}
