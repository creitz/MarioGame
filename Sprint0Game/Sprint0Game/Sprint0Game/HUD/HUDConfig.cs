using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sprint0Game
{
    public static class HUDConfig
    {
        private static int buffer = 15;
        public static readonly Vector2 MARIOLoc = new Vector2(50, 10);
        public static readonly Vector2 WORLDLoc = new Vector2(450, MARIOLoc.Y);
        public static readonly Vector2 TIMELoc = new Vector2(700, WORLDLoc.Y);
        public static readonly Vector2 PointsLoc = new Vector2(MARIOLoc.X, MARIOLoc.Y + 20);
        public static readonly int WorldValueY = (int)PointsLoc.Y;
        public static readonly Vector2 TimeValueLoc = new Vector2(TIMELoc.X + 10, PointsLoc.Y);
        public static readonly Vector2 CoinImgLoc = new Vector2(260, PointsLoc.Y + 7);
        public static readonly Vector2 CoinXLoc = new Vector2(CoinImgLoc.X + buffer, PointsLoc.Y);
        public static readonly Vector2 CoinCountLoc = new Vector2(CoinXLoc.X + buffer, CoinXLoc.Y);
        public static readonly Vector2 PausedLoc = new Vector2(270, 120);
    }
}
