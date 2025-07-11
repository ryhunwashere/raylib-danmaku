using System.Diagnostics;
using System.Numerics;

using Raylib_cs;
using RaylibDanmaku.Core;
using RaylibDanmaku.Structs;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Entities.PlayerShotTypes;

namespace RaylibDanmaku.Entities.Characters
{
    /// <summary> Player constructor and input controls. </summary>
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

        // Timers in seconds
        private const float SHOOT_COOLDOWN = 0.1f;
        private float shootTimer = 0.0f;
        private const float BOMB_COOLDOWN = 3.0f;
        private float bombTimer = 0.0f;

        private int powerLevel = 0;
        public const int MIN_POWER_LEVEL = 0;
        public const int MAX_POWER_LEVEL = 4;

        public void SetBulletShot(IPlayerShot? shot) => BulletShot = shot;
        public void SetBeamShot(IPlayerShot? shot) => BeamShot = shot;

        /// <summary> Constructs and initialize a player character. </summary>
        /// <param name="moveSpeed"> Base movement speed of the player character. </param>
        /// <param name="slowMoveSpeed"> Movement speed on slow mode of the player character. </param>
        /// <param name="hitboxRadius"> Circular hitbox relative to the player's center. </param>
        /// <param name="spritePath"> Directory path of the player's sprite. </param>
        /// <param name="scale"> Multiply the player's sprite size by the provided scale. </param>
        /// <exception cref="ArgumentException"></exception>
        public Player(float moveSpeed, float slowMoveSpeed, float hitboxRadius, string spritePath, float scale)
        {
            if (string.IsNullOrEmpty(spritePath))
                throw new ArgumentException("Player spritePath must not be null or empty.");

            TextureId = EngineTexture.LoadTexture(spritePath);
            TextureScale = scale;

            if (TextureId < 0)
                Trace.TraceWarning("Failed to load player texture!");

            Position = new Vector2(500, 500);   // don't mind the magic numbers
            MoveSpeed = moveSpeed;
            SlowMoveSpeed = slowMoveSpeed;
            HitboxRadius = hitboxRadius;
            GrazeRadius = hitboxRadius * GRAZE_RADIUS_MULT;
            bombTimer = BOMB_COOLDOWN;
        }

        public void Update(float deltaTime)
        {
            float speed = MoveSpeed;
            float slowSpeed = SlowMoveSpeed;
            shootTimer += deltaTime;
            bombTimer += deltaTime;

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
                BeamShot?.ShootBeam(powerLevel);
            if (Input.IsKeyReleased(KeyboardKey.X))
                BeamShot?.StopShootBeam();
            
            // TODO: Bomb system
            if (Input.IsKeyPressed(KeyboardKey.Space))
            {
                if (bombTimer >= BOMB_COOLDOWN)
                {
                    Console.WriteLine("FIRE IN THE HOLE!");
                    bombTimer = 0.0f;
                }
                else
                {
                    float timeLeft = MathF.Round(BOMB_COOLDOWN - bombTimer, digits: 1);
                    Console.WriteLine("Bomb is still on cooldown! Time left: " + timeLeft + "s");
                }
            }

            // Power level tests
            if (Input.IsKeyPressed(KeyboardKey.KpAdd) && powerLevel < MAX_POWER_LEVEL)
            {
                powerLevel += 1;

                BeamShot?.StopShootBeam();
                BeamShot?.ShootBeam(powerLevel);
            }

            if (Input.IsKeyPressed(KeyboardKey.KpSubtract) && powerLevel > MIN_POWER_LEVEL)
            {
                powerLevel -= 1;

                BeamShot?.StopShootBeam();
                BeamShot?.ShootBeam(powerLevel);
            }

            // Clamp movement to window size
            Position.X = Math.Clamp(Position.X, HitboxRadius, Config.SCREEN_WIDTH - HitboxRadius);
            Position.Y = Math.Clamp(Position.Y, HitboxRadius, Config.SCREEN_HEIGHT - HitboxRadius);
        }

        public void Draw()
        {
            EngineRender.QueueDrawTexture(TextureId, Position.X, Position.Y, TextureScale, rotation: 0.0f, color: NativeColor.White, layer: 50);
        }
    }
}
