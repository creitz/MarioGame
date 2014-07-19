
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class Mario : IMario
    {
        public IMarioState CurrentState { get; set; }
        public Vector2 CurrentPosition { get; set; } //bottom left corner
        public Vector2 CurrentVelocity { get; set; }
        public int Height { get { return this.CurrentState.Sprite.Height; } }
        public int Width { get { return this.CurrentState.Sprite.Width; } }
        public bool IsTransitioningFromDamage
        {
            get { return this.TransitioningFromDamage; }
            set
            {
                this.TransitioningFromDamage = value;
                if (value)
                    this.TransitionFromDamageTimer = MarioConfig.TransitioningFromDamageDuration;
            }
        }
        public bool IsTravelingPipe { get; set; }
        public bool IsUnderground { get; set; }
        public bool IsTransitioningFromItem { get; set; }
        public bool IsBouncing { get; set; }
        public bool IsStar
        {
            get { return this.Star; }
            set
            {
                this.Star = value;
                if (value)
                    this.StarTimer = MarioConfig.StarDuration;
            }
        }
        public bool LeftInputEnabled { get; set; }
        public bool RightInputEnabled { get; set; }
        public int TransitionFromDamageTimer { get; set; }
        public int MetalTimer { get; set; }
        public bool ShouldBeRemoved { get; set; }
        public bool HasBeenReached { get; set; }
        public bool OnTransPipe { get; set; }
        public bool TouchingTransPipe { get; set; }
        public ILevel Level { get; private set; }
        private bool TransitioningFromDamage;
        private int StarTimer;
        private bool Star;
        private MarioCollisionResponder CollisionResponder;
        private MarioAllStateResponder StateResponder;

        public Mario(Vector2 startPosition, ILevel level)
        {
            this.Level = level;
            this.CurrentPosition = startPosition;
            this.CurrentState = new SmallIdleRightFacingMarioState(this);
            this.CollisionResponder = new MarioCollisionResponder(this);
            this.StateResponder = new MarioAllStateResponder(this);
            this.CurrentVelocity = new Vector2(0, 0);
            this.Star = false;
        }

        public bool IsBig() { return MarioPowerLevelGeneralizer.IsBig(this.CurrentState.PowerLevel()); }
        public bool IsFire() { return this.CurrentState.PowerLevel() == MarioPowerLevel.Fire; }
        public bool IsRightFacing() { return this.CurrentState.IsRightFacing(); }

        public bool IsCrouching()
        {
            IMarioState state = this.CurrentState;
            return (state is BigCrouchingLeftFacingMarioState) || (state is BigCrouchingRightFacingMarioState)
                || (state is SmallCrouchingLeftFacingMarioState) || (state is SmallCrouchingRightFacingMarioState)
                || (state is FireCrouchingLeftFacingMarioState) || (state is FireCrouchingRightFacingMarioState)
                || (state is MetalCrouchingLeftFacingMarioState) || (state is MetalCrouchingRightFacingMarioState);
        }

        public bool IsDying() { return this.CurrentState is DyingMarioState; }

        public bool IsDead() { return this.CurrentState is DeadMarioState; }

        public bool IsFalling()
        {
            IMarioState state = this.CurrentState;
            return (state is BigFallingLeftFacingMarioState) || (state is BigFallingRightFacingMarioState)
                || (state is SmallFallingLeftFacingMarioState) || (state is SmallFallingRightFacingMarioState)
                || (state is FireFallingLeftFacingMarioState) || (state is FireFallingRightFacingMarioState)
                || (state is MetalFallingLeftFacingMarioState) || (state is MetalFallingRightFacingMarioState);
        }

        public bool IsIdle()
        {
            IMarioState state = this.CurrentState;
            return (state is BigIdleLeftFacingMarioState) || (state is BigIdleRightFacingMarioState)
                || (state is SmallIdleLeftFacingMarioState) || (state is SmallIdleRightFacingMarioState)
                || (state is FireIdleLeftFacingMarioState) || (state is FireIdleRightFacingMarioState)
                || (state is MetalIdleLeftFacingMarioState) || (state is MetalIdleRightFacingMarioState);
        }

        public bool IsJumping()
        {
            IMarioState state = this.CurrentState;
            return (state is BigJumpingLeftFacingMarioState) || (state is BigJumpingRightFacingMarioState)
                || (state is SmallJumpingLeftFacingMarioState) || (state is SmallJumpingRightFacingMarioState)
                || (state is FireJumpingLeftFacingMarioState) || (state is FireJumpingRightFacingMarioState)
                || (state is MetalJumpingLeftFacingMarioState) || (state is MetalJumpingRightFacingMarioState);
        }

        public bool IsRunning()
        {
            IMarioState state = this.CurrentState;
            return (state is BigRunningLeftFacingMarioState) || (state is BigRunningRightFacingMarioState)
                || (state is SmallRunningLeftFacingMarioState) || (state is SmallRunningRightFacingMarioState)
                || (state is FireRunningLeftFacingMarioState) || (state is FireRunningRightFacingMarioState)
                || (state is MetalRunningLeftFacingMarioState) || (state is MetalRunningRightFacingMarioState);
        }

        public void Update()
        {
            UpdateMetal();
            UpdateStar();
            UpdateTransitioning();
            this.CurrentState.Update();
        }

        private void UpdateMetal()
        {
            if (this.MetalTimer == MarioConfig.MetalDuration) 
            {
                MarioConfig.SetMetalMarioSpeed();
            }
            if (this.MetalTimer > 0)
            {
                this.MetalTimer--;
            }
            if (this.MetalTimer <= 0 && this.CurrentState.PowerLevel() == MarioPowerLevel.Metal)
            {
                this.RespondToRequest(MarioActionRequest.GoFire);
                MarioConfig.SetNormalMarioSpeed();
            }
        }

        private void UpdateTransitioning()
        {
            if (this.TransitioningFromDamage)
            {
                this.TransitionFromDamageTimer--;
            }
            if (this.TransitionFromDamageTimer < 0)
            {
                this.TransitioningFromDamage = false;
            }
        }

        public void Flash(int timer)
        {
            this.CurrentState.Sprite.Flash(timer);
        }

        private void UpdateStar()
        {
            if (this.Star)
            {
                this.StarTimer--;
            }
            if (this.StarTimer < 0)
            {
                this.Star = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            this.CurrentState.Draw(spriteBatch, camera);
        }

        public void RespondToRequest(MarioActionRequest req)
        {
            this.CurrentState.RespondToRequest(req);
        }

        public void TakeLeftInput()
        {
            if (this.LeftInputEnabled)
            {
                this.StateResponder.TakeLeftInput(TouchingTransPipe);
            }
            this.RightInputEnabled = true;
        }

        public void TakeRightInput()
        {
            if (this.RightInputEnabled)
            {
                this.StateResponder.TakeRightInput(TouchingTransPipe);
            } this.LeftInputEnabled = true;
        }

        public void TakeUpInput()
        {
            this.StateResponder.TakeUpInput();
        }

        public void TakeDownInput()
        {
            this.StateResponder.TakeDownInput(OnTransPipe);
        }

        public void TakeNoInput()
        {
            this.StateResponder.TakeNoInput();
        }

        public void TakeUpReleased()
        {
            this.StateResponder.TakeUpReleased();
        }

        public void EnableAllInput()
        {
            this.LeftInputEnabled = true;
            this.RightInputEnabled = true;
        }

        public void DisableAllInput()
        {
            this.LeftInputEnabled = false;
            this.RightInputEnabled = false;
        }

        public void RespondToCollision(Side side, IObject obj, Rectangle intersectRect)
        {
            this.CollisionResponder.RespondToCollision(side, obj, intersectRect);
        }

        public void RespondToNoCollision()
        {
            this.StateResponder.RespondToNoCollision();
        }

        public MarioPowerLevel PowerLevel()
        {
            return this.CurrentState.PowerLevel();
        }
    }
}
