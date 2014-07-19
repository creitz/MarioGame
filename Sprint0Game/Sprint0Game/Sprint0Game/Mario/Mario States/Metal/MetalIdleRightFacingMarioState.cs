
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class MetalIdleRightFacingMarioState : IMarioState
    {
        private IMario Mario;
        public IAnimatedMario Sprite { get; set; }

        public MetalIdleRightFacingMarioState(IMario mario)
        {
            this.Mario = mario;
            this.Sprite = AnimatedMarioFactory.IdleMario(this.Mario, this);
        }

        public void RespondToRequest(MarioActionRequest change)
        {
            switch (change)
            {
                case MarioActionRequest.Jump:
                    this.Mario.CurrentState = new MetalJumpingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.Crouch:
                    this.Mario.CurrentState = new MetalCrouchingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoLeft:
                    this.Mario.CurrentState = new MetalIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoRight:
                    this.Mario.CurrentState = new MetalRunningRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoSmall:
                    this.Mario.CurrentState = new SmallIdleRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoBig:
                    this.Mario.CurrentState = new BigIdleRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoFire:
                    this.Mario.CurrentState = new FireIdleRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoMetal:
                    break;
                case MarioActionRequest.TakeDamage:
                    break;
                case MarioActionRequest.GoIdle:
                    this.Mario.CurrentState = new MetalIdleRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.Fall:
                    this.Mario.CurrentState = new MetalFallingRightFacingMarioState(this.Mario);
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
            return MarioPowerLevel.Metal;
        }
    }
}
