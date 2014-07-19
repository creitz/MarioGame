namespace Sprint0Game
{
    public class LaunchMainMenuCommand : ICommand
    {
        private Menu Menu;

        public LaunchMainMenuCommand(Menu menu)
        {
            this.Menu = menu;
        }

        public void Execute()
        {
            this.Menu.ShowHomePage();
        }

    }
}
