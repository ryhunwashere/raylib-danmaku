using System.Diagnostics;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Core;
using RaylibDanmaku.Entities.WeaponManagers;
using RaylibDanmaku.Entities.IPlayerShotTypes;

namespace RaylibDanmaku.Entities.Characters.Player;

/// <summary>
/// Construct a player based on selection, and load weapon textures like bullet and beam.
/// </summary>
internal class PlayerManager
{
    private static Player _player = null!;
    private static BulletManager _bulletManager = null!;
    private static BeamManager _beamManager = null!;

    public static Player GetPlayer() => _player;
    public static BulletManager GetBulletManager() => _bulletManager;
    public static BeamManager GetBeamManager() => _beamManager;

    private static void InitPlayer(
        float moveSpeed,
        float slowMoveSpeed,
        float hitboxRadius,
        string spritePath,
        bool hasBulletShot,
        bool hasBeamShot)
    {
        _bulletManager = new BulletManager();
        _beamManager = new BeamManager();

        int bulletTextureId = EngineTexture.LoadTexture("assets/Bullets/PlayerBullet1.png");
        BulletManager.PlayerBulletTextureId = bulletTextureId;

        int beamTextureId = EngineTexture.LoadTexture("assets/Beams/PlayerBeam1.png");
        BeamManager.PlayerBeamTextureId = beamTextureId;

        _player = new Player(
            moveSpeed,
            slowMoveSpeed,
            hitboxRadius,
            spritePath,
            scale: Config.PlayerScale
        );

        if (hasBulletShot)
        {
            BulletShot bulletShot = new(_player, _bulletManager);
            _player.SetBulletShot(bulletShot);
        }
        else
            _player.SetBulletShot(null);

        if (hasBeamShot)
        {
            BeamShot beamShot = new(_player, _beamManager);
            _player.SetBeamShot(beamShot);
        }
        else
            _player.SetBeamShot(null);
    }

    /// <summary>Initialize player based on selected player ID. </summary>
    /// <param name="playerId"> Select between player type 1 to 3.</param>
    public static void InitSelectedPlayer(int playerId)
    {
        Trace.Assert(playerId >= 1 && playerId <= 3, "Invalid playerId.");
        if (playerId < 1 || playerId > 3)
            throw new ArgumentOutOfRangeException(nameof(playerId), "playerId is beyond the allowed range.");

        switch (playerId)
        {
            case 1:
                InitPlayer(
                    moveSpeed: 700.0f,
                    slowMoveSpeed: 300.0f,
                    hitboxRadius: 4.0f,
                    spritePath: "assets/Player/player1_sprite.png",
                    hasBulletShot: true,
                    hasBeamShot: false);
                break;
            case 2:
                InitPlayer(
                    moveSpeed: 850.0f,
                    slowMoveSpeed: 200.0f,
                    hitboxRadius: 3.0f,
                    spritePath: "assets/Player/player2_sprite.png",
                    hasBulletShot: false,
                    hasBeamShot: true);
                break;
            case 3:
                // TODO: character that can shoot both beam and bullets maybe
                break;
            default:
                Trace.TraceWarning("[PlayerManager] Cannot construct player due to invalid playerId.");
                break;
        }
    }
}
