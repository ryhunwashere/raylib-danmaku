using System.Diagnostics;
namespace RaylibDanmaku.Core
{
    /// <summary>
    /// Time class mainly to calculate delta time between frames.
    /// </summary>
    internal static class Time
    {
        private static Stopwatch stopwatch = Stopwatch.StartNew();
        private static float lastTime = 0f;

        /// <summary>
        /// Get time between frames in seconds.
        /// </summary>
        public static float DeltaTime { get; private set; }

        public static void Update()
        {
            float currentTime = (float)stopwatch.Elapsed.TotalSeconds;
            DeltaTime = currentTime - lastTime;
            lastTime = currentTime;
        }

    }
}

