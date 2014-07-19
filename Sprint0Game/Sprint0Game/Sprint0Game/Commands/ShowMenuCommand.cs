

namespace Sprint0Game
{
    public class ShowMenuCommand : ICommand
    {
        private Game Game;

        public ShowMenuCommand(Game game)
        {
            this.Game = game;
        }

        public void Execute()
        {
            this.Game.MenuDisplaying = true;
            GameStats.Coins = 0;
            GameStats.Lives = 1;
            GameStats.Points = 0;
            this.Game.ResetLevel();
        }
    }
}
