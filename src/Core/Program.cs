// Main program
namespace RaylibDanmaku.Core
{
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
