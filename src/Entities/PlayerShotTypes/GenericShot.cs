using System.Diagnostics;
using System.Numerics;

using RaylibDanmaku.Core;
using RaylibDanmaku.Entities.Characters;
using RaylibDanmaku.Entities.WeaponManagers;

namespace RaylibDanmaku.Entities.PlayerShotTypes
{
    internal class GenericShot(Player player, BulletManager bulletManager) : IPlayerShot
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

        public void Shoot(int powerLevel)
        {
            Trace.Assert(powerLevel >= Player.MIN_POWER_LEVEL && powerLevel <= Player.MAX_POWER_LEVEL,
            "Shoot failed! Player power level must be between " + Player.MIN_POWER_LEVEL + " to " + Player.MAX_POWER_LEVEL + ".");

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
}