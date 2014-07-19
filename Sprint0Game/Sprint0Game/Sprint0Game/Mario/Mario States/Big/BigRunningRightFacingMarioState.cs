
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class BigRunningRightFacingMarioState : IMarioState
    {
        private IMario Mario;
        public IAnimatedMario Sprite { get; set; }

        public BigRunningRightFacingMarioState(IMario mario)
        {
            this.Mario = mario;
            
            this.Sprite = AnimatedMarioFactory.RunningMario(this.Mario, this);
        }

        public MarioPowerLevel PowerLevel()
        {
            return MarioPowerLevel.Big;
        }

        public void RespondToRequest(MarioActionRequest change)
        {
            switch (change)
            {
                case MarioActionRequest.Jump:
                    this.Mario.CurrentState = new BigJumpingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.Crouch:
                    this.Mario.CurrentState = new BigCrouchingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoLeft:
                    this.Mario.CurrentState = new BigIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoRight:
                    break;
                case MarioActionRequest.GoSmall:
                    this.Mario.CurrentState = new SmallRunningRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoBig:
                    break;
                case MarioActionRequest.GoFire:
                    this.Mario.CurrentState = new FireRunningRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoMetal:
                    this.Mario.CurrentState = new MetalRunningRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.TakeDamage:
                    this.Mario.CurrentState = new SmallRunningRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoIdle:
                    this.Mario.CurrentState = new BigIdleRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.Fall:
                    this.Mario.CurrentState = new BigFallingRightFacingMarioState(this.Mario);
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
        
    }
}
