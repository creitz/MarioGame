

namespace Sprint0Game
{
    public class LeftCommand : ICommand
    {
        private ILevel Level;

        public LeftCommand(ILevel level)
        {
            this.Level = level;
        }

        public void Execute()
        {
            if (!this.Level.IsStopped())
                this.Level.Mario.TakeLeftInput();
        }

    }
}
