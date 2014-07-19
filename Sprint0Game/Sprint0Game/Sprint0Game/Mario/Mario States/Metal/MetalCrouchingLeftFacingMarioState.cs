
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class MetalCrouchingLeftFacingMarioState : IMarioState
    {
        private IMario Mario;
        public IAnimatedMario Sprite { get; set; }

        public MetalCrouchingLeftFacingMarioState(IMario mario)
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
                    this.Mario.CurrentState = new MetalCrouchingRightFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoSmall:
                    this.Mario.CurrentState = new SmallCrouchingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoBig:
                    this.Mario.CurrentState = new BigCrouchingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoFire:
                    this.Mario.CurrentState = new FireCrouchingLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.GoMetal:
                    break;
                case MarioActionRequest.TakeDamage:
                    break;
                case MarioActionRequest.GoIdle:
                    this.Mario.CurrentState = new MetalIdleLeftFacingMarioState(this.Mario);
                    break;
                case MarioActionRequest.Fall:
                    this.Mario.CurrentState = new MetalFallingLeftFacingMarioState(this.Mario);
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

        public bool IsRightFacing() {return false;}
        public MarioPowerLevel PowerLevel()
        {
            return MarioPowerLevel.Metal;
        }
    }
}
