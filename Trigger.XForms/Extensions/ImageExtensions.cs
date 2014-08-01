using Eto.Drawing;
using System.IO;

namespace Eto.Drawing
{
    public struct ImageSize
    {
        public const int Size16 = 16;
        public const int Size24 = 24;
        public const int Size32 = 32;
        public const int Size48 = 48;
    }

    public static class ImageExtensions
    {
        public static Image GetImage(string imageName, int size = 32)
        {
            Image bitMap = Bitmap.FromResource(imageName);
            if (size > bitMap.Height)
                size = bitMap.Height;

            var image = new Bitmap(bitMap, size, size);
            bitMap.Dispose();
            return image;
        }
    }
}
