using System.Numerics;
using System.Runtime.InteropServices;

namespace RaylibDanmaku.Engine
{
    /// <summary>
    /// Interop for texture loading and get texture size from native side.
    /// </summary>
    internal partial class EngineTexture
    {
        [LibraryImport("native_renderer.dll", StringMarshalling = StringMarshalling.Utf8)]
        public static partial int LoadTextureFromFile(string filePath);

        [LibraryImport("native_renderer.dll")]
        private static partial void GetTextureSizeNative(int textureId, out float width, out float height);

        /// <summary>
        /// Get width and height of a texture from provided textureId.
        /// </summary>
        public static Vector2 GetTextureSize(int textureId)
        {
            GetTextureSizeNative(textureId, out float width, out float height);
            return new Vector2(width, height);
        }
    }
}