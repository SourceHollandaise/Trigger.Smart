using System.Linq;
using Trigger.BCL.Common.Model;
using Trigger.BCL.Common.Security;
using Trigger.BCL.Common.Services;
using Trigger.BCL.ParaOffice;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Visuals;
using Trigger.XForms;
using Trigger.BCL.Common.Datastore;
using Trigger.XForms.Commands;

namespace Trigger.App.ParaOffice
{
    public class Bootstrapper
    {
        IDependencyMap Map
        {
            get
            {
                return DependencyMapProvider.Instance;
            }
        }

        public virtual void InitalizeDataStore()
        {
            var config = new StoreConfiguration();
            config.InitStore();

            Map.RegisterInstance<IStoreConfiguration>(config);
        }

        public virtual void RegisterDependencies()
        {
            Map.RegisterType<IViewTemplateConfiguration, ViewTemplateConfiguration>();
            Map.RegisterType<IAuthenticate, DataStoreAuthenticate>();
            Map.RegisterType<IdGenerator, GuidIdGenerator>();
            Map.RegisterType<IStore, FileDataStore>();
            Map.RegisterType<IFileDataService, DokumentFileDataService>();

            Map.RegisterType<IMainViewDescriptor, ApplicationMainViewDescriptor>();

            RegisterCommands();

            RegisterViewDescriptors();
        }

        void RegisterCommands()
        {
            Map.RegisterType<ISaveObjectCommand, SaveObjectCommand>();
            Map.RegisterType<IDeleteObjectCommand, DeleteObjectCommand>();
            Map.RegisterType<ICloseWindowCommand, CloseWindowCommand>();
            Map.RegisterType<IOpenObjectListViewCommand, OpenObjectListViewCommand>();
            Map.RegisterType<IRefreshListViewCommand, RefreshListViewCommand>();
            Map.RegisterType<IRefreshDetailViewCommand, RefreshDetailViewCommand>();
            Map.RegisterType<IUpdateDocumentStoreListViewCommand, UpdateDocumentStoreListViewCommand>();
            Map.RegisterType<IAddFileCommand, AddFileCommand>();
            Map.RegisterType<ICreateObjectListViewCommand, CreateObjectListViewCommand>();
            Map.RegisterType<IApplicationExitCommand, ApplicationExitCommand>();
            Map.RegisterType<ILogOffCommand, LogOffCommand>();
            Map.RegisterType<IAktPersonCommand, AktPersonCommand>();
        }

        static void RegisterViewDescriptors()
        {
            DetailViewDescriptorProvider.Declare<Akt, AktViewDescriptor>();
            DetailViewDescriptorProvider.Declare<AktArt, AktArtViewDescriptor>();
            DetailViewDescriptorProvider.Declare<AktPerson, AktPersonViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Dokument, DokumentViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Kontakt, KontaktViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Person, PersonViewDescriptor>();
            DetailViewDescriptorProvider.Declare<SB, SBViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Termin, TerminViewDescriptor>();
            DetailViewDescriptorProvider.Declare<User, UserViewDescriptor>();
            ListViewDescriptorProvider.Declare<Akt, AktListDescriptor>();
            ListViewDescriptorProvider.Declare<AktArt, AktArtListDescriptor>();
            ListViewDescriptorProvider.Declare<AktPerson, AktPersonListDescriptor>();
            ListViewDescriptorProvider.Declare<Dokument, DokumentListDescriptor>();
            ListViewDescriptorProvider.Declare<Kontakt, KontaktListDescriptor>();
            ListViewDescriptorProvider.Declare<Person, PersonListDescriptor>();
            ListViewDescriptorProvider.Declare<SB, SBListDescriptor>();
            ListViewDescriptorProvider.Declare<Termin, TerminListDescriptor>();
            ListViewDescriptorProvider.Declare<User, UserListDescriptor>();
        }

        public virtual void CreateInitialObjects()
        {
            var user = Map.ResolveType<IStore>().LoadAll<User>().FirstOrDefault(p => p.UserName == "A");

            if (user == null)
            {
                user = new User
                {
                    UserName = "A",
                    Password = "a"
                };
			
                user.Save();
            }

            var sb = Map.ResolveType<IStore>().LoadAll<SB>().FirstOrDefault(p => p.ID == "A");

            if (sb == null)
            {
                sb = new SB
                {
                    ID = "A",
                    User = user
                };

                sb.Save();
            }
        }
    }
}
