using System.Diagnostics;
using System.Numerics;

using Raylib_cs;
using RaylibDanmaku.Core;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Entities.PlayerShotTypes;

namespace RaylibDanmaku.Entities.Characters
{
    /// <summary>
    /// Player constructor and input controls.
    /// </summary>
    internal class Player
    {
        // Player shot types
        private IPlayerShot? BulletShot;
        private IPlayerShot? BeamShot;

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

            TextureId = EngineRender.LoadTextureFromFile(spritePath);
            TextureScale = scale;

            if (TextureId < 0)
                Trace.TraceWarning("Failed to load player texture!");

            Position = new Vector2(500, 500);   // don't mind the magic numbers, it's just to test the player spawning.
            MoveSpeed = moveSpeed;
            SlowMoveSpeed = slowMoveSpeed;
            HitboxRadius = hitboxRadius;
            GrazeRadius = hitboxRadius * GRAZE_RADIUS_MULT;

        }

        public void SetBulletShot(IPlayerShot? shot) => BulletShot = shot;
        public void SetBeamShot(IPlayerShot? shot) => BeamShot = shot;

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


            if (Input.IsKeyDown(KeyboardKey.X) && shootTimer >= SHOOT_COOLDOWN)
            {
                BulletShot?.ShootBullet(powerLevel);
                shootTimer = 0.0f;
            }

            if (Input.IsKeyPressed(KeyboardKey.X))
            {
                BeamShot?.ShootBeam(powerLevel);
            }

            if (Input.IsKeyReleased(KeyboardKey.X))
            {
                BeamShot?.StopShootBeam();
            }

            // Power level tests
            if (Input.IsKeyPressed(KeyboardKey.KpAdd) && powerLevel < MAX_POWER_LEVEL)
            {
                powerLevel += 1;

                // Reset beam state during level up
                BeamShot?.StopShootBeam();
                BeamShot?.ShootBeam(powerLevel);
                
                Console.WriteLine("Level increased! Current power level: " + powerLevel);
            }

            if (Input.IsKeyPressed(KeyboardKey.KpSubtract) && powerLevel > MIN_POWER_LEVEL)
            {
                powerLevel -= 1;

                // Reset beam state during level down
                BeamShot?.StopShootBeam();
                BeamShot?.ShootBeam(powerLevel);

                Console.WriteLine("Level decreased! Current power level: " + powerLevel);
            }

            // Clamp movement to window size
            Position.X = Raymath.Clamp(Position.X, HitboxRadius, Config.SCREEN_WIDTH - HitboxRadius);
            Position.Y = Raymath.Clamp(Position.Y, HitboxRadius, Config.SCREEN_HEIGHT - HitboxRadius);
        }

        public void Draw()
        {
            EngineRender.QueueDrawTexture(TextureId, Position.X, Position.Y, TextureScale, rotation: 0.0f, color: NativeColor.White, layer: 50);
        }
    }
}
