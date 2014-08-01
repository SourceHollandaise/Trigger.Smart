using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using System;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Commands
{
    public class RefreshDetailViewCommand : IRefreshDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            var store = DependencyMapProvider.Instance.ResolveType<IStore>();
            var dataObject = store.Load(args.CurrentObject.GetType(), args.CurrentObject.MappingId);
            if (dataObject != null)
            {
                var descriptorType = DetailViewDescriptorProvider.GetDescriptor(dataObject.GetType());
                if (descriptorType != null)
                {
                    var descriptor = Activator.CreateInstance(descriptorType) as IDetailViewDescriptor;
                    args.Template.Content = new DetailViewBuilder(descriptor, dataObject).GetContent();
                }
            }
        }

        public string ID
        {
            get
            {
                return "cmd_refresh";
            }
        }

        public string Name
        {
            get
            {
                return "Refresh";
            }
        }

        public string ImageName
        {
            get
            {
                return "Refresh16";
            }
        }
    }
}
