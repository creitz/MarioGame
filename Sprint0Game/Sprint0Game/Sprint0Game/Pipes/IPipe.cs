using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sprint0Game
{
    public interface IPipe : IObject
    {
        int Type { get; set; }
        bool Side { get; set; }
    }
}
