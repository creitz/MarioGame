
using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public static class MarioConfig
    {
        private static readonly float OriginalSideSpeed = GameConfig.GameSpeed * 3f;
        private static readonly float OriginalJumpVelocity = GameConfig.GameSpeed * -7f;
        public static float SideSpeed
        {
            get
            {
                return sideSpeed;
            }
            set
            {
                sideSpeed = value;
            }
        }
        public static float JumpVelocity
        {
            get
            {
                return jumpVelocity;
            }
            set
            {
                jumpVelocity = value;
            }
        }
        private static readonly float MetalFactor = 1.3f;
        public static readonly float BounceOffEnemiesVelocity = GameConfig.GameSpeed * -3f;
        public static readonly int FlashStepPeriod = 4;
        public static readonly int TransitioningFromDamageDuration = 50;
        public static readonly int TransitioningFromItemDuration = 50;
        public static readonly int StarDuration = 500;
        public static readonly int BottomCollisionTolerance = 1;
        public static readonly int RunningFrameStepPeriod = 10;
        public static readonly int MetalDuration = 500;
        public static readonly Color MetalMarioColor = Color.DarkSlateGray;
        public static readonly float DeadVelocity = GameConfig.GameSpeed * -9f;
        public static readonly int WaitBeforeFlyingUpTimer = 50;

        private static float sideSpeed = OriginalSideSpeed;
        private static float jumpVelocity = OriginalJumpVelocity;

        public static void SetMetalMarioSpeed()
        {
            jumpVelocity = MarioConfig.OriginalJumpVelocity / MarioConfig.MetalFactor;
            sideSpeed = MarioConfig.OriginalSideSpeed / MarioConfig.MetalFactor;
        }

        public static void SetNormalMarioSpeed()
        {
            jumpVelocity = MarioConfig.OriginalJumpVelocity;
            sideSpeed = MarioConfig.OriginalSideSpeed;
        }
    }
}
