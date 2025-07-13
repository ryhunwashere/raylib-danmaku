using System.Diagnostics;

namespace RaylibDanmaku.Entities.Characters.Player;

internal class PlayerBomb
{
    private int BombCount { get; set; } = 4;
    private const float BombCooldown = 3.0f;
    private float _bombTimer = BombCooldown;   // Set initial bomb timer

    public void Update(float deltaTime) => _bombTimer += deltaTime;

    public void Use()
    {
        Trace.Assert(BombCount >= 0, "[PlayerBomb.Use()] BombCount cannot be negative!");
        if (BombCount < 0)
            throw new ArgumentOutOfRangeException(nameof(BombCount), "[PlayerBomb.Use()] Negative BombCount value");

        if (_bombTimer >= BombCooldown && BombCount > 0)
        {
            BombCount -= 1;
            _bombTimer = 0.0f;
            Console.WriteLine("[PlayerBomb] Fire in the hole! Bombs left: " + BombCount);
        }
        else if (BombCount == 0)
            Console.WriteLine("[PlayerBomb] No bombs left!");
        else
        {
            float timeLeft = MathF.Round(_bombTimer - BombCooldown, digits: 1) * -1;
            Console.WriteLine("[PlayerBomb] Bomb is still on cooldown! Time left: " + timeLeft + "s");
        }
    }

}
