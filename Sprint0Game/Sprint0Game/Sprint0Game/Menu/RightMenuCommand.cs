

namespace Sprint0Game
{
    public class RightMenuCommand : ICommand
    {
        private Menu Menu;

        public RightMenuCommand(Menu menu)
        {
            this.Menu = menu;
        }

        public void Execute()
        {
            if (this.Menu.NextHasBeenUnpressed)
            {
                this.Menu.MoveCursorToNext();
                this.Menu.NextHasBeenUnpressed = false;
            }
        }

    }
}
