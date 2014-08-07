
namespace XForms.Dependency
{
    public static class MapProvider
    {
        readonly static object _lock = new object();
		
        static IDependencyMap map;

        public static IDependencyMap Instance
        {
            get
            {
                lock (_lock)
                {
                    if (map == null)
                        map = new DependencyMap();
                    return map;
                }
            }
        }

        public static void Destroy()
        {
            map.Dispose();
            map = null;
        }
    }
}

