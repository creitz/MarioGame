
namespace Sprint0Game
{
    public class MarioAllStateResponder
    {
        private IMario Mario;

        public MarioAllStateResponder(IMario mario)
        {
            this.Mario = mario;
        }

        public void TakeDownInput(bool TouchingTransPipe)
        {
            IMarioStateResponder stateResponder = GetStateResponder();
            stateResponder.TakeDownInput(TouchingTransPipe);
        }

        public void TakeUpInput()
        {
            IMarioStateResponder stateResponder = GetStateResponder();
            stateResponder.TakeUpInput();
        }

        public void TakeLeftInput(bool TouchingTransPipe)
        {
            IMarioStateResponder stateResponder = GetStateResponder();
            stateResponder.TakeLeftInput(TouchingTransPipe);
        }

        public void TakeRightInput(bool TouchingTransPipe)
        {
            IMarioStateResponder stateResponder = GetStateResponder();
            stateResponder.TakeRightInput(TouchingTransPipe);
        }

        public void TakeNoInput()
        {
            IMarioStateResponder stateResponder = GetStateResponder();
            stateResponder.TakeNoInput();
        }

        public void TakeUpReleased()
        {
            IMarioStateResponder stateResponder = GetStateResponder();
            stateResponder.TakeUpReleased();
        }

        public void RespondToNoCollision()
        {
            IMarioStateResponder stateResponder = GetStateResponder();
            stateResponder.RespondToNoCollision();
        }

        private IMarioStateResponder GetStateResponder() {

            if (this.Mario.IsCrouching()) {
                return new CrouchingMarioStateResponder(this.Mario);
            }
            else if (this.Mario.IsFalling())
            {
                return new FallingMarioStateResponder(this.Mario);
            }
            else if (this.Mario.IsIdle())
            {
                return new IdleMarioStateResponder(this.Mario);
            }
            else if (this.Mario.IsJumping())
            {
                return new JumpingMarioStateResponder(this.Mario);
            }
            else if (this.Mario.IsRunning())
            {
                return new RunningMarioStateResponder(this.Mario);
            }
            else 
            {
                return new DeadMarioStateResponder();
            }
        }

    }
}
