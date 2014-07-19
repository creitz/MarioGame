

namespace Sprint0Game
{
    public class ToggleMusicCommand : ICommand
    {
        private Game Game;

        public ToggleMusicCommand(Game game)
        {
            this.Game = game;
        }

        public void Execute()
        {
            this.Game.MusicShouldPlay = !this.Game.MusicShouldPlay;
        }

    }
}
