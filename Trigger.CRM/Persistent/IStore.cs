using System.Collections.Generic;
using System;

namespace Trigger.CRM.Persistent
{
    public interface IStore
    {
        void Save(Type type, IStorable item);

        void DeleteById(Type type, object itemId);

        void Delete(Type type, IStorable item);

        IStorable Load(Type type, object itemId);

        IEnumerable<IStorable>LoadAll(Type type);

        IEnumerable<T> LoadAll<T>() where T: IStorable;
    }
}
