using RaylibDanmaku.Entities.ShotTypes;

namespace RaylibDanmaku.Entities.Characters.Player;

internal class PlayerShooting(float cooldownTime)
{
    private PlayerShotType? _shotType;
    private readonly BulletCooldown _bulletCooldown = new(cooldownTime);

    public void SetShotType(PlayerShotType? shotType)
    {
        _shotType = shotType;
    }

    public void Update(float deltaTime)
    {
        _bulletCooldown.Update(deltaTime);
        _shotType?.Update(deltaTime);
    }

    public void Shoot(Player player, PlayerPower playerPower)
    {
        if (_bulletCooldown.IsReady)
        {
            _shotType?.Shoot(player, playerPower);
            _bulletCooldown.Reset();
        }
    }

    // Only for beams
    public void StopShoot(Player player)
    {
        _shotType?.StopShoot(player);
    }

    public void RestartBeam(Player player, PlayerPower playerPower)
    {
        _shotType?.StopShoot(player);
        _shotType?.Shoot(player, playerPower);
    }
}
