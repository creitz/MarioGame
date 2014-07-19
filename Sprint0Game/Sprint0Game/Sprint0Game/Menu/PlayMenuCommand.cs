

namespace Sprint0Game
{
    public class PlayMenuCommand : ICommand
    {
        private Game Game;

        public PlayMenuCommand(Game game)
        {
            this.Game = game;
        }

        public void Execute()
        {
            this.Game.PlayGame();
        }
    }
}
