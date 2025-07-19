namespace RaylibDanmaku.Entities.Characters.Player;

internal class PlayerPower
{
    public int PlayerLevel { get; private set; } = 0;
    public const int MaxPlayerLevel = 4;
    public const int MinPlayerLevel = 0;

    public event Action<int>? OnPowerChanged;

    public void Increase()
    {
        if (PlayerLevel < MaxPlayerLevel)
        {
            PlayerLevel++;
            OnPowerChanged?.Invoke(PlayerLevel);
        }
    }

    public void Decrease()
    {
        if (PlayerLevel > MinPlayerLevel)
        {
            PlayerLevel--;
            OnPowerChanged?.Invoke(PlayerLevel);
        }
    }
}
