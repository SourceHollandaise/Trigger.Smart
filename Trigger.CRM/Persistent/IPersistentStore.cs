using System.Collections.Generic;

namespace Trigger.CRM.Persistent
{
    public interface IPersistentStore<T> where T: IPersistentId
    {
        string Save(T item);

        T Load(object itemId);

        IEnumerable<T>LoadAll();

        void Delete(object itemId);
    }
}
