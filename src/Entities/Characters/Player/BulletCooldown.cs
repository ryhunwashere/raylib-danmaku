namespace RaylibDanmaku.Entities.Characters.Player;

internal class BulletCooldown(float cooldownTime)
{
    private readonly float _cooldown = cooldownTime;
    private float _timer = cooldownTime;
    public bool IsReady => _timer >= _cooldown;

    public void Update(float deltaTime)
    {
        _timer += deltaTime;
    }

    public void Reset()
    {
        _timer = 0.0f;
    }
}
