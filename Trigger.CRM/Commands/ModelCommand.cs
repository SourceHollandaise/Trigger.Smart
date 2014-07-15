using System;
using Trigger.Dependency;
using Trigger.CRM.Persistent;
using System.Collections.Generic;
using System.Linq;

namespace Trigger.CRM.Commands
{
    public abstract class ModelCommand<T> where T: IStorable
    {
        protected IDependencyMap Map
        {
            get
            { 
                return DependencyMapProvider.Instance;
            }
        }

        protected IStore<T> Store
        {
            get
            {
                return Map.ResolveType<IStore<T>>();
            }
        }

        public virtual void Save(T item)
        {
            Store.Save(item);
        }

        public virtual void Delete(object id)
        {
            Store.Delete(id);
        }

        public virtual IEnumerable<T> GetObjects(Func<T, bool> function)
        {
            return Store.LoadAll().Where(function);
        }

        public virtual IEnumerable<T> GetObjects()
        {
            return Store.LoadAll();
        }

        public abstract string GetRepresentation(T item);

    }
    
}
