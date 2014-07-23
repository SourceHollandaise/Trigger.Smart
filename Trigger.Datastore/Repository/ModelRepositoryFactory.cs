using Trigger.Dependency;
using Trigger.Datastore.Persistent;

namespace Trigger.Datastore.Repository
{
    public static class ModelRepositoryFactory
    {
        public static IModelRepository<T> Create<T>() where T: IStorable
        {
            var map = DependencyMapProvider.Instance;

            map.RegisterInstance<IModelRepository<T>>(new ModelRespository<T>());

            return map.ResolveInstance<IModelRepository<T>>();
        }
    }
}
