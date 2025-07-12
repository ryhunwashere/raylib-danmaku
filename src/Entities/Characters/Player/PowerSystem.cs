namespace RaylibDanmaku.Entities.Characters;

internal class PlayerPower
{
    public int PowerLevel { get; private set; } = 0;
    public const int MAX_POWER_LEVEL = 4;
    public const int MIN_POWER_LEVEL = 0;

    public event Action<int>? OnPowerChanged;

    public void Increase()
    {
        if (PowerLevel < MAX_POWER_LEVEL)
        {
            PowerLevel++;
            OnPowerChanged?.Invoke(PowerLevel);
        }
    }

    public void Decrease()
    {
        if (PowerLevel > MIN_POWER_LEVEL)
        {
            PowerLevel--;
            OnPowerChanged?.Invoke(PowerLevel);
        }
    }
}
