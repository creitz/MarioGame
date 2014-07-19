
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public class RunningMarioStateResponder : IMarioStateResponder
    {
        private IMario Mario;

        public RunningMarioStateResponder(IMario mario)
        {
            this.Mario = mario;
        }

        public void TakeUpInput() 
        {
            SoundBoard.JumpSound.Play();
            this.Mario.RespondToRequest(MarioActionRequest.Jump);
        }

        public void TakeDownInput(bool isOnTransitionPipe) 
        {
            this.Mario.RespondToRequest(MarioActionRequest.Crouch);
        }

        public void TakeLeftInput(bool TouchingTransPipe)
        {
            this.Mario.RespondToRequest(MarioActionRequest.GoLeft);
            if (TouchingTransPipe && !this.Mario.IsTravelingPipe)
            {
                this.Mario.Level.HandlePipeExit();
            }
        }

        public void TakeRightInput(bool TouchingTransPipe)
        {
            this.Mario.RespondToRequest(MarioActionRequest.GoRight);
            if (TouchingTransPipe && !this.Mario.IsTravelingPipe)
            {
                this.Mario.Level.HandlePipeExit();
            }
        }

        public void TakeNoInput() 
        {
            this.Mario.RespondToRequest(MarioActionRequest.GoIdle);
        }

        public void TakeUpReleased() { }

        public void RespondToNoCollision()
        {
            this.Mario.RespondToRequest(MarioActionRequest.Fall);         
        }
    }
}
