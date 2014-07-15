using System.Collections.Generic;

namespace Trigger.CRM.Persistent
{
    public interface IStore<T> where T: IStorable
    {
        void Save(T item);

        T Load(object itemId);

        IEnumerable<T>LoadAll();

        void Delete(object itemId);
    }
}
