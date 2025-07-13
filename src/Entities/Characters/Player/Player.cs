using System.Diagnostics;
using System.Numerics;

using RaylibDanmaku.Core;
using RaylibDanmaku.Structs;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Entities.IPlayerShotTypes;

namespace RaylibDanmaku.Entities.Characters.Player;

/// <summary> Player character: handles input, movement, shooting, power, and bombs. </summary>
internal class Player
{
    // Player state
    public Vector2 Position;
    public float MoveSpeed;
    public float SlowMoveSpeed;
    public float HitboxRadius;
    public float GrazeRadius;
    public int TextureId;
    public float TextureScale { get; private set; }

    // Helpers
    private readonly PlayerInput input;
    private readonly PlayerPower playerPower;
    private readonly PlayerBomb playerBomb;
    private readonly PlayerShooting playerShooting;

    public Player(float moveSpeed, float slowMoveSpeed, float hitboxRadius, string spritePath, float scale)
    {
        if (string.IsNullOrEmpty(spritePath))
            throw new ArgumentNullException(nameof(spritePath), "Player spritePath must not be null or empty.");

        TextureId = EngineTexture.LoadTexture(spritePath);
        TextureScale = scale;

        if (TextureId < 0)
            Trace.TraceWarning("Failed to load player texture!");

        Position = new Vector2(Config.SCREEN_WIDTH / 2, Config.SCREEN_HEIGHT);
        MoveSpeed = moveSpeed;
        SlowMoveSpeed = slowMoveSpeed;
        HitboxRadius = hitboxRadius;
        GrazeRadius = hitboxRadius * 4.0f;

        // Initialize helpers
        input = new PlayerInput();
        playerPower = new PlayerPower();
        playerBomb = new PlayerBomb();
        playerShooting = new PlayerShooting(cooldownTime: 0.1f);

        // Subscribe to power change event
        playerPower.OnPowerChanged += HandlePowerChanged;
    }

    public void SetBulletShot(BulletShot? shot) => playerShooting.SetBulletShot(shot);

    public void SetBeamShot(BeamShot? shot) => playerShooting.SetBeamShot(shot);

    public void Update(float deltaTime)
    {
        // Update cooldown timers
        playerShooting.Update(deltaTime);
        playerBomb.Update(deltaTime);

        // Movement
        Vector2 moveDelta = input.GetMovement(MoveSpeed, SlowMoveSpeed, deltaTime);
        Position += moveDelta;

        // Clamp position to screen
        Position.X = Math.Clamp(Position.X, HitboxRadius, Config.SCREEN_WIDTH - HitboxRadius);
        Position.Y = Math.Clamp(Position.Y, HitboxRadius, Config.SCREEN_HEIGHT - HitboxRadius);

        Trace.Assert(Position.X > 0 && Position.X < Config.SCREEN_WIDTH && Position.Y > 0 && Position.Y < Config.SCREEN_HEIGHT,
            "[Player.Update()] Player's position must be inside the game window!");

        // Bomb
        if (input.UseBomb)
            playerBomb.Use();

        // Shooting bullets
        if (input.ShootBullet)
            playerShooting.ShootBullet(playerPower);

        // Shooting beams
        if (input.ShootBeam)
            playerShooting.ShootBeam(playerPower);
        if (input.StopBeam)
            playerShooting.StopBeam();

        // Power level up/down
        if (input.PowerUp)
            playerPower.Increase();
        if (input.PowerDown)
            playerPower.Decrease();
    }

    public void Draw()
    {
        EngineRender.QueueDrawTexture(
            textureId: TextureId,
            x: Position.X,
            y: Position.Y,
            scale: TextureScale,
            rotation: 0.0f,
            color: NativeColor.White,
            layer: 50
        );
    }

    // Power change event
    private void HandlePowerChanged(int newPowerLevel)
    {
        // Restart beam at new power
        if (input.IsBeamHeld)
            playerShooting.RestartBeam(playerPower);
    }
}
