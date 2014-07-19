

namespace Sprint0Game
{
    public class LeftMenuCommand : ICommand
    {
        private Menu Menu;

        public LeftMenuCommand(Menu menu)
        {
            this.Menu = menu;
        }

        public void Execute()
        {
            if (this.Menu.PreviousHasBeenUnpressed)
            {
                this.Menu.MoveCursorToPrevious();
                this.Menu.PreviousHasBeenUnpressed = false;
            }
        }

    }
}
