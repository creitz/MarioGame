
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class FireCrouchingLeftFacingMarioState : IMarioState
    {
        private IMario Mario;
        public IAnimatedMario Sprite { get; set; }

        public FireCrouchingLeftFacingMarioState(IMario mario)
        {
            this.Mario = mario;
            this.Sprite = AnimatedMarioFactory.CrouchingMario(this.Mario, this);
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
                    break;
                case MarioActionRequest.GoRight:
                    this.Mario.CurrentState = new FireCrouchingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoSmall:
                    this.Mario.CurrentState = new SmallCrouchingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoBig:
                    break;
                case MarioActionRequest.GoFire:
                    break;
                case MarioActionRequest.GoMetal:
                    this.Mario.CurrentState = new MetalCrouchingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.TakeDamage:
                    this.Mario.CurrentState = new BigCrouchingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoIdle:
                    this.Mario.CurrentState = new FireIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.Fall:
                    this.Mario.CurrentState = new FireFallingLeftFacingMarioState(this.Mario);
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
            return MarioPowerLevel.Fire;
        }
    }
}
