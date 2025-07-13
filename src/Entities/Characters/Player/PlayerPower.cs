namespace RaylibDanmaku.Entities.Characters.Player;

internal class PlayerPower
{
    public int PowerLevel { get; private set; } = 0;
    public const int MaxPowerLevel = 4;
    public const int MinPowerLevel = 0;

    public event Action<int>? OnPowerChanged;

    public void Increase()
    {
        if (PowerLevel < MaxPowerLevel)
        {
            PowerLevel++;
            OnPowerChanged?.Invoke(PowerLevel);
        }
    }

    public void Decrease()
    {
        if (PowerLevel > MinPowerLevel)
        {
            PowerLevel--;
            OnPowerChanged?.Invoke(PowerLevel);
        }
    }
}
