using System.Diagnostics;

namespace RaylibDanmaku.Core;

/// <summary>
/// Time class mainly to calculate delta time between frames.
/// </summary>
internal static class Time
{
    private static readonly Stopwatch _stopwatch = Stopwatch.StartNew();
    private static float _lastTime = 0f;

    /// <summary>
    /// Get time between frames in seconds.
    /// </summary>
    public static float DeltaTime { get; private set; }

    public static void Update()
    {
        float currentTime = (float)_stopwatch.Elapsed.TotalSeconds;
        DeltaTime = currentTime - _lastTime;
        _lastTime = currentTime;
    }

}
