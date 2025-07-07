using System.Runtime.InteropServices;
namespace RaylibDanmaku.Core
{
    /// <summary>
    /// Native renderer written in C, because rendering in C# causes stuttering issues.
    /// </summary>
    internal static partial class Render
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

        [LibraryImport("native_renderer.dll")]
        public static partial int IsKeyDownNative(int key);

        [LibraryImport("native_renderer.dll")]
        public static partial int IsKeyPressedNative(int key);
    }
}
