using System;
using System.Linq;
using XForms.Dependency;

namespace XForms.Store
{

    public static class LinkedObjectHelper
    {
        static IStore Store => MapProvider.Instance.ResolveType<IStore>();

        public static void UpdateStoredReferences(IStorable storable)
        {
            var properties = storable.GetType().GetProperties().AsEnumerable()
                .Where(p => p.GetCustomAttributes(typeof(LinkedObjectAttribute), true).FirstOrDefault() != null)
                .ToList();

            foreach (var property in properties)
            {
                var value = property.GetValue(storable, null);

                if (value != null)
                {
                    var persistentRef = value as IStorable;
                    if (persistentRef != null)
                    {
                        var fromStore = Store.Load(persistentRef.GetType(), persistentRef.MappingId);

                        if (fromStore == null)
                        {
                            property.SetValue(storable, null, null);
                        }
                    }
                }
            }
        }
    }
}
