using System.Runtime.InteropServices;

using RaylibDanmaku.Core;

namespace RaylibDanmaku.Engine
{
    /// <summary>
    /// Interop to rendering engine backend in native C, because rendering in C# causes stuttering issues.
    /// </summary>
    internal static partial class EngineRender
    {
        [LibraryImport("native_renderer.dll")]
        public static partial void QueueDraw(int textureId, float x, float y, float scale, float rotation, NativeColor tint, int layer);

        [LibraryImport("native_renderer.dll")]
        public static partial void RenderFrame();

        [LibraryImport("native_renderer.dll", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void InitRender(int screenWidth, int screenHeight, string windowName, int targetFPS);

        [LibraryImport("native_renderer.dll")]
        public static partial int NativeWindowShouldClose();

        [LibraryImport("native_renderer.dll")]
        public static partial void ShutdownRender();

        [LibraryImport("native_renderer.dll", StringMarshalling = StringMarshalling.Utf8)]
        public static partial int LoadTextureFromFile(string filePath);
    }
}
