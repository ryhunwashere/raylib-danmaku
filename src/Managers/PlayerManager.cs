using System.Diagnostics;

using RaylibDanmaku.Engine;
using RaylibDanmaku.Core;
using RaylibDanmaku.Builders;
using RaylibDanmaku.Entities.ShotTypes;
using RaylibDanmaku.Entities.ShotTypes.Bullet;
using RaylibDanmaku.Entities.ShotTypes.Beam;
using RaylibDanmaku.Entities.Characters.Player;
using System.ComponentModel;

namespace RaylibDanmaku.Managers;

internal class PlayerManager
{
    private static Player _player = null!;
    private static BulletManager _bulletManager = null!;
    private static BeamManager _beamManager = null!;

    public static Player GetPlayer() => _player;
    public static BulletManager GetBulletManager() => _bulletManager;
    public static BeamManager GetBeamManager() => _beamManager;

    public enum PlayerCharacters { ReimuA, MarisaA, MarisaB }

    /// <summary> 
    ///     Initialize player based on <see cref="PlayerCharacters"/> enum. <br/> 
    ///     Available choices: Reimu, MarisaA, MarisaB <br/><br/>
    /// 
    ///     ⚠️ NOTE: Update the choice list if <see cref="PlayerCharacters"/> changes.
    /// </summary>
    public static void InitSelectedPlayer(PlayerCharacters character)
    {
        _bulletManager = new BulletManager();
        _beamManager = new BeamManager();

        BulletManager.PlayerBulletTextureId = EngineTexture.LoadTexture("assets/Bullets/PlayerBullet1.png");
        BeamManager.PlayerBeamTextureId = EngineTexture.LoadTexture("assets/Beams/PlayerBeam1.png");

        var playerBuilder = new PlayerBuilder(_bulletManager, _beamManager);

        switch (character)
        {
            case PlayerCharacters.ReimuA:
                _player = playerBuilder
                    .SetBaseStats(moveSpeed: 700.0f, slowMoveSpeed: 200.0f, hitboxRadius: 4.0f)
                    .SetSprite("assets/Player/player1_sprite.png")
                    .AddReimuBulletShot()
                    .Build();
                break;

            case PlayerCharacters.MarisaA:
                _player = playerBuilder
                    .SetBaseStats(moveSpeed: 850.0f, slowMoveSpeed: 200.0f, hitboxRadius: 3.0f)
                    .SetSprite("assets/Player/player2_sprite.png")
                    .AddMarisaBeamShot()
                    .Build();
                break;

            case PlayerCharacters.MarisaB:
                _player = playerBuilder
                    .SetBaseStats(moveSpeed: 850.0f, slowMoveSpeed: 200.0f, hitboxRadius: 3.0f)
                    .SetSprite("assets/Player/player2_sprite.png")
                    .AddMarisaBeamShot()
                    .AddReimuBulletShot()
                    .Build();
                break;

            default:
                break;
        }
    }
}
