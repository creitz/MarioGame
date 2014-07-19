
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class IdleMarioStateResponder : IMarioStateResponder
    {
        private IMario Mario;

        public IdleMarioStateResponder(IMario mario)
        {
            this.Mario = mario;
        }

        public void TakeUpInput()
        {
            SoundBoard.JumpSound.Play();
            this.Mario.RespondToRequest(MarioActionRequest.Jump);
        }

        public void TakeDownInput(bool TouchingTransPipe)
        {
            this.Mario.RespondToRequest(MarioActionRequest.Crouch);
            if (TouchingTransPipe)
            {
                this.Mario.Level.HandlePipeEntrance();
            }
        }

        public void TakeLeftInput(bool TouchingTransPipe)
        {
            this.Mario.RespondToRequest(MarioActionRequest.GoLeft);
        }

        public void TakeRightInput(bool TouchingTransPipe)
        {
            this.Mario.RespondToRequest(MarioActionRequest.GoRight);
        }

        public void RespondToNoCollision()
        {
            this.Mario.RespondToRequest(MarioActionRequest.Fall);
        }

        public void TakeUpReleased() { }

        public void TakeNoInput() { }
    }
}
