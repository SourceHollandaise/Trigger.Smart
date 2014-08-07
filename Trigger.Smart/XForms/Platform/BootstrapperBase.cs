using System.Linq;
using XForms.Dependency;
using XForms.Commands;
using XForms.Store;
using XForms.Model;

namespace XForms.App
{
    public abstract class BootstrapperBase
    {
        IDependencyMap Map
        {
            get
            {
                return MapProvider.Instance;
            }
        }

        public abstract void InitalizeDataStore();

        public abstract void InitialiteSecurityProvider();

        public abstract void RegisterDependencies();

        public abstract void RegisterViewDescriptors();

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
