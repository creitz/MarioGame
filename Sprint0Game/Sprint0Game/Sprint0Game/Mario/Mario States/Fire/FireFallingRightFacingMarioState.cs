
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class FireFallingRightFacingMarioState : IMarioState
    {
        private IMario Mario;
        public IAnimatedMario Sprite { get; set; }

        public FireFallingRightFacingMarioState(IMario mario)
        {
            this.Mario = mario;
            this.Sprite = AnimatedMarioFactory.FallingMario(this.Mario, this);
        }

        public void RespondToRequest(MarioActionRequest change)
        {
            switch (change)
            {
                case MarioActionRequest.Jump:
                    break;
                case MarioActionRequest.Crouch:
                    this.Mario.CurrentState = new FireCrouchingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoLeft:
                    this.Mario.CurrentState = new FireFallingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoRight:
                    break;
                case MarioActionRequest.GoSmall:
                    this.Mario.CurrentState = new SmallFallingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoBig:
                    break;
                case MarioActionRequest.GoFire:
                    break;
                case MarioActionRequest.GoMetal:
                    this.Mario.CurrentState = new MetalFallingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.TakeDamage:
                    this.Mario.CurrentState = new BigFallingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoIdle:
                    this.Mario.CurrentState = new FireIdleRightFacingMarioState(this.Mario);
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
