using Raylib_cs;
namespace RaylibDanmaku
{
    internal class Program
    {
        public static void Main()
        {
            GameManager.InitGame();

            Raylib.SetTargetFPS(60);

            while (!Raylib.WindowShouldClose())
            {
                GameManager.UpdateGame();

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                GameManager.DrawGame();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
