

namespace Sprint0Game
{
    public class RightMenuUnpressedCommand : ICommand
    {
        private Menu Menu;

        public RightMenuUnpressedCommand(Menu menu)
        {
            this.Menu = menu;
        }

        public void Execute()
        {
            this.Menu.NextHasBeenUnpressed = true;
        }

    }
}
