

namespace Sprint0Game
{
    public class PlayHordeCommand : ICommand
    {
        private Game Game;

        public PlayHordeCommand(Game game)
        {
            this.Game = game;
        }

        public void Execute()
        {
            this.Game.PlayHordeMode();
        }

    }
}
