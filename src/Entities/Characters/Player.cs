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
        private IPlayerShot? Shot;

        public Vector2 Position;
        public float MoveSpeed;
        public float SlowMoveSpeed;
        public float HitboxRadius;
        public float GrazeRadius;
        private const float GRAZE_RADIUS_MULT = 4.0f;
        public int TextureId;
        public float TextureScale { get; private set; }
        private const float SHOOT_COOLDOWN = 0.1f;
        private float shootTimer = 0.0f;

        private int powerLevel = 0;
        public const int MIN_POWER_LEVEL = 0;
        public const int MAX_POWER_LEVEL = 4;

        public Player(float moveSpeed, float slowMoveSpeed, float hitboxRadius, string spritePath, float scale)
        {
            if (string.IsNullOrEmpty(spritePath))
                throw new ArgumentException("Player spritePath must not be null or empty.");

            TextureId = Render.LoadTextureFromFile(spritePath);
            TextureScale = scale;

            if (TextureId < 0)
                Trace.TraceWarning("Failed to load player texture!");

            Position = new Vector2(500, 500);   // don't mind the magic numbers, it's just to test the player spawning.
            MoveSpeed = moveSpeed;
            SlowMoveSpeed = slowMoveSpeed;
            HitboxRadius = hitboxRadius;
            GrazeRadius = hitboxRadius * GRAZE_RADIUS_MULT;

        }

        public void SetShot(IPlayerShot shotType)
        {
            Shot = shotType;
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

            if (Input.IsKeyDown(KeyboardKey.X) && (shootTimer >= SHOOT_COOLDOWN))
            {
                Shot?.Shoot(powerLevel);
                shootTimer = 0.0f;
            }

            // Power level tests
            if (Input.IsKeyPressed(KeyboardKey.KpAdd) && powerLevel < MAX_POWER_LEVEL)
            {
                powerLevel += 1;
                Console.WriteLine("Level increased! Current power level: " + powerLevel);
            }

            if (Input.IsKeyPressed(KeyboardKey.KpSubtract) && powerLevel > MIN_POWER_LEVEL)
            {
                powerLevel -= 1;
                Console.WriteLine("Level decreased! Current power level: " + powerLevel);
            }

            // Clamp movement to window size
            Position.X = Raymath.Clamp(Position.X, HitboxRadius, Config.SCREEN_WIDTH - HitboxRadius);
            Position.Y = Raymath.Clamp(Position.Y, HitboxRadius, Config.SCREEN_HEIGHT - HitboxRadius);
        }

        public void Draw()
        {
            Render.QueueDraw(TextureId, Position.X, Position.Y, TextureScale, rotation: 0.0f, tint: NativeColor.White, layer: 2);
        }
    }
}
