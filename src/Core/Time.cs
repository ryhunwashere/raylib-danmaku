// Time module mainly to calculate delta time.
using System.Diagnostics;
namespace RaylibDanmaku.Core
{
    internal static class Time
    {
        private static Stopwatch stopwatch = Stopwatch.StartNew();
        private static float lastTime = 0f;
        public static float DeltaTime { get; private set; }

        public static void Update()
        {
            float currentTime = (float)stopwatch.Elapsed.TotalSeconds;
            DeltaTime = currentTime - lastTime;
            lastTime = currentTime;
        }

    }
}

