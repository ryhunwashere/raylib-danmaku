using System.Numerics;

using RaylibDanmaku.Core;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Structs;
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
        private readonly float beamHeight = EngineTexture.GetTextureHeight(EngineTexture.GetTextureId("PlayerBeam1.png"));

        private void SpawnPlayerBeam(float offsetX)
        {
            Vector2 beamStartPos = player.Position;
            float beamScale = 2.0f;
            float beamRotation = -90.0f;
            NativeColor beamTint = NativeColor.Transparent;

            float scaledBeamHeight = beamHeight * beamScale;

            int beamCopies = Config.SCREEN_HEIGHT / (int)scaledBeamHeight + 1;

            for (int i = 0; i < beamCopies; i++)
            {
                float offsetY = -(i * scaledBeamHeight + scaledBeamHeight / 2);

                var beam = beamManager.SpawnBeam(
                    BeamOwner.PLAYER,
                    beamStartPos,
                    beamRotation,
                    beamScale,
                    beamTint,
                    textureId: BeamManager.PlayerBeamTextureId,
                    followTargetFunc: () => player.Position + new Vector2(offsetX, offsetY)
                );

                if (beam != null)
                    activePlayerBeams.Add(beam);
            }
        }

        public void ShootBeam(int powerLevel)
        {
            if (activePlayerBeams.Count == 0)  // only spawn beams if none are active
            {
                switch (powerLevel)
                {
                    case 0:
                        SpawnPlayerBeam(0.0f);
                        break;
                    case 1:
                        SpawnPlayerBeam(offsetX: 30.0f);
                        SpawnPlayerBeam(offsetX: -30.0f);
                        break;
                    case 2:
                        SpawnPlayerBeam(offsetX: 60.0f);
                        SpawnPlayerBeam(offsetX: 0.0f);
                        SpawnPlayerBeam(offsetX: -60.0f);
                        break;
                    case 3:
                        SpawnPlayerBeam(offsetX: 90.0f);
                        SpawnPlayerBeam(offsetX: 30.0f);
                        SpawnPlayerBeam(offsetX: -30.0f);
                        SpawnPlayerBeam(offsetX: -90.0f);
                        break;
                    case 4:
                        SpawnPlayerBeam(offsetX: 120.0f);
                        SpawnPlayerBeam(offsetX: 60.0f);
                        SpawnPlayerBeam(offsetX: 0.0f);
                        SpawnPlayerBeam(offsetX: -60.0f);
                        SpawnPlayerBeam(offsetX: -120.0f);
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