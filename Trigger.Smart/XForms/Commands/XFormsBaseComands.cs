using XForms.Dependency;

namespace XForms.Commands
{
    public class XFormsBaseComands
    {
        static IDependencyMap Map => MapProvider.Instance;

        public virtual void Register()
        {
            Map.RegisterType<ISaveObjectDetailViewCommand, SaveObjectDetailViewCommand>();
            Map.RegisterType<IDeleteObjectDetailViewCommand, DeleteObjectDetailViewCommand>();
            Map.RegisterType<ICloseDetailViewCommand, CloseDetailViewCommand>();
            Map.RegisterType<IOpenObjectListViewCommand, OpenObjectListViewCommand>();
            Map.RegisterType<IRefreshListViewCommand, RefreshListViewCommand>();
            Map.RegisterType<IRefreshDetailViewCommand, RefreshDetailViewCommand>();
            Map.RegisterType<IUpdateDocumentStoreListViewCommand, UpdateDocumentStoreListViewCommand>();
            Map.RegisterType<IAddFileDetailViewCommand, AddFileDetailViewCommand>();
            Map.RegisterType<ICreateObjectListViewCommand, CreateObjectListViewCommand>();
            Map.RegisterType<IApplicationExitCommand, ApplicationExitCommand>();
            Map.RegisterType<ILogonCommand, LogonCommand>();
            Map.RegisterType<ITagDetailViewCommand, TagDetailViewCommand>();
            Map.RegisterType<INavigateBackDetailViewCommand, NavigateBackDetailViewCommand>();
            Map.RegisterType<INavigateBackListViewCommand, NavigateBackListViewCommand>();
            Map.RegisterType<INavigateHomeDetailViewCommand, NavigateHomeDetailViewCommand>();
            Map.RegisterType<INavigateHomeListViewCommand, NavigateHomeListViewCommand>();
            Map.RegisterType<IFileViewerListViewCommand, FileViewerListViewCommand>();
        }
    }
}
