using System.Linq;
using XForms.Dependency;
using XForms.Commands;
using XForms.Store;
using XForms.Model;
using XForms.Design;
using XForms.Security;

namespace XForms.Platform
{
    public abstract class BootstrapperBase
    {
        protected IDependencyMap Map
        {
            get
            {
                return MapProvider.Instance;
            }
        }

        public abstract void InitalizeDataStore();

        public abstract void InitialiteSecurityProvider();

        public abstract void RegisterViewDescriptors();

        public virtual void RegisterDependencies()
        {
            Map.RegisterType<IViewTemplateConfiguration, ViewTemplateConfiguration>();
            Map.RegisterType<IAuthenticate, DataStoreAuthenticate>();
            Map.RegisterType<IdGenerator, GuidIdGenerator>();
            Map.RegisterType<IStore, FileDataStore>();
        }


        public virtual void RegisterCommands()
        {
            new XFormsBaseComands().Register(); 
        }

        public virtual void CreateDefaultUser()
        {
            var user = Map.ResolveType<IStore>().LoadAll<User>().FirstOrDefault(p => p.UserName == "Admin");

            if (user == null)
                user = new User();

            user.AllowAdministration = true;
            user.Password = "admin";
            user.Role = ApplicationUserRole.Administrator;
            user.UserName = "Admin";
            user.UserSex = Sex.Female;
            user.Save();
        }
    }
}
