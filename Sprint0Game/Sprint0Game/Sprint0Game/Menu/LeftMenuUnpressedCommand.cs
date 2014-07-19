

namespace Sprint0Game
{
    public class LeftMenuUnpressedCommand : ICommand
    {
        private Menu Menu;

        public LeftMenuUnpressedCommand(Menu menu)
        {
            this.Menu = menu;
        }

        public void Execute()
        {
            this.Menu.PreviousHasBeenUnpressed = true;
        }

    }
}
