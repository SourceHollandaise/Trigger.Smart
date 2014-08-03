using Eto.Drawing;
using System.IO;
using System;

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
            if (!File.Exists(imageName))
                return GetImageFromResource(imageName, size);

            return GetImageFromFile(imageName, size);
        }

        public static Image GetImageFromResource(string imageName, int size = 32)
        {
            if (!imageName.EndsWith(".png"))
                imageName += ".png";

            Image bitMap = Bitmap.FromResource(imageName);
            if (size > bitMap.Height)
                size = bitMap.Height;

            var image = new Bitmap(bitMap, size, size);
            bitMap.Dispose();
            return image;
        }

        public static Image GetImageFromFile(string fileName, int size = 32)
        {
            Image bitMap = new Bitmap(fileName);
            if (size > bitMap.Height)
                size = bitMap.Height;

            var image = new Bitmap(bitMap, size, size);
            bitMap.Dispose();
            return image;
        }
    }

    public static class ImageHelpers
    {
        public static Bitmap Scale(this Bitmap sourceBitmap, int maxWidth, int maxHeight)
        {  
            // original dimensions  
            int width = sourceBitmap.Width;  
            int height = sourceBitmap.Height;  

            // Find the longest and shortest dimentions  
            int longestDimension = (width > height) ? width : height;  
            int shortestDimension = (width < height) ? width : height;  

            double factor = ((double)longestDimension) / (double)shortestDimension;  

            // Set width as max  
            double newWidth = maxWidth;  
            double newHeight = maxWidth / factor;  

            //If height is actually greater, then we reset it to use height instead of width  
            if (width < height)
            {  
                newWidth = maxHeight / factor;  
                newHeight = maxHeight;  
            }  

            // Create new Bitmap at new dimensions based on original bitmap  
            Bitmap resizedBitmap = new Bitmap((int)newWidth, (int)newHeight, PixelFormat.Format32bppRgb);  

            using (Graphics g = new Graphics(resizedBitmap))
            {
                g.DrawImage(sourceBitmap, 0, 0, (int)newWidth, (int)newHeight);
            }

            return resizedBitmap; 
        }
    }
}
