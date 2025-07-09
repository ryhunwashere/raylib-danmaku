using System.Numerics;

using RaylibDanmaku.Core;
using RaylibDanmaku.Entities.WeaponManagers;

namespace RaylibDanmaku.Entities.WeaponTypes
{
    /// <summary>
    /// Represents a single beam object.
    /// </summary>
    internal class Beam
    {
        public BeamOwner Owner;
        public Vector2 Position;
        public float Rotation;
        public float Scale;
        public NativeColor Tint;
        public int TextureId;
        public bool Active;
        // public Vector2 Origin;

        // Optional: makes beam always stick to something, like the player or boss
        public Func<Vector2>? FollowTargetFunc;

        // Activate the beam
        public void Activate(
            BeamOwner owner,
            Vector2 startPos,
            float rotation,
            float scale,
            NativeColor tint,
            int textureId,
            Func<Vector2>? followTargetFunc = null)
        {
            Owner = owner;
            Position = startPos;
            Rotation = rotation;
            Scale = scale;
            Tint = tint;
            TextureId = textureId;
            Active = true;
            FollowTargetFunc = followTargetFunc;

            // Console.WriteLine("[Beam.Activate()] Activate called.");
        }

        // Deactivate this beam
        public void Deactivate()
        {
            Active = false;
            FollowTargetFunc = null;
        }
    }
}