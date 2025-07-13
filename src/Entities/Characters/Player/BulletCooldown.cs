namespace RaylibDanmaku.Entities.Characters.Player;

internal class BulletCooldown(float cooldownTime)
{
    private readonly float cooldown = cooldownTime;
    private float timer = cooldownTime;
    public bool IsReady => timer >= cooldown;

    public void Update(float deltaTime)
    {
        timer += deltaTime;
    }

    public void Reset()
    {
        timer = 0.0f;
    }
}
