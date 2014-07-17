using System.Collections.Generic;
using System;

namespace Trigger.CRM.Persistent
{
    public interface IStore
    {
        void Save(Type type, IPersistentId item);

        void Save<T>(T item) where T: IPersistentId;

        void DeleteById(Type type, object itemId);

        void DeleteById<T>(object itemId) where T: IPersistentId;

        void Delete(Type type, IPersistentId item);

        void Delete<T>(T item) where T: IPersistentId;

        IPersistentId Load(Type type, object itemId);

        T Load<T>(object itemId) where T: IPersistentId;

        IEnumerable<IPersistentId>LoadAll(Type type);

        IEnumerable<T> LoadAll<T>() where T: IPersistentId;
    }
}
