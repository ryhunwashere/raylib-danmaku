using System.Numerics;
using RaylibDanmaku.Core;
namespace RaylibDanmaku.Entities
{
    /// <summary>
    /// Represents a single bullet object.
    /// </summary>
    internal struct Bullet
    {
        public BulletManager.BulletOwner Owner;
        public BulletManager.BulletType Type;
        public Vector2 Position;
        public Vector2 Direction;
        public float Speed;
        public float Scale;
        public float Rotation;
        public NativeColor Tint;
        public int TextureId;
        public float Lifetime;
        public bool Active;
    }
}