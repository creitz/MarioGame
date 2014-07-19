
namespace Sprint0Game
{
    public static class ProjectileConfig
    {
        public static readonly int FireballLife = 70;
        public static readonly double FireballHeightScale = 1.5;
        public static readonly double FireballWidthScale = FireballHeightScale * 1.2;
        public static readonly float FireballVelocity = GameConfig.GameSpeed * 4f;
        public static readonly float FireballBounceVelocity = GameConfig.GameSpeed * -1.5f;
        public static readonly float FireballDisappearanceVelocity = GameConfig.GameSpeed * -1f;
        public static readonly int FrameStepperPeriod = 7;
        public static readonly float ShellVelocity = GameConfig.GameSpeed * MarioConfig.SideSpeed * 1.2f;
        public static readonly float EggUpwardsVelocity = GameConfig.GameSpeed * -2f;
        public static readonly float EggXVelocityMultiplier = -.02f;
    }
}
