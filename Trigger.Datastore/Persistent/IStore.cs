using System.Collections.Generic;
using System;

namespace Trigger.Datastore.Persistent
{
    public interface IStore
    {
        void Save(Type type, IPersistent item);

        void Save<T>(T item) where T: IPersistent;

        void DeleteById(Type type, object itemId);

        void DeleteById<T>(object itemId) where T: IPersistent;

        void Delete(Type type, IPersistent item);

        void Delete<T>(T item) where T: IPersistent;

        IPersistent Load(Type type, object itemId);

        T Load<T>(object itemId) where T: IPersistent;

        IEnumerable<IPersistent>LoadAll(Type type);

        IEnumerable<T> LoadAll<T>() where T: IPersistent;
    }
}
