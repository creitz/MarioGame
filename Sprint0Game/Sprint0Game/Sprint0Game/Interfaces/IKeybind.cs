using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections;

namespace Sprint0Game
{
    public interface IKeybind
    {
        void Rebind(Keys key);

        Keys key { get; set; }

    }
}
