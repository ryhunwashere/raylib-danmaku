using System.Diagnostics;
using System.Numerics;
using Raylib_cs;
using RaylibDanmaku.Core;
namespace RaylibDanmaku.Entities
{
    /// <summary>
    /// Player constructor and input controls.
    /// </summary>
    internal class Player
    {
        public Vector2 Position;
        public float MoveSpeed;
        public float SlowMoveSpeed;
        public float HitboxRadius;
        public float GrazeRadius;
        public int TextureId;
        public float TextureScale { get; private set; }
        private readonly float shootCooldown = 0.1f;
        private float shootTimer = 0.0f;
        
        private readonly BulletManager bulletManager;

        public Player(float moveSpeed, float slowMoveSpeed, float hitboxRadius, string spritePath, float scale, BulletManager bulletManager)
        {
            this.bulletManager = bulletManager;

            if (string.IsNullOrEmpty(spritePath))
                throw new ArgumentException("Player spritePath must not be null or empty.");

            TextureId = Render.LoadTextureFromFile(spritePath);
            TextureScale = scale;

            if (TextureId < 0)
                Trace.TraceWarning("Failed to load player texture!");

            Position = new Vector2(500, 500);
            MoveSpeed = moveSpeed;
            SlowMoveSpeed = slowMoveSpeed;
            HitboxRadius = hitboxRadius;
            GrazeRadius = hitboxRadius * 4.0f;
        }

        public void Update(float deltaTime)
        {
            float speed = MoveSpeed;
            float slowSpeed = SlowMoveSpeed;
            shootTimer += deltaTime;

            if (Input.IsKeyDown(KeyboardKey.LeftShift))
                speed = slowSpeed;

            if (Input.IsKeyDown(KeyboardKey.Left))
                Position.X -= speed * deltaTime;
            if (Input.IsKeyDown(KeyboardKey.Right))
                Position.X += speed * deltaTime;
            if (Input.IsKeyDown(KeyboardKey.Up))
                Position.Y -= speed * deltaTime;
            if (Input.IsKeyDown(KeyboardKey.Down))
                Position.Y += speed * deltaTime;

            if (Input.IsKeyDown(KeyboardKey.X) && (shootTimer >= shootCooldown))
            {
                Shoot();
                shootTimer = 0.0f;
            }

            // Clamp movement to window size
            Position.X = Raymath.Clamp(Position.X, HitboxRadius, Config.SCREEN_WIDTH - HitboxRadius);
            Position.Y = Raymath.Clamp(Position.Y, HitboxRadius, Config.SCREEN_HEIGHT - HitboxRadius);
        }

        public void Draw()
        {
            Render.QueueDraw(TextureId, Position.X, Position.Y, TextureScale, rotation: 0.0f, tint: NativeColor.White, layer: 2);
        }

        public Vector2 GetBulletSpawnPos(float offsetX, float offsetY)
        {
            return new Vector2(Position.X + offsetX, Position.Y - offsetY);
        }

        public void Shoot()
        {
            Vector2 bulletDir = new(0, -1); // shoot upward
            float bulletSpeed = 2500.0f;
            float bulletScale = 1.8f;
            float rotation = -90.0f;
            float lifetime = 2.0f;

            // Where to spawn bullet relative to player
            Vector2 spawnPos = GetBulletSpawnPos(0, 20);

            bulletManager.SpawnBullet(
                BulletManager.BulletOwner.PLAYER,
                BulletManager.BulletType.RECT,
                spawnPos,
                bulletDir,
                bulletSpeed,
                bulletScale,
                rotation,
                NativeColor.Transparent,
                lifetime,
                textureId: BulletManager.PlayerBulletTextureId
            );
        }
    }
}
