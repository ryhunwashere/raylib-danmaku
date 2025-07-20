namespace RaylibDanmaku.Entities.Characters.Player;

internal class PlayerPower
{
    public int PlayerLevel { get; private set; } = 0;
    private const int MaxPlayerLevel = 4;
    private const int MinPlayerLevel = 0;

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
