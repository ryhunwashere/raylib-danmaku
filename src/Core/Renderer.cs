// Native renderer written in C, because rendering in C# causes stuttering issues.
// nativerenderer.dll is located in lib folder.
using System.Runtime.InteropServices;
namespace RaylibDanmaku.Core
{
    internal static partial class Render
    {
        [LibraryImport("nativerenderer.dll", StringMarshalling = StringMarshalling.Utf8)]
        public static partial void InitRender(int screenWidth, int screenHeight, string windowName, int targetFPS);

        [LibraryImport("nativerenderer.dll")]
        public static partial void ShutdownRender();

        [LibraryImport("nativerenderer.dll", StringMarshalling = StringMarshalling.Utf8)]
        public static partial int LoadTextureFromFile(string filePath);

        [LibraryImport("nativerenderer.dll")]
        public static partial void RenderFrame(float playerX, float playerY, int textureId, float textureScale);

        [LibraryImport("nativerenderer.dll")]
        public static partial int NativeWindowShouldClose();

        [LibraryImport("nativerenderer.dll")]
        public static partial int IsKeyDownNative(int key);

        [LibraryImport("nativerenderer.dll")]
        public static partial int IsKeyPressedNative(int key);
    }
}