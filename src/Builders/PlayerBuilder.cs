using RaylibDanmaku.Core;
using RaylibDanmaku.Entities.Characters.Player;
using RaylibDanmaku.Entities.ShotTypes;
using RaylibDanmaku.Entities.ShotTypes.Beam;
using RaylibDanmaku.Entities.ShotTypes.Bullet;
using RaylibDanmaku.Managers;

namespace RaylibDanmaku.Builders;

internal class PlayerBuilder(BulletManager bulletManager, BeamManager beamManager)
{
    private readonly BulletManager _bulletManager = bulletManager;
    private readonly BeamManager _beamManager = beamManager;

    // Fields to store the configuration of player
    private float _moveSpeed;
    private float _slowMoveSpeed;
    private float _hitboxRadius;
    private string _spritePath = string.Empty;
    private float _scale = Config.PlayerScale;
    private readonly List<PlayerShotType> _shotTypes = [];

    public PlayerBuilder SetBaseStats(float moveSpeed, float slowMoveSpeed, float hitboxRadius)
    {
        _moveSpeed = moveSpeed;
        _slowMoveSpeed = slowMoveSpeed;
        _hitboxRadius = hitboxRadius;

        return this;
    }

    public PlayerBuilder SetSprite(string spritePath)
    {
        _spritePath = spritePath;
        _scale = Config.PlayerScale;

        return this;
    }

    public PlayerBuilder AddReimuBulletShot()
    {
        _shotTypes.Add(new ReimuBulletShot(_bulletManager));

        return this;
    }

    public PlayerBuilder AddMarisaBeamShot()
    {
        _shotTypes.Add(new MarisaBeamShot(_beamManager));

        return this;
    }

    public Player Build()
    {
        PlayerShotType finalShotType;

        if (_shotTypes.Count == 0)
            finalShotType = null!;
        else if (_shotTypes.Count == 1)
            finalShotType = _shotTypes[0];
        else
            finalShotType = new CompositePlayerShot(_shotTypes.ToArray());

        // Create player instance with the configured stats
        var player = new Player(_moveSpeed, _slowMoveSpeed, _hitboxRadius, _spritePath, _scale);

        // Configure player with the assembled components
        player.SetShotType(finalShotType);

        return player;
    }
}
