
namespace Sprint0Game
{

    public static class SideGeneralizer
    {
        public static bool IsLeft(Side side)
        {
            return (side == Side.TopOfLeft || side == Side.BottomOfLeft);
        }

        public static bool IsRight(Side side)
        {
            return (side == Side.TopOfRight || side == Side.BottomOfRight);
        }

        public static bool IsTop(Side side)
        {
            return (side == Side.LeftOfTop || side == Side.RightOfTop);
        }

        public static bool IsBottom(Side side)
        {
            return (side == Side.LeftOfBottom || side == Side.RightOfBottom);
        }
    }
}
