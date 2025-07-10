using System.Diagnostics;
using System.Numerics;

using RaylibDanmaku.Core;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Entities.WeaponTypes;

namespace RaylibDanmaku.Entities.WeaponManagers
{
    internal class BulletManager
    {
        private const int MAX_BULLETS = 1024;

        // Bullet pools
        private readonly Bullet[] playerBullets = new Bullet[MAX_BULLETS];
        private readonly Bullet[] enemyBullets = new Bullet[MAX_BULLETS];
        public static int PlayerBulletTextureId;
        private const int PLAYER_BULLET_LAYER = 1;
        private const int ENEMY_BULLET_LAYER = 2;

        public enum BulletType { CIRCLE, RECT, LINE }
        public enum BulletOwner { PLAYER, ENEMY }

        /// <summary>
        /// Spawn an individual bullet.
        /// </summary>
        public void SpawnBullet(
            BulletOwner owner,
            BulletType type,
            Vector2 position,
            Vector2 direction,
            float speed,
            float scale,
            float rotation,
            NativeColor color,
            float lifetimeSec,
            int textureId)
        {
            Bullet[] pool = owner == BulletOwner.PLAYER ? playerBullets : enemyBullets;
            for (int i = 0; i < pool.Length; i++)
            {
                if (!pool[i].Active)    // find inactive bullet index in the pool
                {
                    pool[i] = new Bullet
                    {
                        Owner = owner,
                        Type = type,
                        Position = position,
                        Direction = Vector2.Normalize(direction),
                        Speed = speed,
                        Scale = scale,
                        Rotation = rotation,
                        Color = color,
                        Lifetime = lifetimeSec,
                        TextureId = textureId,
                        Active = true
                    };
                    return; // bullet spawned
                }
            }
            // If the bullet exceeds the max limit:
            Trace.TraceWarning("[BulletManager] Bullet pool reached the limit! Max Bullets: " + MAX_BULLETS);
        }

        public void Update(float deltaTime)
        {
            UpdatePool(playerBullets, deltaTime);
            // UpdatePool(enemyBullets, deltaTime);
        }

        private static void UpdatePool(Bullet[] pool, float deltaTime)
        {
            for (int i = 0; i < pool.Length; i++)
            {
                if (!pool[i].Active) continue;

                pool[i].Position += pool[i].Direction * pool[i].Speed * deltaTime;
                pool[i].Lifetime -= deltaTime;

                if (pool[i].Position.X < 0 - Config.SCREEN_MARGIN ||
                    pool[i].Position.X > Config.SCREEN_WIDTH + Config.SCREEN_MARGIN ||
                    pool[i].Position.Y < 0 - Config.SCREEN_MARGIN ||
                    pool[i].Position.Y > Config.SCREEN_HEIGHT + Config.SCREEN_MARGIN ||
                    pool[i].Lifetime <= 0)
                {
                    pool[i].Active = false;
                }
            }
        }

        public void Draw()
        {
            DrawPool(playerBullets);
            DrawActiveBulletCount();
        }

        private static void DrawPool(Bullet[] pool)
        {
            foreach (var bullet in pool)
            {
                if (!bullet.Active) continue;

                EngineRender.QueueDrawTexture(
                    bullet.TextureId,
                    bullet.Position.X,
                    bullet.Position.Y,
                    bullet.Scale,
                    bullet.Rotation,
                    bullet.Color,
                    layer: bullet.Owner == BulletOwner.PLAYER ? PLAYER_BULLET_LAYER : ENEMY_BULLET_LAYER
                );
            }
        }

        private void DrawActiveBulletCount()
        {
            EngineRender.QueueDrawText("Active bullets: " + CountActiveBullets(), x: 10, y: 40, fontSize: 20, color: NativeColor.White, layer: 10);
        }

        private int CountActiveBullets()
        {
            int activeBulletCount = 0;
            for (int i = 0; i < MAX_BULLETS; i++)
            {
                if (playerBullets[i].Active) activeBulletCount++;
            }
            return activeBulletCount;
        }


    }
}