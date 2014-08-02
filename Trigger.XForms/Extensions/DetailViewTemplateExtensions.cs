using Trigger.XStorable.DataStore;
using Trigger.XForms.Visuals;
using Trigger.XStorable.Dependency;
using System;

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

        public static void ReloadObject(this IStorable target)
        {
            var store = DependencyMapProvider.Instance.ResolveType<IStore>();
            var dataObject = store.Load(target.GetType(), target.MappingId);
            if (dataObject != null)
            {
                var descriptorType = DetailViewDescriptorProvider.GetDescriptor(dataObject.GetType());
                if (descriptorType != null)
                {
                    var descriptor = Activator.CreateInstance(descriptorType) as IDetailViewDescriptor;
                    target.TryGetDetailView().Content = new DetailViewBuilder(descriptor, dataObject).GetContent();
                }
            }
        }
    }
}
