
using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public static class Level1Config
    {
        public static readonly int GridHeight = 16;
        public static readonly int GridWidth = 16;
        public static readonly string LevelPath = "Level1-1";
        public static readonly int MarioFallingDeathYThreshold = 40;
        public static readonly int PauseWaitTimer = 20;
        public static readonly int MarioRunningFromFlagTimer = 50;
        public static readonly int MarioStandingAfterFlagTimer = 20;
        public static readonly int TimeAfterMarioHasDisappeared = 20;
        public static readonly float FlagAndMarioDownPoleSpeed = 1.5f;
        public static readonly int LevelTime = 500;
        public static readonly Vector2 MarioFirstUnderGroundPos = new Vector2(40, 350);
        public static readonly Vector2 MarioSecondUnderGroundPos = new Vector2(675, 325);
        public static readonly int MarioOnPipeCenterTolerance = 5;
        public static readonly int FirstPipePosition = 2500;
    }
}
