using Trigger.XStorable.DataStore;
using Trigger.XForms.Visuals;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms
{
    public static class DetailViewTemplateExtensions
    {
        public static void ReloadObject(this DetailViewTemplate detailForm)
        {
            if (detailForm != null)
            {
                var store = DependencyMapProvider.Instance.ResolveType<IStore>();
                var dataObject = store.Load(detailForm.ModelType, detailForm.CurrentObject.MappingId);
                if (dataObject != null)
                {
                    detailForm.SetContent(dataObject);
                }
            }
        }
    }
}
