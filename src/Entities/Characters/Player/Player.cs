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
    private readonly PlayerInput _input;
    private readonly PlayerPower _playerPower;
    private readonly PlayerBomb _playerBomb;
    private readonly PlayerShooting _playerShooting;

    public Player(float moveSpeed, float slowMoveSpeed, float hitboxRadius, string spritePath, float scale)
    {
        if (string.IsNullOrEmpty(spritePath))
            throw new ArgumentNullException(nameof(spritePath), "Player spritePath must not be null or empty.");

        TextureId = EngineTexture.LoadTexture(spritePath);
        TextureScale = scale;

        if (TextureId < 0)
            Trace.TraceWarning("Failed to load player texture!");

        Position = new Vector2(Config.ScreenWidth / 2, Config.ScreenHeight);
        MoveSpeed = moveSpeed;
        SlowMoveSpeed = slowMoveSpeed;
        HitboxRadius = hitboxRadius;
        GrazeRadius = hitboxRadius * 4.0f;

        // Initialize helpers
        _input = new PlayerInput();
        _playerPower = new PlayerPower();
        _playerBomb = new PlayerBomb();
        _playerShooting = new PlayerShooting(cooldownTime: 0.1f);

        // Subscribe to power change event
        _playerPower.OnPowerChanged += HandlePowerChanged;
    }

    public void SetBulletShot(BulletShot? shot) => _playerShooting.SetBulletShot(shot);

    public void SetBeamShot(BeamShot? shot) => _playerShooting.SetBeamShot(shot);

    public void Update(float deltaTime)
    {
        // Update cooldown timers
        _playerShooting.Update(deltaTime);
        _playerBomb.Update(deltaTime);

        // Movement
        Vector2 moveDelta = _input.GetMovement(MoveSpeed, SlowMoveSpeed, deltaTime);
        Position += moveDelta;

        // Clamp position to screen
        Position.X = Math.Clamp(Position.X, HitboxRadius, Config.ScreenHeight - HitboxRadius);
        Position.Y = Math.Clamp(Position.Y, HitboxRadius, Config.ScreenHeight - HitboxRadius);

        Trace.Assert(Position.X > 0 && Position.X < Config.ScreenHeight && Position.Y > 0 && Position.Y < Config.ScreenHeight,
            "[Player.Update()] Player's position must be inside the game window!");

        // Bomb
        if (_input.UseBomb)
            _playerBomb.Use();

        // Shooting bullets
        if (_input.ShootBullet)
            _playerShooting.ShootBullet(_playerPower);

        // Shooting beams
        if (_input.ShootBeam)
            _playerShooting.ShootBeam(_playerPower);
        if (_input.StopBeam)
            _playerShooting.StopBeam();

        // Power level up/down
        if (_input.PowerUp)
            _playerPower.Increase();
        if (_input.PowerDown)
            _playerPower.Decrease();
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
        if (_input.IsBeamHeld)
            _playerShooting.RestartBeam(_playerPower);
    }
}
