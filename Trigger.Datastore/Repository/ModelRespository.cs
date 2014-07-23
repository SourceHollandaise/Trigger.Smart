using System.Collections.Generic;
using System;
using System.Linq;
using Trigger.Datastore.Persistent;

namespace Trigger.Datastore.Repository
{
    public class ModelRespository<T> : IModelRepository<T> where T: IStorable
    {
        public event RepositoryHandler ItemAdded;
        public event RepositoryHandler ItemRemoved;

        readonly List<T> repository = new List<T>();

        public void Add(T item, Func<T, bool> func = null)
        {
            if (func == null)
            {
                if (repository.Contains(item))
                    throw new ArgumentException("Item is already in repository", "item");

                repository.Add(item);

                OnItemAdded(new ModelRepositoryEventArgs(item));
            }
            else
            {
                if (repository.Any(func))
                    throw new ArgumentException("Item is already in repository", "item");

                repository.Add(item);

                OnItemAdded(new ModelRepositoryEventArgs(item));
            }
        }

        public IEnumerable<T> Get(Func<T, bool> func)
        {
            return repository.Where(func);
        }

        public void Remove(Func<T, bool> func)
        {

            while (repository.Any(func))
            {
                OnItemRemoved(new ModelRepositoryEventArgs(repository[0]));

                repository.Remove(repository[0]);
            }
        }

        protected virtual void OnItemAdded(ModelRepositoryEventArgs e)
        {
            var handler = ItemAdded;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void OnItemRemoved(ModelRepositoryEventArgs e)
        {
            var handler = ItemRemoved;
            if (handler != null)
                handler(this, e);
        }
    }
}
