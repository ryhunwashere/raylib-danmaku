using System.Runtime.InteropServices;
namespace RaylibDanmaku.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct NativeColor(byte r, byte g, byte b, byte a)
    {
        public readonly byte r = r;
        public readonly byte g = g;
        public readonly byte b = b;
        public readonly byte a = a;
        public static readonly NativeColor Black = new(0, 0, 0, 255);
        public static readonly NativeColor White = new(255, 255, 255, 255);
        public static readonly NativeColor Transparent = new(255, 255, 255, 150);
        public static readonly NativeColor Red = new(255, 0, 0, 255);
        public static readonly NativeColor Green = new(108, 239, 39, 255);
    }
}