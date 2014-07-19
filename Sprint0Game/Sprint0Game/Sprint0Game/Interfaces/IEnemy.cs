
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public interface IEnemy : IObject
    {
        bool IsDead { get; }
    }
}
