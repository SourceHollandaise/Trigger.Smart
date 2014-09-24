using System.IO;
using XForms.Dependency;
using XForms.Store;

namespace System.IO
{
    public static class FileExtensions
    {
        public static string GetValidPath(this string fileName)
        {
            var storeConfig = MapProvider.Instance.ResolveInstance<IStoreConfiguration>();

            if (!string.IsNullOrEmpty(fileName))
            {
                var imagePath = Path.Combine(storeConfig.DocumentStoreLocation, fileName);
                if (File.Exists(imagePath))
                    return imagePath;
            }

            return null;
        }
    }

    public static class FileInfoExtensions
    {
        public static bool IsSupportedDocument(this FileInfo fileInfo)
        {
            var ext = fileInfo.Extension.ToLower();

            if (ext.EndsWith("pdf")
                || ext.EndsWith("doc")
                || ext.EndsWith("docx")
                || ext.EndsWith("txt")
                || ext.EndsWith("rtf"))
                return true;

            return false;
        }

        public static bool IsSupportedImage(this FileInfo fileInfo)
        {
            var ext = fileInfo.Extension.ToLower();

            if (ext.EndsWith("jpg")
                || ext.EndsWith("jpeg")
                || ext.EndsWith("png")
                || ext.EndsWith("gif")
                || ext.EndsWith("bmp")
                || ext.EndsWith("tif")
                || ext.EndsWith("ico")
                || ext.EndsWith("icns"))
                return true;

            return false;

        }
    }
}
	