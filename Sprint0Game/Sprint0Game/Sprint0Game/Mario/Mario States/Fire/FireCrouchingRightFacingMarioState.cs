
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class FireCrouchingRightFacingMarioState : IMarioState
    {
        private IMario Mario;
        public IAnimatedMario Sprite { get; set; }

        public FireCrouchingRightFacingMarioState(IMario mario)
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
                    this.Mario.CurrentState = new FireCrouchingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoRight:
                    break;
                case MarioActionRequest.GoSmall:
                    this.Mario.CurrentState = new SmallCrouchingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoBig:
                    break;
                case MarioActionRequest.GoFire:
                    break;
                case MarioActionRequest.GoMetal:
                    this.Mario.CurrentState = new MetalCrouchingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.TakeDamage:
                    this.Mario.CurrentState = new BigCrouchingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoIdle:
                    this.Mario.CurrentState = new FireIdleRightFacingMarioState(this.Mario);
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
