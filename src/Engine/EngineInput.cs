using System.Runtime.InteropServices;

namespace RaylibDanmaku.Engine
{
    /// <summary>
    /// Interop for key inputs to the engine.
    /// </summary>
    internal static partial class EngineInput
    {
        [LibraryImport("native_renderer.dll")]
        public static partial int IsKeyDownNative(int key);

        [LibraryImport("native_renderer.dll")]
        public static partial int IsKeyPressedNative(int key);

        [LibraryImport("native_renderer.dll")]
        public static partial int IsKeyReleasedNative(int key);
    }
}
