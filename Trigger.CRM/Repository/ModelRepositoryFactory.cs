using Trigger.Dependency;
using Trigger.CRM.Model;

namespace Trigger.CRM.Repository
{
    public static class ModelRepositoryFactory
    {
        public static IModelRepository<T> Create<T>() where T: ModelBase
        {
            var map = DependencyMapProvider.Instance;

            map.RegisterInstance<IModelRepository<T>>(new ModelRespository<T>());

            return map.ResolveInstance<IModelRepository<T>>();
        }
    }
}
