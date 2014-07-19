
namespace Sprint0Game
{
    public class CrouchingMarioStateResponder : IMarioStateResponder
    {
        private IMario Mario;

        public CrouchingMarioStateResponder(IMario mario)
        {
            this.Mario = mario;
        }

        public void TakeUpInput() 
        {
            this.Mario.RespondToRequest(MarioActionRequest.GoIdle);
        }

        public void TakeDownInput(bool isOnTransitionPipe) { }

        public void TakeLeftInput(bool TouchingTransPipe)
        {
            this.Mario.RespondToRequest(MarioActionRequest.GoLeft);
        }

        public void TakeRightInput(bool TouchingTransPipe)
        {
            this.Mario.RespondToRequest(MarioActionRequest.GoRight);
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
