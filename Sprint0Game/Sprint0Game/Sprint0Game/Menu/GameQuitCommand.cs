

namespace Sprint0Game
{
    public class GameQuitCommand : ICommand
    {
        private Game Game;

        public GameQuitCommand(Game game)
        {
            this.Game = game;
        }

        public void Execute()
        {
            this.Game.Exit();
        }

    }
}
