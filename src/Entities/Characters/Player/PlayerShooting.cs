using RaylibDanmaku.Entities.IPlayerShotTypes;

namespace RaylibDanmaku.Entities.Characters.Player;

internal class PlayerShooting(float cooldownTime)
{
    private BulletShot? bulletShot;
    private BeamShot? beamShot;
    private readonly BulletCooldown bulletCooldown = new(cooldownTime);

    public void Update(float deltaTime)
    {
        bulletCooldown.Update(deltaTime);
    }

    public void ShootBullet(PlayerPower playerPower)
    {
        if (bulletCooldown.IsReady)
        {
            bulletShot?.ShootBullet(playerPower.PowerLevel);
            bulletCooldown.Reset();
        }
    }

    public void ShootBeam(PlayerPower playerPower)
    {
        beamShot?.ShootBeam(playerPower.PowerLevel);
    }

    public void StopBeam()
    {
        beamShot?.StopShootBeam();
    }

    public void RestartBeam(PlayerPower playerPower)
    {
        beamShot?.StopShootBeam();
        beamShot?.ShootBeam(playerPower.PowerLevel);
    }

    public void SetBulletShot(BulletShot? bulletShot)
    {
        this.bulletShot = bulletShot;
    }

    public void SetBeamShot(BeamShot? beamShot)
    {
        this.beamShot = beamShot;
    }
}
