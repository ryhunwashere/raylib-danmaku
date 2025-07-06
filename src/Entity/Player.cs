using System.Numerics;
using Raylib_cs;
using RaylibDanmaku.Core;
namespace RaylibDanmaku.Entity
{
    internal class Player
    {
        public Vector2 Position;
        public float MoveSpeed;
        public float HitboxRadius;
        public float GrazeRadius;
        public int TextureId;
        public float TextureScale { get; private set; }

        public Player(float moveSpeed, float hitboxRadius, string spritePath, float scale)
        {
            if (string.IsNullOrEmpty(spritePath))
                throw new ArgumentException("spritePath must not be null or empty.");

            TextureId = Render.LoadTextureFromFile(spritePath);
            TextureScale = scale;

            if (TextureId < 0)
                Console.WriteLine("Failed to load player texture!");

            Position = new Vector2(500, 500);
            MoveSpeed = moveSpeed;
            HitboxRadius = hitboxRadius;        // smaller hitbox
            GrazeRadius = hitboxRadius * 4.0f;
        }

        public void Update(float deltaTime)
        {
            Console.WriteLine($"deltaTime: {deltaTime}");
            float speed = MoveSpeed;

            if (Input.IsKeyDown(KeyboardKey.LeftShift))
                speed /= 2.0f;

            if (Input.IsKeyDown(KeyboardKey.Left))
                Position.X -= speed * deltaTime;
            if (Input.IsKeyDown(KeyboardKey.Right))
                Position.X += speed * deltaTime;
            if (Input.IsKeyDown(KeyboardKey.Up))
                Position.Y -= speed * deltaTime;
            if (Input.IsKeyDown(KeyboardKey.Down))
                Position.Y += speed * deltaTime;
            
            // Clamp movement to window size
            Position.X = Raymath.Clamp(Position.X, HitboxRadius, Config.SCREEN_WIDTH - HitboxRadius);
            Position.Y = Raymath.Clamp(Position.Y, HitboxRadius, Config.SCREEN_HEIGHT - HitboxRadius);
        }

        public Vector2 GetBulletSpawnPos(float offsetX, float offsetY)
        {
            return new Vector2(Position.X + offsetX, Position.Y - offsetY);
        }
    }
}
