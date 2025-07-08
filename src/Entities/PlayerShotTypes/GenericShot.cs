using System.Diagnostics;
using System.Numerics;
using RaylibDanmaku.Core;
namespace RaylibDanmaku.Entities
{
    internal class GenericShot(Player player, BulletManager bulletManager) : IPlayerShot
    {
        private readonly Player player = player;
        private readonly BulletManager bulletManager = bulletManager;

        private void SpawnPlayerBullet(float offsetX)
        {
            Vector2 bulletDir = new(0, -1);
            Vector2 spawnPos = player.Position + new Vector2(offsetX, -50);
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
            Trace.Assert(powerLevel >= Config.MIN_POWER_LEVEL && powerLevel <= Config.MAX_POWER_LEVEL,
            "Shoot failed! Player power level must be between " + Config.MIN_POWER_LEVEL + " to " + Config.MAX_POWER_LEVEL + ".");

            switch (powerLevel)
            {
                case 0:
                    SpawnPlayerBullet(0.0f);
                    break;
                case 1:
                    SpawnPlayerBullet(20.0f);
                    SpawnPlayerBullet(-20.0f);
                    break;
                case 2:
                    SpawnPlayerBullet(30.0f);
                    SpawnPlayerBullet(0.0f);
                    SpawnPlayerBullet(-30.0f);
                    break;
                case 3:
                    SpawnPlayerBullet(60.0f);
                    SpawnPlayerBullet(20.0f);
                    SpawnPlayerBullet(-20.0f);
                    SpawnPlayerBullet(-60.0f);
                    break;
                case 4:
                    SpawnPlayerBullet(80.0f);
                    SpawnPlayerBullet(40.0f);
                    SpawnPlayerBullet(0.0f);
                    SpawnPlayerBullet(-40.0f);
                    SpawnPlayerBullet(-80.0f);
                    break;
                default:
                    break;
            }
        }
    }
}