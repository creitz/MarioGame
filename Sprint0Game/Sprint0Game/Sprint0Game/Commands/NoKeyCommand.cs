

namespace Sprint0Game
{
    public class NoKeyCommand : ICommand
    {
        private ILevel Level;

        public NoKeyCommand(ILevel level)
        {
            this.Level = level;
        }

        public void Execute()
        {
            if (!this.Level.IsStopped())
                this.Level.Mario.TakeNoInput();
        }

    }
}
