
using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public class FallingMarioStateResponder : IMarioStateResponder
    {
        private IMario Mario;

        public FallingMarioStateResponder(IMario mario)
        {
            this.Mario = mario;
        }

        public void TakeUpInput() { }

        public void TakeDownInput(bool isOnTransitionPipe) { }

        public void TakeLeftInput(bool TouchingTransPipe)
        {
            this.Mario.CurrentPosition = new Vector2(this.Mario.CurrentPosition.X - MarioConfig.SideSpeed, 
                this.Mario.CurrentPosition.Y);
        }

        public void TakeRightInput(bool TouchingTransPipe)
        {
            this.Mario.CurrentPosition = new Vector2(this.Mario.CurrentPosition.X + MarioConfig.SideSpeed, 
                this.Mario.CurrentPosition.Y);
        }

        public void TakeNoInput() { }

        public void TakeUpReleased() { }

        public void RespondToNoCollision() { }
    }
}
