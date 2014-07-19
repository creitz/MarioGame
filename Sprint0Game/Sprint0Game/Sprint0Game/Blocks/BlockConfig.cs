using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public static class BlockConfig
    {
        public static readonly int BumpHeight = 10;
        public static readonly int BumpUpSpeed = 1;
        public static readonly int BumpDownSpeed = -1;
        public static readonly int FrameStepPeriod = 10;
        public static readonly int DestroyedBlockTimer = 50;
        public static readonly Vector2 BrokenBlockVelocity = new Vector2(GameConfig.GameSpeed*2f, GameConfig.GameSpeed*2f);        
    }
}
