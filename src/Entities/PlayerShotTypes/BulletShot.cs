using System.Numerics;

using RaylibDanmaku.Structs;
using RaylibDanmaku.Entities.Characters.Player;
using RaylibDanmaku.Entities.WeaponManagers;

namespace RaylibDanmaku.Entities.IPlayerShotTypes;

internal class BulletShot(Player player, BulletManager bulletManager) : IBulletShot
{
    private readonly Player player = player;
    private readonly BulletManager bulletManager = bulletManager;

    private void SpawnPlayerBullet(float offsetX, float offsetY)
    {
        Vector2 bulletDir = new(0, -1);
        Vector2 spawnPos = player.Position + new Vector2(offsetX, offsetY);     // Top padding
        float bulletSpeed = 2000.0f;
        float bulletScale = 1.8f;
        float rotation = -90.0f;
        float lifetime = 2.0f;
        NativeColor tint = NativeColor.Transparent;

        bulletManager.SpawnBullet(
            BulletManager.BulletOwner.PLAYER,
            BulletManager.BulletType.RECT,
            spawnPos,
            bulletDir,
            bulletSpeed,
            bulletScale,
            rotation,
            tint,
            lifetime,
            BulletManager.PlayerBulletTextureId
        );
    }

    public void ShootBullet(int powerLevel)
    {
        if (powerLevel < PlayerPower.MIN_POWER_LEVEL || powerLevel > PlayerPower.MAX_POWER_LEVEL)
        {
            throw new ArgumentOutOfRangeException(nameof(powerLevel),
            "[BulletShot] Player power level exceeds allowed range (" + PlayerPower.MIN_POWER_LEVEL + "-" + PlayerPower.MAX_POWER_LEVEL + ").");
        }

        switch (powerLevel)
        {
            case 0:
                SpawnPlayerBullet(offsetX: 0.0f, offsetY: -50.0f);
                break;
            case 1:
                SpawnPlayerBullet(offsetX: 20.0f, offsetY: -50.0f);
                SpawnPlayerBullet(offsetX: -20.0f, offsetY: -50.0f);
                break;
            case 2:
                SpawnPlayerBullet(offsetX: 30.0f, offsetY: -50.0f);
                SpawnPlayerBullet(offsetX: 0.0f, offsetY: -50.0f);
                SpawnPlayerBullet(offsetX: -30.0f, offsetY: -50.0f);
                break;
            case 3:
                SpawnPlayerBullet(offsetX: 60.0f, offsetY: -50.0f);
                SpawnPlayerBullet(offsetX: 20.0f, offsetY: -50.0f);
                SpawnPlayerBullet(offsetX: -20.0f, offsetY: -50.0f);
                SpawnPlayerBullet(offsetX: -60.0f, offsetY: -50.0f);
                break;
            case 4:
                SpawnPlayerBullet(offsetX: 80.0f, offsetY: -50.0f);
                SpawnPlayerBullet(offsetX: 40.0f, offsetY: -50.0f);
                SpawnPlayerBullet(offsetX: 0.0f, offsetY: -50.0f);
                SpawnPlayerBullet(offsetX: -40.0f, offsetY: -50.0f);
                SpawnPlayerBullet(offsetX: -80.0f, offsetY: -50.0f);
                break;
            default:
                break;
        }
    }
}
