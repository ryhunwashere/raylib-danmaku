using System.Runtime.InteropServices;
namespace RaylibDanmaku.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct NativeColor
    {
        public readonly byte r;
        public readonly byte g;
        public readonly byte b;
        public readonly byte a;
        public static readonly NativeColor Black = new(0, 0, 0, 255);
        public static readonly NativeColor White = new(255, 255, 255, 255);
        public static readonly NativeColor Red = new(255, 0, 0, 255);

        public NativeColor(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }
}