using RaylibDanmaku.Entities.IPlayerShotTypes;

namespace RaylibDanmaku.Entities.Characters.Player;

internal class PlayerShooting(float cooldownTime)
{
    private BulletShot? _bulletShot;
    private BeamShot? _beamShot;
    private readonly BulletCooldown _bulletCooldown = new(cooldownTime);

    public void Update(float deltaTime)
    {
        _bulletCooldown.Update(deltaTime);
    }

    public void ShootBullet(PlayerPower playerPower)
    {
        if (_bulletCooldown.IsReady)
        {
            _bulletShot?.ShootBullet(playerPower.PowerLevel);
            _bulletCooldown.Reset();
        }
    }

    public void ShootBeam(PlayerPower playerPower)
    {
        _beamShot?.ShootBeam(playerPower.PowerLevel);
    }

    public void StopBeam()
    {
        _beamShot?.StopShootBeam();
    }

    public void RestartBeam(PlayerPower playerPower)
    {
        _beamShot?.StopShootBeam();
        _beamShot?.ShootBeam(playerPower.PowerLevel);
    }

    public void SetBulletShot(BulletShot? bulletShot)
    {
        _bulletShot = bulletShot;
    }

    public void SetBeamShot(BeamShot? beamShot)
    {
        _beamShot = beamShot;
    }
}
