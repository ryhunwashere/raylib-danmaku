// Keyboard input bindings to the native renderer
using Raylib_cs;
namespace RaylibDanmaku.Core
{
    internal static class Input
    {
        /// <summary> Returns true if the given key is currently held down. </summary>
        public static bool IsKeyDown(KeyboardKey key)
        {
            return Render.IsKeyDownNative((int)key) != 0;
        }

        /// <summary> Returns true if the key was pressed on this frame. </summary>
        public static bool IsKeyPressed(KeyboardKey key)
        {
            return Render.IsKeyPressedNative((int)key) != 0;
        }
    }
}