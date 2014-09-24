using Eto.Drawing;
using System.IO;
using XForms.Store;

namespace Eto.Drawing
{
    public static class ImageExtensions
    {
        public static Image ConvertToImage(this IFileData fileData)
        {
            var value = fileData.FileName;
            if (!string.IsNullOrWhiteSpace(value))
            {
                var file = value.GetValidPath();
                if (file != null)
                {
                    try
                    {
                        var image = new Bitmap(file);

                        return image;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        public static Image GetImage(string imageName, int size = 32)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                return null;

            try
            {
                if (!File.Exists(imageName))
                    return GetImageFromResource(imageName, size);

                return GetImageFromFile(imageName, size);
            }
            catch
            {
                return null;
            }
        }

        static Image GetImageFromResource(string imageName, int size = 32)
        {
            if (!imageName.EndsWith(".png"))
                imageName += ".png";

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            Image bitMap = Bitmap.FromResource(imageName, assembly);
            if (size > bitMap.Height)
                size = bitMap.Height;

            var image = new Bitmap(bitMap, size, size);
            bitMap.Dispose();
            return image;
        }

        static Image GetImageFromFile(string fileName, int size = 32)
        {
            Image bitMap = new Bitmap(fileName);
            if (size > bitMap.Height)
                size = bitMap.Height;
                
            var image = new Bitmap(bitMap, size, size);

            bitMap.Dispose();
            return image;
        }
    }
}
