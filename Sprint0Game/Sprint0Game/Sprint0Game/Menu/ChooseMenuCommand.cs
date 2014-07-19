

namespace Sprint0Game
{
    public class SelectMenuCommand : ICommand
    {
        private Menu Menu;

        public SelectMenuCommand(Menu menu)
        {
            this.Menu = menu;
        }

        public void Execute()
        {
            if (this.Menu.SelectHasBeenUnpressed)
            {
                this.Menu.Choose();
                this.Menu.SelectHasBeenUnpressed = false;
            }
        }

    }
}
