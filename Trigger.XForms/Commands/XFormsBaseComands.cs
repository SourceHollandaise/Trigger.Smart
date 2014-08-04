using Eto.Forms;
using Trigger.XForms.Visuals;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Commands
{

    public class XFormsBaseComands
    {
        IDependencyMap Map
        {
            get
            {
                return DependencyMapProvider.Instance;
            }
        }

        public void Register()
        {
            Map.RegisterType<ISaveObjectDetailViewCommand, SaveObjectDetailViewCommand>();
            Map.RegisterType<IDeleteObjectCommand, DeleteObjectCommand>();
            Map.RegisterType<ICloseDetailViewCommand, CloseDetailViewCommand>();
            Map.RegisterType<IOpenObjectListViewCommand, OpenObjectListViewCommand>();
            Map.RegisterType<IRefreshListViewCommand, RefreshListViewCommand>();
            Map.RegisterType<IRefreshDetailViewCommand, RefreshDetailViewCommand>();
            Map.RegisterType<IUpdateDocumentStoreListViewCommand, UpdateDocumentStoreListViewCommand>();
            Map.RegisterType<IAddFileDetailViewCommand, AddFileDetailViewCommand>();
            Map.RegisterType<ICreateObjectListViewCommand, CreateObjectListViewCommand>();
            Map.RegisterType<IApplicationExitCommand, ApplicationExitCommand>();
            Map.RegisterType<ILogOffCommand, LogOffCommand>();
            Map.RegisterType<ITagDetailViewCommand, TagDetailViewCommand>();
            Map.RegisterType<ISearchListViewCommand, SearchListViewCommand>();
            Map.RegisterType<ICurrentUserListViewCommand, CurrentUserListViewCommand>();
        }
    }
}
