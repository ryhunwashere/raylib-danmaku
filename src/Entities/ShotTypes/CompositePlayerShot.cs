using RaylibDanmaku.Entities.Characters.Player;

namespace RaylibDanmaku.Entities.ShotTypes;

/// <summary>
/// Container list for multiple shot types
/// </summary>
/// <param name="shotTypes">List of shot types</param>
internal class CompositePlayerShot(PlayerShotType[] shotTypes) : PlayerShotType
{
    private readonly PlayerShotType[] _shotTypes = shotTypes;
    private readonly List<WeaponTypes.Beam> _activePlayerBeams = [];

    public override void Shoot(Player player, PlayerPower power)
    {
        foreach (var shotType in _shotTypes)
        {
            shotType.Shoot(player, power);
            _activePlayerBeams.AddRange(shotType.GetActiveBeams());
        }
    }

    public override void StopShoot(Player player)
    {
        foreach (var beam in _activePlayerBeams)
            beam.Deactivate();

        _activePlayerBeams.Clear();

        // Also call StopShoot on children to clear their internal lists
        foreach (var shotType in _shotTypes)
            shotType.StopShoot(player);
    }
}