

namespace Sprint0Game
{
    public class FireballCommand : ICommand
    {
        private ILevel Level;

        public FireballCommand(ILevel level)
        {
            this.Level = level;
        }

        public void Execute()
        {
            if (!this.Level.IsStopped())
            {
                this.Level.ShootFireballFromMario();
            }
        }

    }
}
