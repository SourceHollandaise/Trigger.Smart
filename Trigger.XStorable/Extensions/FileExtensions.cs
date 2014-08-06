using Trigger.XStorable.Dependency;
using Trigger.XStorable.DataStore;
using System.IO;

namespace System.IO
{
    public static class FileExtensions
    {
        public static string GetValidPath(this string fileName)
        {
            var storeConfig = DependencyMapProvider.Instance.ResolveInstance<IStoreConfiguration>();

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
	