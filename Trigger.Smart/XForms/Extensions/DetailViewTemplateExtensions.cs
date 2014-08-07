using System;
using XForms.Dependency;
using XForms.Store;
using XForms.Design;

namespace XForms.Design
{
    public static class DetailViewTemplateExtensions
    {
        public static void ReloadObject(this IStorable target)
        {
            var store = MapProvider.Instance.ResolveType<IStore>();
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
