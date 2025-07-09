namespace RaylibDanmaku.Entities.PlayerShotTypes
{
    internal interface IPlayerShot
    {
        void ShootBullet(int powerLevel);
        void ShootBeam(int powerLevel);
        void StopShootBeam();
    }
}