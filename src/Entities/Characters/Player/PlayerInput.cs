using System.Numerics;

using Raylib_cs;
using RaylibDanmaku.Core;

namespace RaylibDanmaku.Entities.Characters.Player;

internal class PlayerInput
{
    public Vector2 GetMovement(float moveSpeed, float slowMoveSpeed, float deltaTime)
    {
        var speed = Input.IsKeyDown(KeyboardKey.LeftShift) ? slowMoveSpeed : moveSpeed;

        Vector2 move = Vector2.Zero;

        if (Input.IsKeyDown(KeyboardKey.Left)) move.X -= speed * deltaTime;
        if (Input.IsKeyDown(KeyboardKey.Right)) move.X += speed * deltaTime;
        if (Input.IsKeyDown(KeyboardKey.Up)) move.Y -= speed * deltaTime;
        if (Input.IsKeyDown(KeyboardKey.Down)) move.Y += speed * deltaTime;

        return move;
    }

    // NOTE: Do not turn anything below here to static
    public bool ShootBullet => Input.IsKeyDown(KeyboardKey.X);
    public bool ShootBeam => Input.IsKeyPressed(KeyboardKey.X);
    public bool StopBeam => Input.IsKeyReleased(KeyboardKey.X);
    public bool UseBomb => Input.IsKeyPressed(KeyboardKey.Space);
    public bool PowerUp => Input.IsKeyPressed(KeyboardKey.KpAdd);
    public bool PowerDown => Input.IsKeyPressed(KeyboardKey.KpSubtract);
    public bool IsBeamHeld => Input.IsKeyDown(KeyboardKey.X);
}
