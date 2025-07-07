using System.Numerics;
using Raylib_cs;
using RaylibDanmaku.Entities;
namespace RaylibDanmaku.Core
{
    /// <summary>
    /// Game manager to glue all modules before sent into Program
    /// </summary>
    internal static class Game
    {
        private static Player? player;
        private static BulletManager? bulletManager;

        public static void InitGame()
        {
            Render.InitRender(Config.SCREEN_WIDTH, Config.SCREEN_HEIGHT, "Raylib Danmaku", 60);

            bulletManager = new BulletManager();

            int bulletTextureId = Render.LoadTextureFromFile("assets/Bullets/PlayerBullet1.png");
            BulletManager.PlayerBulletTextureId = bulletTextureId;

            // Construct new player
            player = new Player(
                moveSpeed: 700.0f,
                slowMoveSpeed: 200.0f,
                hitboxRadius: 4.0f,
                spritePath: "assets/player/player_sprite.png",
                scale: Config.PLAYER_SCALE,
                bulletManager
            );
        }

        public static void UpdateGame()
        {
            Time.Update();
            float deltaTime = Time.DeltaTime;

            player?.Update(deltaTime);
            bulletManager?.Update(deltaTime);
        }

        public static void DrawGame()
        {
            player?.Draw();
            bulletManager?.Draw();
            Render.RenderFrame();   // Draw all queued objects
        }

        public static void UnloadGame()
        {
            Render.ShutdownRender();
        }
    }
}


