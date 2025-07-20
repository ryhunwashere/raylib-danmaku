using RaylibDanmaku.Entities.Characters.Player;

namespace RaylibDanmaku.Entities.ShotTypes;

internal abstract class PlayerShotType
{
    public abstract void Shoot(Player player, PlayerPower power);
    public virtual void StopShoot(Player player) { }
    public virtual void Update(float deltaTime) { }
    public virtual List<WeaponTypes.Beam> GetActiveBeams() => [];
}