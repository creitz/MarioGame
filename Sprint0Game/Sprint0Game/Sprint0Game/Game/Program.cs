
namespace Sprint0Game
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            using (Game game = new Game())
            {
                game.Run();
            }
        }
    }
#endif
}

