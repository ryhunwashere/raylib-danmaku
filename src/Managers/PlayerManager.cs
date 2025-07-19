using System.Diagnostics;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Core;
using RaylibDanmaku.Entities.ShotTypes;
using RaylibDanmaku.Entities.ShotTypes.Bullet;
using RaylibDanmaku.Entities.ShotTypes.Beam;
using RaylibDanmaku.Entities.Characters.Player;

namespace RaylibDanmaku.Managers;

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
        PlayerShotType shotType)
    {
        _player = new Player(
            moveSpeed,
            slowMoveSpeed,
            hitboxRadius,
            spritePath,
            scale: Config.PlayerScale
        );

        _player.SetShotType(shotType);
    }

    public enum PlayerCharacters { Reimu, Marisa, Sanae }

    /// <summary> 
    ///     Initialize player based on selected player enum. <br /> 
    ///     Available choices: Reimu, Marisa, Sanae
    /// </summary>
    public static void InitSelectedPlayer(PlayerCharacters character)
    {
        _bulletManager = new BulletManager();
        _beamManager = new BeamManager();

        BulletManager.PlayerBulletTextureId = EngineTexture.LoadTexture("assets/Bullets/PlayerBullet1.png");
        BeamManager.PlayerBeamTextureId = EngineTexture.LoadTexture("assets/Beams/PlayerBeam1.png");

        PlayerShotType? selectedShotType;
        switch (character)
        {
            case PlayerCharacters.Reimu: // Reimu
                selectedShotType = new ReimuBulletShot(_bulletManager);
                InitPlayer(
                    moveSpeed: 700.0f,
                    slowMoveSpeed: 300.0f,
                    hitboxRadius: 4.0f,
                    spritePath: "assets/Player/player1_sprite.png",
                    shotType: selectedShotType);
                break;

            case PlayerCharacters.Marisa: // Marisa
                selectedShotType = new MarisaBeamShot(_beamManager);
                InitPlayer(
                    moveSpeed: 850.0f,
                    slowMoveSpeed: 200.0f,
                    hitboxRadius: 3.0f,
                    spritePath: "assets/Player/player2_sprite.png",
                    shotType: selectedShotType);
                break;

            case PlayerCharacters.Sanae:
                // TODO: character that can shoot both beam and bullets maybe
                break;
                
            default:
                Trace.TraceWarning("[PlayerManager] Cannot construct player due to invalid playerId.");
                break;
        }
    }
}
