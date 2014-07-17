using System;
using System.Linq;
using Trigger.CRM.Model;
using Trigger.CRM.Persistent;
using Trigger.CRM.Security;
using Trigger.Dependency;

namespace Trigger.CRM.Persistent
{

    public static class PersistentReferenceHelper
    {
        static IStore Store
        {
            get
            {
                return DependencyMapProvider.Instance.ResolveType<IStore>();
            }
        }

        public static void UpdatePersistentReferences(IPersistentId target)
        {
            var properties = target.GetType().GetProperties().AsEnumerable()
                .Where(p => p.GetCustomAttributes(typeof(PersistentReferenceAttribute), true).Length > 0).ToList();

            foreach (var property in properties)
            {
                var value = property.GetValue(target, null);

                if (value != null)
                {
                    var persistent = value as IPersistentId;
                    if (persistent != null)
                    {
                        var loaded = Store.Load(persistent.GetType(), persistent.MappingId);

                        if (loaded == null)
                        {
                            property.SetValue(target, null, null);
                        }
                    }
                }
            }
        }
    }
}
