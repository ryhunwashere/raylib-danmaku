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

            // Construct new player
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

            player?.Update(Time.DeltaTime);
        }

        public static void DrawGame()
        {
            player?.Draw();
            Render.RenderFrame();   // Draw all queued objects
        }

        public static void UnloadGame()
        {
            Render.ShutdownRender();
        }
    }
}