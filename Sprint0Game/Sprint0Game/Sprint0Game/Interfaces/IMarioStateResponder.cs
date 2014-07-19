
namespace Sprint0Game
{
    public interface IMarioStateResponder
    {
        void TakeUpInput();
        void TakeDownInput(bool TouchingTransPipe);
        void TakeLeftInput(bool TouchingTransPipe);
        void TakeRightInput(bool TouchingTransPipe);
        void TakeNoInput();
        void TakeUpReleased();

        void RespondToNoCollision();
    }
}
