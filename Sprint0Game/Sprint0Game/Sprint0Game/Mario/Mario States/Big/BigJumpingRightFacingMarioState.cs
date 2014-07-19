
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class BigJumpingRightFacingMarioState : IMarioState
    {
        private IMario Mario;
        public IAnimatedMario Sprite { get; set; }

        public BigJumpingRightFacingMarioState(IMario mario)
        {
            this.Mario = mario;
            
            this.Sprite = AnimatedMarioFactory.JumpingMario(this.Mario, this);
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
                    break;
                case MarioActionRequest.Crouch:
                    break;
                case MarioActionRequest.GoLeft:
                    this.Mario.CurrentState = new BigJumpingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoRight:
                    break;
                case MarioActionRequest.GoSmall:
                    this.Mario.CurrentState = new SmallJumpingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoBig:
                    break;
                case MarioActionRequest.GoFire:
                    this.Mario.CurrentState = new FireJumpingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoMetal:
                    this.Mario.CurrentState = new MetalJumpingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.TakeDamage:
                    this.Mario.CurrentState = new SmallJumpingRightFacingMarioState(this.Mario);
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
