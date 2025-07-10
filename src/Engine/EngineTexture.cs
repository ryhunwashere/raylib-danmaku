using System.Numerics;
using System.Runtime.InteropServices;

namespace RaylibDanmaku.Engine
{
    /// <summary> Texture loading, get texture id, and get texture size.</summary>
    internal partial class EngineTexture
    {
        [LibraryImport("native_renderer.dll", StringMarshalling = StringMarshalling.Utf8)]
        private static partial int LoadTextureFromFile(string path);

        private static readonly Dictionary<string, int> textureMap = [];    // Map from filename → texture ID

        /// <summary> Load a texture asset from file directory. </summary>
        /// <param name="filePath"> Directory to the texture asset e.g. "assets/Player/player1_sprite.png".</param>
        /// <returns> int textureId </returns>
        public static int LoadTexture(string filePath)
        {
            int textureId = LoadTextureFromFile(filePath);

            string fileName = Path.GetFileName(filePath);
            textureMap[fileName] = textureId;

            return textureId;
        }

        /// <summary>
        ///     Get texture ID by filename (e.g. "PlayerBeam1.png"). <br/>
        ///     Throws an error if not found.
        /// </summary>
        /// <param name="fileName"> File name of the texture asset e.g. "player1_sprite.png" </param>
        /// <returns> Texture ID of the provided file name (int). </returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static int GetTextureId(string fileName)
        {
            if (textureMap.TryGetValue(fileName, out var id))
                return id;
            else
                throw new InvalidOperationException($"[EngineTexture] Texture ID not found for: {fileName}");
        }

        [LibraryImport("native_renderer.dll")]
        private static partial void GetTextureSizeNative(int textureId, out float width, out float height);

        /// <summary> Get width and height in Vector2 of a texture from provided textureId. </summary>
        public static Vector2 GetTextureSize(int textureId)
        {
            GetTextureSizeNative(textureId, out float width, out float height);
            return new Vector2(width, height);
        }

        /// <summary> Get height in float of a texture from provided textureId. </summary>
        public static float GetTextureHeight(int textureId)
        {
            Vector2 textureSize = GetTextureSize(textureId);
            return textureSize.Y;
        }
        
        /// <summary> Get width in float of a texture from provided textureId. </summary>
        public static float GetTextureWidth(int textureId)
        {
            Vector2 textureSize = GetTextureSize(textureId);
            return textureSize.X;
        }
    }
}