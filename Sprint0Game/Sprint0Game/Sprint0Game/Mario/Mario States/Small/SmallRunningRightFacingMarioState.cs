
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class SmallRunningRightFacingMarioState : IMarioState
    {
        private IMario Mario;
        public IAnimatedMario Sprite { get; set; }

        public SmallRunningRightFacingMarioState(IMario mario)
        {
            this.Mario = mario;
            this.Sprite = AnimatedMarioFactory.RunningMario(this.Mario, this);
        }

        public void RespondToRequest(MarioActionRequest change)
        {
            switch (change)
            {
                case MarioActionRequest.Jump:
                    this.Mario.CurrentState = new SmallJumpingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.Crouch:
                    //this.Mario.CurrentState = new SmallCrouchingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoLeft:
                    this.Mario.CurrentState = new SmallIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoRight:
                    break;
                case MarioActionRequest.GoSmall:
                    break;
                case MarioActionRequest.GoBig:
                    this.Mario.CurrentState = new BigRunningRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoFire:
                    this.Mario.CurrentState = new FireRunningRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoMetal:
                    this.Mario.CurrentState = new MetalRunningRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.TakeDamage:
                    this.Mario.CurrentState = new DyingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoIdle:
                    this.Mario.CurrentState = new SmallIdleRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.Fall:
                    this.Mario.CurrentState = new SmallFallingRightFacingMarioState(this.Mario);
                    break;
            }
        }

        public void Update()
        {
            this.Sprite.Update();
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
            this.Sprite.Draw(spriteBatch, camera);
        }

        public bool IsRightFacing() { return true; }
        public MarioPowerLevel PowerLevel()
        {
            return MarioPowerLevel.Small;
        }
    }
}
