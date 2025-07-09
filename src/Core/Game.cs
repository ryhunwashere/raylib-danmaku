using RaylibDanmaku.Engine;
using RaylibDanmaku.Entities.Characters;
using RaylibDanmaku.Entities.WeaponManagers;

namespace RaylibDanmaku.Core
{
    /// <summary>
    /// Game manager to glue all modules before sent into Program
    /// </summary>
    internal static class Game
    {
        private static Player? player;
        private static BulletManager? bulletManager;
        private static BeamManager? beamManager;
        
        public static void InitGame()
        {
            EngineRender.InitRender(Config.SCREEN_WIDTH, Config.SCREEN_HEIGHT, Config.WINDOW_NAME, Config.TARGET_FPS);

            PlayerManager.InitSelectedPlayer(playerId: 2);      // Choose player character here

            player = PlayerManager.GetPlayer();
            bulletManager = PlayerManager.GetBulletManager();
            beamManager = PlayerManager.GetBeamManager();
        }

        public static void UpdateGame()
        {
            Time.Update();
            float deltaTime = Time.DeltaTime;

            player?.Update(deltaTime);
            bulletManager?.Update(deltaTime);
            beamManager?.Update();
        }

        public static void DrawGame()
        {
            player?.Draw();
            bulletManager?.Draw();
            beamManager?.Draw();

            EngineRender.RenderFrame();   // Draw all queued objects
        }

        public static void UnloadGame()
        {
            EngineRender.ShutdownRender();
        }
    }
}


