using System.Diagnostics;
using System.Numerics;

using RaylibDanmaku.Core;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Entities.WeaponTypes;

namespace RaylibDanmaku.Entities.WeaponManagers
{
    public enum BeamOwner { PLAYER, ENEMY }

    /// <summary>
    /// Handles how to spawn or move a beam object.
    /// </summary>
    internal class BeamManager
    {
        private const int MAX_BEAMS = 128;
        private readonly Beam[] playerBeams = new Beam[MAX_BEAMS];
        private readonly Beam[] enemyBeams = new Beam[MAX_BEAMS];

        public static int PlayerBeamTextureId;
        private const int PLAYER_BEAM_LAYER = 3;
        private const int ENEMY_BEAM_LAYER = 4;


        /// <summary>
        /// Initialize all beam slots so they're ready
        /// </summary>
        public BeamManager()
        {
            for (int i = 0; i < MAX_BEAMS; i++)
            {
                playerBeams[i] = new Beam();
                enemyBeams[i] = new Beam();
            }
        }

        public void SpawnBeam(
            BeamOwner owner,
            Vector2 startPosition,
            float rotation,
            float scale,
            NativeColor tint,
            int textureId,
            Func<Vector2>? followTargetFunc = null)
        {
            Beam[] pool = owner == BeamOwner.PLAYER ? playerBeams : enemyBeams;

            for (int i = 0; i < pool.Length; i++)
            {
                if (!pool[i].Active)
                {
                    pool[i].Activate(owner, startPosition, rotation, scale, tint, textureId, followTargetFunc);
                    return;
                }
            }
            Trace.TraceWarning("[BeamManager.SpawnBeam()] Too many active beams! Max: " + MAX_BEAMS);
        }

        public void Update()
        {
            UpdatePool(playerBeams);
            // UpdatePool(enemyBeams);
        }

        private void UpdatePool(Beam[] pool)
        {
            foreach (var beam in pool)
            {
                if (!beam.Active) continue;
                // Console.WriteLine($"[BeamManager.UpdatePool()] Beam is active at pos: {beam.Position}");

                if (beam.FollowTargetFunc != null)
                {
                    beam.Position = beam.FollowTargetFunc();
                }
            }
        }

        public void Draw()
        {
            // Console.WriteLine("[BeamManager.Draw()] Draw called");
            DrawPool(playerBeams, PLAYER_BEAM_LAYER);
            DrawPool(enemyBeams, ENEMY_BEAM_LAYER);
        }

        private static void DrawPool(Beam[] pool, int layer)
        {
            foreach (var beam in pool)
            {
                if (!beam.Active) continue;

                // Console.WriteLine($"[BeamManager.DrawPool()] Drawing beam at pos: {beam.Position}, layer: {layer}");
                EngineRender.QueueDraw(
                    beam.TextureId,
                    beam.Position.X,
                    beam.Position.Y,
                    beam.Scale,
                    beam.Rotation,
                    beam.Tint,
                    layer
                );
            }
        }
    }
}