using System.Diagnostics;
using System.Numerics;

using RaylibDanmaku.Structs;
using RaylibDanmaku.Core;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Entities.WeaponTypes;

namespace RaylibDanmaku.Managers;

internal class BulletManager
{
    private const int MaxBullets = 1024;

    // Bullet pools
    private readonly Bullet[] _playerBullets = new Bullet[MaxBullets];
    private readonly Bullet[] _enemyBullets = new Bullet[MaxBullets];

    private const int PlayerBulletLayer = 1;
    private const int EnemyBulletLayer = 2;
    
    public static int PlayerBulletTextureId;

    /// <summary> Spawn an individual bullet. </summary>
    public void SpawnBullet(
        BulletOwner bulletOwner,
        BulletType bulletType,
        Vector2 position,
        Vector2 direction,
        float speed,
        float scale,
        float rotation,
        NativeColor color,
        float lifetimeSec,
        int textureId)
    {
        Bullet[] pool = bulletOwner == BulletOwner.Player ? _playerBullets : _enemyBullets;
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].Active)    // find inactive bullet index in the pool
            {
                pool[i] = new Bullet
                {
                    BulletOwner = bulletOwner,
                    BulletType = bulletType,
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
        Trace.TraceWarning("[BulletManager] Bullet pool reached the limit! Max Bullets: " + MaxBullets);
    }

    public void Update(float deltaTime)
    {
        UpdatePool(_playerBullets, deltaTime);
        // UpdatePool(_enemyBullets, deltaTime);
    }

    private static void UpdatePool(Bullet[] pool, float deltaTime)
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].Active) continue;

            pool[i].Position += pool[i].Direction * pool[i].Speed * deltaTime;
            pool[i].Lifetime -= deltaTime;

            if (pool[i].Position.X < 0 - Config.ScreenMargin ||
                pool[i].Position.X > Config.ScreenWidth + Config.ScreenMargin ||
                pool[i].Position.Y < 0 - Config.ScreenMargin ||
                pool[i].Position.Y > Config.ScreenHeight + Config.ScreenMargin ||
                pool[i].Lifetime <= 0)
            {
                pool[i].Active = false;
            }
        }
    }

    public void Draw()
    {
        DrawPool(_playerBullets);
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
                layer: bullet.BulletOwner == BulletOwner.Player ? PlayerBulletLayer : EnemyBulletLayer
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
        for (int i = 0; i < MaxBullets; i++)
        {
            if (_playerBullets[i].Active) activeBulletCount++;
        }
        return activeBulletCount;
    }


}
