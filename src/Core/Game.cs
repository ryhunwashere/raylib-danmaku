using RaylibDanmaku.Engine;
using RaylibDanmaku.Entities.Characters;
using RaylibDanmaku.Entities.WeaponManagers;
using RaylibDanmaku.Entities.PlayerShotTypes;

namespace RaylibDanmaku.Core
{
    /// <summary>
    /// Game manager to glue all modules before sent into Program
    /// </summary>
    internal static class Game
    {
        // Modules
        private static Player? player;
        private static BulletManager? bulletManager;
        private static BeamManager? beamManager;

        public static void InitGame()
        {
            EngineRender.InitRender(Config.SCREEN_WIDTH, Config.SCREEN_HEIGHT, Config.WINDOW_NAME, Config.TARGET_FPS);

            bulletManager = new BulletManager();
            beamManager = new BeamManager();

            int bulletTextureId = EngineRender.LoadTextureFromFile("assets/Bullets/PlayerBullet1.png");
            BulletManager.PlayerBulletTextureId = bulletTextureId;

            int beamTextureId = EngineRender.LoadTextureFromFile("assets/Beams/PlayerBeam1.png");
            BeamManager.PlayerBeamTextureId = beamTextureId;

            // TODO: character selection system to change the player constructor values.
            // Don't mind the magic numbers for now.
            player = new Player(
                moveSpeed: 700.0f,
                slowMoveSpeed: 200.0f,
                hitboxRadius: 4.0f,
                spritePath: "assets/player/player_sprite.png",
                scale: Config.PLAYER_SCALE
            );

            // Create shooting behavior
            var genericShot = new GenericShot(player, bulletManager);

            // Inject into the player
            player.SetShot(genericShot);
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


