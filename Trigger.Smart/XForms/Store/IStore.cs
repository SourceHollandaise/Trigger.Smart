using System.Collections.Generic;
using System;

namespace XForms.Store
{
    public interface IStore
    {
        void Save(Type type, IStorable item);

        void Save<T>(T item) where T: IStorable;

        void DeleteById(Type type, object itemId);

        void DeleteById<T>(object itemId) where T: IStorable;

        void Delete(Type type, IStorable item);

        void Delete<T>(T item) where T: IStorable;

        IStorable Load(Type type, object itemId);

        T Load<T>(object itemId) where T: IStorable;

        IEnumerable<IStorable>LoadAll(Type type);

        IEnumerable<T> LoadAll<T>() where T: IStorable;

        IEnumerable<IStorable> SearchResult(string input, params Type[] typesToSearch);
    }
}
