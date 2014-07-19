

namespace Sprint0Game
{
    public class SelectMenuUnpressedCommand : ICommand
    {
        private Menu Menu;

        public SelectMenuUnpressedCommand(Menu menu)
        {
            this.Menu = menu;
        }

        public void Execute()
        {
            this.Menu.SelectHasBeenUnpressed = true;
        }

    }
}
