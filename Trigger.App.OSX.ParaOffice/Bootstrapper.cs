using System;
using System.Linq;
using Trigger.XForms.Controllers;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.BCL.Common.Model;
using Trigger.BCL.Common.Services;
using Trigger.BCL.Common.Security;
using Trigger.BCL.ParaOffice;

namespace Trigger.App.OSX.ParaOffice
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
            StoreConfigurator.InitStore();
        }

        public virtual void RegisterDependencies()
        {
            Map.RegisterType<IAuthenticate, DataStoreAuthenticate>();
            Map.RegisterType<IdGenerator, GuidIdGenerator>();
            Map.RegisterType<IStore, FileStore>();
            Map.RegisterType<IFileDataService, ParaOfficeDocumentDataService>();
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

        public virtual void RegisterDeclaredTypes()
        {
            ModelTypesDeclaration.DeclareModelTypes(DeclaredTypes());
            ActionControllerDeclaration.DeclareControllerTypes(DeclaredControllers());
        }

        protected virtual Type[] DeclaredTypes()
        {
            return new[]
            {
                typeof(Akt),
                typeof(AktArt),
                typeof(Dokument),
                typeof(Kontakt),
                typeof(Person),
                typeof(SB),
                typeof(User)
            };
        }

        protected virtual Type[] DeclaredControllers()
        {
            return new []
            {
                typeof(ActionActiveWindowsController),
                typeof(ActionApplicationExitController),
                typeof(ActionCloseController),
                typeof(ActionDeleteController),
                typeof(ActionFileDataDetailController),
                typeof(ActionFileDataListController),
                typeof(ActionLinkedListController),
                typeof(ActionNewController),
                typeof(ActionOpenObjectListController),
                typeof(ActionRefreshDetailController),
                typeof(ActionRefreshListController),
                typeof(ActionSaveController),

            };
        }
    }
}
