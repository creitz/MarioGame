
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class BigIdleLeftFacingMarioState : IMarioState
    {
        private IMario Mario;
        public IAnimatedMario Sprite { get; set; }

        public BigIdleLeftFacingMarioState(IMario mario)
        {
            this.Mario = mario;
            this.Sprite = AnimatedMarioFactory.IdleMario(this.Mario, this);
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
                    this.Mario.CurrentState = new BigRunningLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoRight:
                    this.Mario.CurrentState = new BigIdleRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoSmall:
                    this.Mario.CurrentState = new SmallIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoBig:
                    break;
                case MarioActionRequest.GoFire:
                    this.Mario.CurrentState = new FireIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoMetal:
                    this.Mario.CurrentState = new MetalIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.TakeDamage:
                    this.Mario.CurrentState = new SmallIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoIdle:
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
