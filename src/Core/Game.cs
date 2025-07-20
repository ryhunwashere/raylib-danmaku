using RaylibDanmaku.Engine;
using RaylibDanmaku.Entities.Characters.Player;
using RaylibDanmaku.Managers;

namespace RaylibDanmaku.Core;

/// <summary>
/// Game manager to glue all modules before sent into Program
/// </summary>
internal static class Game
{
    private static Player? _player;
    private static BulletManager? _bulletManager;
    private static BeamManager? _beamManager;

    public static void InitGame()
    {
        EngineRender.InitRender(Config.ScreenWidth, Config.ScreenHeight, Config.WindowName, Config.TargetFPS);

        PlayerManager.InitSelectedPlayer(PlayerManager.PlayerCharacters.MarisaB);

        _player = PlayerManager.GetPlayer();
        _bulletManager = PlayerManager.GetBulletManager();
        _beamManager = PlayerManager.GetBeamManager();
    }

    public static void UpdateGame()
    {
        Time.Update();
        float deltaTime = Time.DeltaTime;

        _player?.Update(deltaTime);
        _bulletManager?.Update(deltaTime);
        _beamManager?.Update();
    }

    public static void DrawGame()
    {
        _player?.Draw();
        _bulletManager?.Draw();
        _beamManager?.Draw();

        EngineRender.RenderFrame();   // Draw all queued objects
    }

    public static void UnloadGame()
    {
        EngineRender.ShutdownRender();
    }
}



