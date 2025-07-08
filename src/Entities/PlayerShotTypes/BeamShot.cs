using System.Numerics;
using RaylibDanmaku.Core;
namespace RaylibDanmaku.Entities
{
    internal class BeamShot(Player player, BeamManager beamManager) : IPlayerShot
    {
        private readonly Player player = player;
        private readonly BeamManager beamManager = beamManager;

        private void SpawnPlayerBeam(float offsetX)
        {
            Vector2 spawnPos = player.Position;
            float beamScale = 1.8f;
            float beamRotation = -90.0f;
            NativeColor beamTint = NativeColor.Transparent;

            beamManager.SpawnBeam(
                BeamOwner.PLAYER,
                spawnPos,
                beamRotation,
                beamScale,
                beamTint,
                BeamManager.PlayerBeamTextureId,
                () => player.Position);
        }

        public void Shoot(int powerLevel)
        {
            switch (powerLevel)
            {
                case 0:
                    SpawnPlayerBeam(0.0f);
                    break;
                case 1:
                    SpawnPlayerBeam(20.0f);
                    SpawnPlayerBeam(-20.0f);
                    break;
            }
            
        }
    }
}