using System.Numerics;

using RaylibDanmaku.Core;
using RaylibDanmaku.Structs;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Managers;
using RaylibDanmaku.Entities.Characters.Player;

namespace RaylibDanmaku.Entities.ShotTypes.Beam;

internal class MarisaBeamShot : PlayerShotType
{
    private readonly BeamManager _beamManager;
    private readonly List<WeaponTypes.Beam> _activePlayerBeams = [];
    private readonly float _beamHeight;

    public MarisaBeamShot(BeamManager beamManager)
    {
        _beamManager = beamManager;
        int textureId = EngineTexture.GetTextureId("PlayerBeam1.png");
        _beamHeight = EngineTexture.GetTextureHeight(textureId);
    }

    public override void Shoot(Player player, PlayerPower power)
    {
        if (_activePlayerBeams.Count == 0)  // only spawn beams if none are active
        {
            switch (power.PlayerLevel)
            {
                case 0:
                    SpawnPlayerBeam(player, offsetX: 0.0f);
                    break;
                case 1:
                    SpawnPlayerBeam(player, offsetX: 30.0f);
                    SpawnPlayerBeam(player, offsetX: -30.0f);
                    break;
                case 2:
                    SpawnPlayerBeam(player, offsetX: 60.0f);
                    SpawnPlayerBeam(player, offsetX: 0.0f);
                    SpawnPlayerBeam(player, offsetX: -60.0f);
                    break;
                case 3:
                    SpawnPlayerBeam(player, offsetX: 90.0f);
                    SpawnPlayerBeam(player, offsetX: 30.0f);
                    SpawnPlayerBeam(player, offsetX: -30.0f);
                    SpawnPlayerBeam(player, offsetX: -90.0f);
                    break;
                case 4:
                    SpawnPlayerBeam(player, offsetX: 120.0f);
                    SpawnPlayerBeam(player, offsetX: 60.0f);
                    SpawnPlayerBeam(player, offsetX: 0.0f);
                    SpawnPlayerBeam(player, offsetX: -60.0f);
                    SpawnPlayerBeam(player, offsetX: -120.0f);
                    break;
            }
        }
    }

    public override void StopShoot(Player player)
    {
        foreach (var beam in _activePlayerBeams)
            beam.Deactivate();

        _activePlayerBeams.Clear();
    }

    public override List<WeaponTypes.Beam> GetActiveBeams()
    {
        return _activePlayerBeams;
    }

    private void SpawnPlayerBeam(Player player, float offsetX)
    {
        float beamScale = 2.0f;
        float beamRotation = -90.0f;
        NativeColor beamColor = NativeColor.Transparent;
        float scaledBeamHeight = _beamHeight * beamScale;
        int beamCopies = Config.ScreenHeight / (int)scaledBeamHeight + 1;

        for (int i = 0; i < beamCopies; i++)
        {
            float offsetY = -(i * scaledBeamHeight + scaledBeamHeight / 2);

            var beam = _beamManager.SpawnBeam(
                owner: BeamManager.Owner.Player,
                startPos: player.Position,
                rotation: beamRotation,
                scale: beamScale,
                color: beamColor,
                textureId: BeamManager.PlayerBeamTextureId,
                followTargetFunc: () => player.Position + new Vector2(offsetX, offsetY)
            );

            if (beam != null)
                _activePlayerBeams.Add(beam);
        }
    }
}
