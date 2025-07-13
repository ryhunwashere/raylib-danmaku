using System.Diagnostics;
using System.Numerics;

using RaylibDanmaku.Structs;
using RaylibDanmaku.Engine;
using RaylibDanmaku.Entities.WeaponTypes;

namespace RaylibDanmaku.Entities.WeaponManagers;

public enum BeamOwner { PLAYER, ENEMY }

/// <summary>
/// Handles how to spawn or move a beam object.
/// </summary>
internal class BeamManager
{
    private const int MaxBeams = 64;
    private readonly Beam[] _playerBeams = new Beam[MaxBeams];
    private readonly Beam[] _enemyBeams = new Beam[MaxBeams];

    public static int PlayerBeamTextureId;
    private const int PlayerBeamLayer = 2;
    

    /// <summary> Initialize all beam slots so they're ready </summary>
    public BeamManager()
    {
        for (int i = 0; i < MaxBeams; i++)
        {
            _playerBeams[i] = new Beam();
            _enemyBeams[i] = new Beam();
        }
    }

    public Beam? SpawnBeam(
        BeamOwner owner,
        Vector2 startPosition,
        float rotation,
        float scale,
        NativeColor tint,
        int textureId,
        Func<Vector2>? followTargetFunc = null)
    {
        Beam[] pool = owner == BeamOwner.PLAYER ? _playerBeams : _enemyBeams;
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].Active)
            {
                pool[i].Activate(owner, startPosition, rotation, scale, tint, textureId, followTargetFunc);
                return pool[i];
            }
        }
        Trace.TraceWarning("[BeamManager.SpawnBeam()] Too many active beams! Max: " + MaxBeams);
        return null;
    }

    public void Update()
    {
        UpdatePool(_playerBeams);
        // UpdatePool(_enemyBeams);
    }

    private static void UpdatePool(Beam[] pool)
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

        DrawPool(_playerBeams, PlayerBeamLayer);
        DrawActiveBeamCount();
        // DrawPool(enemyBeams, EnemyBeamLayer);
    }

    private static void DrawPool(Beam[] pool, int layer)
    {
        foreach (var beam in pool)
        {
            if (!beam.Active) continue;

            EngineRender.QueueDrawTexture(
                    beam.TextureId,
                    beam.Position.X,
                    beam.Position.Y,
                    beam.Scale,
                    beam.Rotation,
                    beam.Color,
                    layer);
        }
    }

    private void DrawActiveBeamCount()
    {
        EngineRender.QueueDrawText("Active beams: " + CountActiveBeams(), x: 10, y: 80, fontSize: 20, color: NativeColor.White, layer: 100);
    }

    private int CountActiveBeams()
    {
        int activeBeamCount = 0;
        for (int i = 0; i < MaxBeams; i++)
        {
            if (_playerBeams[i].Active) activeBeamCount++;
        }
        return activeBeamCount;
    }
}
