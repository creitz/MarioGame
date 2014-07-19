

namespace Sprint0Game
{
    public class UpReleasedCommand : ICommand
    {
        private ILevel Level;

        public UpReleasedCommand(ILevel level)
        {
            this.Level = level;
        }

        public void Execute()
        {
            if (!this.Level.IsStopped())
                this.Level.Mario.TakeUpReleased();
        }

    }
}
