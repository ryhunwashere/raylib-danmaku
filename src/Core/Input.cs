using Raylib_cs;
using RaylibDanmaku.Engine;

namespace RaylibDanmaku.Core;

/// <summary>
/// Keyboard input bindings to the native renderer.
/// </summary>
internal static class Input
{
    /// <summary> Returns true if the given key is currently held down. </summary>
    public static bool IsKeyDown(KeyboardKey key)
    {
        return EngineInput.IsKeyDownNative((int)key) != 0;
    }

    /// <summary> Returns true if the key was pressed on this frame. </summary>
    public static bool IsKeyPressed(KeyboardKey key)
    {
        return EngineInput.IsKeyPressedNative((int)key) != 0;
    }

    /// <summary> Returns true if the key was released on this frame. </summary>
    public static bool IsKeyReleased(KeyboardKey key)
    {
        return EngineInput.IsKeyReleasedNative((int)key) != 0;
    }
}
