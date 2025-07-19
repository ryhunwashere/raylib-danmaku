using System.Numerics;

using RaylibDanmaku.Structs;
using RaylibDanmaku.Managers;
using RaylibDanmaku.Entities.Characters.Player;
using RaylibDanmaku.Entities.WeaponTypes;

namespace RaylibDanmaku.Entities.ShotTypes.Bullet;

internal class ReimuBulletShot(BulletManager bulletManager) : PlayerShotType
{
    private readonly BulletManager _bulletManager = bulletManager;

    public override void Shoot(Player player, PlayerPower power)
    {
        SpawnBullet(player, new Vector2(0, -10));
        SpawnBullet(player, new Vector2(-20, 0));
        SpawnBullet(player, new Vector2(20, 0));
    }

    private void SpawnBullet(Player player, Vector2 offset)
    {
        Vector2 bulletDir = new(0, -1);
        Vector2 spawnPos = player.Position + offset;
        float bulletSpeed = 2000.0f;
        float bulletScale = 1.8f;
        float rotation = -90.0f;
        float lifetime = 2.0f;
        NativeColor color = NativeColor.Transparent;

        BulletOwner bulletOwner = BulletOwner.Player;
        BulletType bulletType = BulletType.Rect;

        _bulletManager.SpawnBullet(
            bulletOwner,
            bulletType,
            spawnPos,
            bulletDir,
            bulletSpeed,
            bulletScale,
            rotation,
            color,
            lifetime,
            BulletManager.PlayerBulletTextureId
        );
    }
}
