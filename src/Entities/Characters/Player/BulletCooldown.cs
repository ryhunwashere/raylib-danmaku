namespace RaylibDanmaku.Entities.Characters;

internal class PlayerBulletCooldown(float cooldownTime)
{
    private readonly float cooldown = cooldownTime;
    private float timer = cooldownTime;

    public void Update(float dt) => timer += dt;
    public bool IsReady => timer >= cooldown;
    public void Reset() => timer = 0.0f;
}
