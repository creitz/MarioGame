
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class BigRunningLeftFacingMarioState : IMarioState
    {
        private IMario Mario;
        public IAnimatedMario Sprite { get; set; }

        public BigRunningLeftFacingMarioState(IMario mario)
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
                    this.Mario.CurrentState = new BigJumpingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.Crouch:
                    this.Mario.CurrentState = new BigCrouchingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoLeft:
                    break;
                case MarioActionRequest.GoRight:
                    this.Mario.CurrentState = new BigIdleRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoSmall:
                    this.Mario.CurrentState = new SmallRunningLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoBig:
                    break;
                case MarioActionRequest.GoFire:
                    this.Mario.CurrentState = new FireRunningLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoMetal:
                    this.Mario.CurrentState = new MetalRunningLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.TakeDamage:
                    this.Mario.CurrentState = new SmallRunningLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoIdle:
                    this.Mario.CurrentState = new BigIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.Fall:
                    this.Mario.CurrentState = new BigFallingLeftFacingMarioState(this.Mario);
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
        
    }
}
