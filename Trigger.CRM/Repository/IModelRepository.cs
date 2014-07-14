using System.Collections.Generic;
using System;

namespace Trigger.CRM.Repository
{
    public interface IModelRepository<T>
    {
        event RepositoryHandler ItemAdded;

        event RepositoryHandler ItemRemoved;

        void Add(T item, Func<T, bool> func = null);

        IEnumerable<T> Get(Func<T, bool> func);

        void Remove(Func<T, bool> func);
    }
}
