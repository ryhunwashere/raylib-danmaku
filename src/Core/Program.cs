namespace RaylibDanmaku.Core
{
    /// <summary>
    /// Main program to run the game.
    /// </summary>
    internal class Program
    {
        static void Main()
        {
            Game.InitGame();

            while (Render.NativeWindowShouldClose() == 0)
            {
                Game.UpdateGame();
                Game.DrawGame();
            }
            Game.UnloadGame();
        }
    }
}
