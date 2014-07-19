
namespace Sprint0Game
{

    public static class MarioPowerLevelGeneralizer
    {
        public static bool IsBig(MarioPowerLevel powerLevel)
        {
            return (powerLevel == MarioPowerLevel.Big || powerLevel == MarioPowerLevel.Fire || powerLevel == MarioPowerLevel.Metal);
        }
    }
}
