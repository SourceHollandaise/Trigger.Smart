using System.Linq;
using XForms.Commands;
using XForms.Design;
using XForms.Model;
using XForms.Platform;
using XForms.Security;
using XForms.Store;
using Trigger.BCL.ParaOffice;

namespace Trigger.BCL.ParaOffice
{
    public class Bootstrapper : BootstrapperBase
    {
        public override void InitalizeDataStore()
        {
            var config = new StoreConfiguration();
            config.InitStore();

            Map.RegisterInstance<IStoreConfiguration>(config);
        }

        public override void InitialiteSecurityProvider()
        {
            var provider = new SecurityInfoProvider();

            Map.RegisterInstance<ISecurityInfoProvider>(provider);
        }

        public override void RegisterDependencies()
        {
            base.RegisterDependencies();

            Map.RegisterType<IFileDataService, DokumentFileDataService>();
            Map.RegisterType<IErvRueckverkehrService, ErvRueckverkehrService>();
        }

        public override void RegisterViewDescriptors()
        {
            Map.RegisterType<IMainViewDescriptor, ApplicationMainViewDescriptor>();
            Map.RegisterType<IMainViewTemplate, MainViewTemplate>();
            Map.RegisterType<IViewTemplateConfiguration, ViewTemplateDefaultConfiguration>();

            DetailViewDescriptorProvider.Declare<Akt, AktViewDescriptor>();
            DetailViewDescriptorProvider.Declare<AktArt, AktArtViewDescriptor>();
            DetailViewDescriptorProvider.Declare<AktPerson, AktPersonViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Dokument, DokumentViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Kontakt, KontaktViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Person, PersonViewDescriptor>();
            DetailViewDescriptorProvider.Declare<SB, SBViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Telefonat, TelefonatViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Termin, TerminViewDescriptor>();
            DetailViewDescriptorProvider.Declare<User, UserViewDescriptor>();
            DetailViewDescriptorProvider.Declare<ErvRueckverkehr, RueckverkehrViewDescriptor>();
            DetailViewDescriptorProvider.Declare<ErvRueckverkehrDokument, RueckverkehrDokumentViewDescriptor>();
            DetailViewDescriptorProvider.Declare<Leistung, LeistungViewDescriptor>();

            ListViewDescriptorProvider.Declare<Akt, AktListDescriptor>();
            ListViewDescriptorProvider.Declare<AktArt, AktArtListDescriptor>();
            ListViewDescriptorProvider.Declare<AktPerson, AktPersonListDescriptor>();
            ListViewDescriptorProvider.Declare<Dokument, DokumentListDescriptor>();
            ListViewDescriptorProvider.Declare<Kontakt, KontaktListDescriptor>();
            ListViewDescriptorProvider.Declare<Person, PersonListDescriptor>();
            ListViewDescriptorProvider.Declare<SB, SBListDescriptor>();
            ListViewDescriptorProvider.Declare<Telefonat, TelefonatListDescriptor>();
            ListViewDescriptorProvider.Declare<Termin, TerminListDescriptor>();
            ListViewDescriptorProvider.Declare<User, UserListDescriptor>();
            ListViewDescriptorProvider.Declare<ErvRueckverkehr, RueckverkehrListDescriptor>();
            ListViewDescriptorProvider.Declare<ErvRueckverkehrDokument, RueckverkehrDokumentListDescriptor>();
            ListViewDescriptorProvider.Declare<Leistung, LeistungListDescriptor>();
        }

        public override void RegisterCommands()
        {
            base.RegisterCommands();

            Map.RegisterType<IAktPersonDetailViewCommand, AktPersonDetailViewCommand>();
            Map.RegisterType<ICurrentUserDetailsCommand, CurrentSBDetailsCommand>();
            Map.RegisterType<IErvRueckverkehrAbrufenListViewCommand, ErvRueckverkehrAbrufenListViewCommand>();
        }

        public override void CreateDefaultUser()
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
