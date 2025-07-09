using RaylibDanmaku.Engine;

namespace RaylibDanmaku.Core
{
    /// <summary>
    /// Main program to run the game.
    /// </summary>
    internal class Program
    {
        private static void Main()
        {
            Game.InitGame();

            while (EngineRender.NativeWindowShouldClose() == 0)
            {
                Game.UpdateGame();
                Game.DrawGame();
            }
            Game.UnloadGame();
        }
    }
}
