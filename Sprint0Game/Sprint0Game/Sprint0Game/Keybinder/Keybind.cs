using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;

namespace Sprint0Game
{
    class Keybind : IKeybind
    {
        public Keys key { get; set; }

        public Keybind(Keys Key)
        {
            this.key = Key;
        }

        public void Rebind(Keys key)
        {
            this.key = key;
        }
    }
}
