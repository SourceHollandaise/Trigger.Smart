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
}
	