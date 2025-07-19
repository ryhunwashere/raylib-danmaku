using System.Numerics;

using RaylibDanmaku.Structs;

namespace RaylibDanmaku.Entities.WeaponTypes;

/// <summary> Bullet owner to be used for collision detection between player & enemy </summary>
public enum BulletOwner { Player, Enemy }

/// <summary> 'Rect' or 'Circle'. <br /> Used to determine the collision shape of the bullet. </summary>
public enum BulletType { Rect, Circle }

/// <summary> Represents a single bullet object. <br /> Can be owned by either player or enemy. </summary>
internal struct Bullet
{
    public BulletType BulletType;
    public BulletOwner BulletOwner;
    public Vector2 Direction;
    public float Speed;
    public float Scale;
    public float Rotation;
    public NativeColor Color;
    public int TextureId;
    public float Lifetime;
    public bool Active;
    public Vector2 Position;
}
