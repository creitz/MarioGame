using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;

namespace Sprint0Game
{
    public class RebindCommand : ICommand
    {
        public IKeybind keybind { get; set; }
        public MenuKeyboardController keyboard { get; set; }

        public RebindCommand(IKeybind keybind, MenuKeyboardController keyboard) 
        {
            this.keybind = keybind;
            this.keyboard = keyboard;
        }

        public void Execute()
        {
            Keys key = this.keyboard.LastKey;
            keybind.Rebind(key);
        }
    }
}
