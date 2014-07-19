
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class SmallIdleLeftFacingMarioState : IMarioState
    {
        private IMario Mario;
        public IAnimatedMario Sprite { get; set; }

        public SmallIdleLeftFacingMarioState(IMario mario)
        {
            this.Mario = mario;
            this.Sprite = AnimatedMarioFactory.IdleMario(this.Mario, this);
        }

        public void RespondToRequest(MarioActionRequest change)
        {
            switch (change)
            {
                case MarioActionRequest.Jump:
                    this.Mario.CurrentState = new SmallJumpingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.Crouch:
                    //this.Mario.CurrentState = new SmallCrouchingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoLeft:
                    this.Mario.CurrentState = new SmallRunningLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoRight:
                    this.Mario.CurrentState = new SmallIdleRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoSmall:
                    break;
                case MarioActionRequest.GoBig:
                    this.Mario.CurrentState = new BigIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoFire:
                    this.Mario.CurrentState = new FireIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoMetal:
                    this.Mario.CurrentState = new MetalIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.TakeDamage:
                    this.Mario.CurrentState = new DyingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoIdle:
                    break;
                case MarioActionRequest.Fall:
                    this.Mario.CurrentState = new SmallFallingLeftFacingMarioState(this.Mario);
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

        public bool IsRightFacing() { return false; }
        public MarioPowerLevel PowerLevel()
        {
            return MarioPowerLevel.Small;
        }
    }
}
