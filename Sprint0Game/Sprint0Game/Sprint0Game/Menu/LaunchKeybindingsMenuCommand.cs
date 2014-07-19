namespace Sprint0Game
{
    public class LaunchKeybindingsMenuCommand : ICommand
    {
        private Menu Menu;

        public LaunchKeybindingsMenuCommand(Menu menu)
        {
            this.Menu = menu;
        }

        public void Execute()
        {
            this.Menu.ShowKeybindingsPage();
        }
    }
}
