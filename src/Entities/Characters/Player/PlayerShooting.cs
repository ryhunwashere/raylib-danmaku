using RaylibDanmaku.Entities.IPlayerShotTypes;

namespace RaylibDanmaku.Entities.Characters.Player;

internal class PlayerShooting(BulletShot? bulletShot, BeamShot? beamShot, float bulletCooldownSeconds)
{
    private BulletShot? bulletShot = bulletShot;
    private BeamShot? beamShot = beamShot;
    private readonly PlayerBulletCooldown playerBulletCooldown = new PlayerBulletCooldown(bulletCooldownSeconds);

    public void Update(float deltaTime)
    {
        playerBulletCooldown.Update(deltaTime);
    }

    public void ShootBullet(int powerLevel)
    {
        if (bulletShot == null)
        {
            Console.WriteLine("bulletShot is null!");
            return;
        }

        Console.WriteLine("Calling bulletShot.ShootBullet!");
        bulletShot.ShootBullet(powerLevel);
    }

    public void ShootBeam(int powerLevel)
    {
        beamShot?.ShootBeam(powerLevel);
    }

    public void StopBeam()
    {
        beamShot?.StopShootBeam();
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
