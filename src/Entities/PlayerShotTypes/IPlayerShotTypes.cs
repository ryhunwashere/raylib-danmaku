namespace RaylibDanmaku.Entities.IPlayerShotTypes;

internal interface IBulletShot
{
    void ShootBullet(int powerLevel);
}

internal interface IBeamShot
{
    void ShootBeam(int powerLevel);
    void StopShootBeam();
}
