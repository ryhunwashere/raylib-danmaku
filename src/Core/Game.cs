using Raylib_cs;
using RaylibDanmaku.Entity;
namespace RaylibDanmaku.Core
{
    /// <summary>
    /// Game manager to glue all modules before sent into Program
    /// </summary>
    internal static class Game
    {
        private static Player? player;
        public static void InitGame()
        {
            Render.InitRender(Config.SCREEN_WIDTH, Config.SCREEN_HEIGHT, "Raylib Danmaku", 60);

            player = new Player(
                700.0f,
                200.0f,
                4.0f,
                "assets/player/player_sprite.png",
                Config.PLAYER_SCALE
            );
        }


        public static void UpdateGame()
        {
            Time.Update();

            Console.WriteLine($"deltaTime: {Time.DeltaTime}");
            player?.Update(Time.DeltaTime);
        }

        public static void DrawGame()
        {
            if (player != null)
            {
                Render.RenderFrame(
                    player.Position.X,
                    player.Position.Y,
                    player.TextureId,
                    player.TextureScale
                );
            }

            Raylib.DrawFPS(10, 10);
            float deltaTime = Raylib.GetFrameTime();
            Raylib.DrawText($"DeltaTime: {deltaTime:F4}", 10, 30, 20, Color.Red);
        }

        public static void UnloadGame()
        {
            Render.ShutdownRender();
        }
    }
}