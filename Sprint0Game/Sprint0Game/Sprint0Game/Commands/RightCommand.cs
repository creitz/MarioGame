

namespace Sprint0Game
{
    public class RightCommand : ICommand
    {
        private ILevel Level;

        public RightCommand(ILevel level)
        {
            this.Level = level;
        }

        public void Execute()
        {
            if (!this.Level.IsStopped())
                this.Level.Mario.TakeRightInput();
        }

    }
}
