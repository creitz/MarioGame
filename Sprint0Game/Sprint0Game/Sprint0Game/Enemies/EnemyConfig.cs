
namespace Sprint0Game
{
    public static class EnemyConfig
    {
        public static readonly int GoombaFramePeriod = 5;
        public static readonly float GoombaRunVelocity = GameConfig.GameSpeed * 1f;
        public static readonly int KoopaFramePeriod = 5;
        public static readonly float KoopaRunVelocity = GameConfig.GameSpeed * 1f;
        public static readonly int GoombaDeathTimer = 10;
        public static readonly int FlyingKoopaFramePeriod = 5;
        public static readonly float FlyingKoopaRunVelocity = GameConfig.GameSpeed * 1.5f;
        public static readonly float FlyingKoopaBounceVelocity = GameConfig.GameSpeed * -3.5f;
        public static readonly int SpinyFramePeriod = 5;
        public static readonly float SpinyRunVelocity = GameConfig.GameSpeed * 1f;
        public static readonly int LakituInCloudPeriod = 75;
        public static readonly int LakituOutOfCloudPeriod = 100;
        public static readonly float LakituHorizontalVelocity = GameConfig.GameSpeed * .01f;
        public static readonly float LakituVerticalVelocity = GameConfig.GameSpeed * 0.1f;
        public static readonly float DeathUpVelocity = GameConfig.GameSpeed * -3f;
    }
}
