

namespace Sprint0Game
{
    public class PauseCommand : ICommand
    {
        private ILevel Level;

        public PauseCommand(ILevel level)
        {
            this.Level = level;
        }

        public void Execute()
        {
            this.Level.IsPaused = !this.Level.IsPaused;
        }

    }
}
