
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace Sprint0Game
{
    public class DeadMarioState : IMarioState
    {
        public IAnimatedMario Sprite { get; set; }

        public DeadMarioState()
        {
        }

        public void RespondToRequest(MarioActionRequest change)
        {
            //change state accordingly
        }

        public void Update()
        {
        }

        public void Draw(SpriteBatch spriteBatch, ICamera camera)
        {
        }

        public bool IsRightFacing() { return true; }
        public MarioPowerLevel PowerLevel()
        {
            return MarioPowerLevel.Dead;
        }
    }
}
