

namespace Sprint0Game
{
    public class DownCommand : ICommand
    {
        private ILevel Level;

        public DownCommand(ILevel level)
        {
            this.Level = level;
        }

        public void Execute()
        {
            if (!this.Level.IsStopped())
                this.Level.Mario.TakeDownInput();
        }

    }
}
