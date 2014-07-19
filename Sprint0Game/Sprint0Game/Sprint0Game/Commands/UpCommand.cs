

namespace Sprint0Game
{
    public class UpCommand : ICommand
    {
        private ILevel Level;

        public UpCommand(ILevel level)
        {
            this.Level = level;
        }

        public void Execute()
        {
            if (!this.Level.IsStopped())
                this.Level.Mario.TakeUpInput();
        }

    }
}
