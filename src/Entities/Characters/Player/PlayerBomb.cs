using System.Diagnostics;

namespace RaylibDanmaku.Entities.Characters.Player;

internal class PlayerBomb
{
    private int BombCount { get; set; } = 4;
    private const float BOMB_COOLDOWN = 3.0f;
    private float bombTimer = BOMB_COOLDOWN;   // Set initial bomb timer

    public void Update(float deltaTime) => bombTimer += deltaTime;

    public void Use()
    {
        Trace.Assert(BombCount >= 0, "[PlayerBomb.Use()] BombCount cannot be negative!");
        if (BombCount < 0)
            throw new ArgumentOutOfRangeException(nameof(BombCount), "[PlayerBomb.Use()] Negative BombCount value");

        if (bombTimer >= BOMB_COOLDOWN && BombCount > 0)
        {
            BombCount -= 1;
            bombTimer = 0.0f;
            Console.WriteLine("[PlayerBomb] Fire in the hole! Bombs left: " + BombCount);
        }
        else if (BombCount == 0)
            Console.WriteLine("[PlayerBomb] No bombs left!");
        else
        {
            float timeLeft = MathF.Round(bombTimer - BOMB_COOLDOWN, digits: 1) * -1;
            Console.WriteLine("[PlayerBomb] Bomb is still on cooldown! Time left: " + timeLeft + "s");
        }
    }

}
