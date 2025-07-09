using System.Numerics;

using RaylibDanmaku.Core;
using RaylibDanmaku.Entities.Characters;
using RaylibDanmaku.Entities.WeaponManagers;
using RaylibDanmaku.Entities.WeaponTypes;

namespace RaylibDanmaku.Entities.PlayerShotTypes
{
    internal class BeamShot(Player player, BeamManager beamManager) : IPlayerShot
    {
        private readonly Player player = player;
        private readonly BeamManager beamManager = beamManager;

        public List<Beam> activePlayerBeams = [];

        private void SpawnPlayerBeam(float offsetX, float offsetY)
        {
            Vector2 spawnPos = player.Position;
            float beamScale = 1.8f;
            float beamRotation = -90.0f;
            NativeColor beamTint = NativeColor.Transparent;

            var beam = beamManager.SpawnBeam(
                BeamOwner.PLAYER,
                spawnPos,
                beamRotation,
                beamScale,
                beamTint,
                BeamManager.PlayerBeamTextureId,
                followTargetFunc: () => player.Position + new Vector2(offsetX, offsetY)
                );
            if (beam != null)
            {
                activePlayerBeams.Add(beam);
            }
        }

        public void ShootBeam(int powerLevel)
        {
            float beamLength = -250.0f;
            if (activePlayerBeams.Count == 0)  // only spawn beams if none are active
            {
                switch (powerLevel)
                {
                    case 0:
                        SpawnPlayerBeam(offsetX: 0.0f, offsetY: beamLength);
                        break;
                    case 1:
                        SpawnPlayerBeam(offsetX: 30.0f, offsetY: beamLength);
                        SpawnPlayerBeam(offsetX: -30.0f, offsetY: beamLength);
                        break;
                    case 2:
                        SpawnPlayerBeam(offsetX: 50.0f, offsetY: beamLength);
                        SpawnPlayerBeam(offsetX: 0.0f, offsetY: beamLength);
                        SpawnPlayerBeam(offsetX: -50.0f, offsetY: beamLength);
                        break;
                    case 3:
                        SpawnPlayerBeam(offsetX: 60.0f, offsetY: beamLength);
                        SpawnPlayerBeam(offsetX: 20.0f, offsetY: beamLength);
                        SpawnPlayerBeam(offsetX: -20.0f, offsetY: beamLength);
                        SpawnPlayerBeam(offsetX: -60.0f, offsetY: beamLength);
                        break;
                    case 4:
                        SpawnPlayerBeam(offsetX: 100.0f, offsetY: beamLength);
                        SpawnPlayerBeam(offsetX: 50.0f, offsetY: beamLength);
                        SpawnPlayerBeam(offsetX: 0.0f, offsetY: beamLength);
                        SpawnPlayerBeam(offsetX: -50.0f, offsetY: beamLength);
                        SpawnPlayerBeam(offsetX: -100.0f, offsetY: beamLength);
                        break;
                }
            }
        }
        
        public void StopShootBeam()
        {
            foreach (var beam in activePlayerBeams)
            {
                beam.Deactivate();
            }
            activePlayerBeams.Clear();
        }

        public void ShootBullet(int powerLevel) { } // useless function meant for bullets
    }
}