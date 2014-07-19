
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class FireIdleRightFacingMarioState : IMarioState
    {
        private IMario Mario;
        public IAnimatedMario Sprite { get; set; }

        public FireIdleRightFacingMarioState(IMario mario)
        {
            this.Mario = mario; 
            this.Sprite = AnimatedMarioFactory.IdleMario(this.Mario, this);
        }

        public void RespondToRequest(MarioActionRequest change)
        {
            switch (change)
            {
                case MarioActionRequest.Jump:
                    this.Mario.CurrentState = new FireJumpingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.Crouch:
                    this.Mario.CurrentState = new FireCrouchingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoLeft:
                    this.Mario.CurrentState = new FireIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoRight:
                    this.Mario.CurrentState = new FireRunningRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoSmall:
                    this.Mario.CurrentState = new SmallIdleRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoBig:
                    break;
                case MarioActionRequest.GoFire:
                    break;
                case MarioActionRequest.GoMetal:
                    this.Mario.CurrentState = new MetalIdleRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.TakeDamage:
                    this.Mario.CurrentState = new BigIdleRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoIdle:
                    break;
                case MarioActionRequest.Fall:
                    this.Mario.CurrentState = new FireFallingRightFacingMarioState(this.Mario);
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
            return MarioPowerLevel.Fire;
        }
    }
}
