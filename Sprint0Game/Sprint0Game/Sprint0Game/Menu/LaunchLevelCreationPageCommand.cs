

namespace Sprint0Game
{
    public class LaunchLevelCreationPageCommand : ICommand
    {
        private Menu Menu;

        public LaunchLevelCreationPageCommand(Menu menu)
        {
            this.Menu = menu;
        }

        public void Execute()
        {
            this.Menu.ShowLevelCreationPage();
        }

    }
}
