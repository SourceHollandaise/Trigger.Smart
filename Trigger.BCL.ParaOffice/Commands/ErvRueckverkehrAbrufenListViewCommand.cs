using System.Collections.Generic;
using System.IO;
using XForms.Commands;
using XForms.Dependency;
using XForms.Store;

namespace Trigger.BCL.ParaOffice
{
    public class ErvRueckverkehrGenerator
    {
        public void Create(int items)
        {
            for (int i = 0; i < items; i++)
            {
                var rv = new ErvRueckverkehr();
                rv.AktenZeichen = string.Format("1 C 332/{0} Z", i + 10);
                rv.Art = "LA";
                rv.EmpfangDatum = System.DateTime.Now;
                rv.ErvCode = "R123456";
                rv.Gericht = "BG Korneuburg (110)";
                rv.HinterlegungDatum = rv.EmpfangDatum.AddHours(-8);
                rv.Partei1 = "Max Muster";
                rv.Partei2 = "Anton Anwalt";

                rv.Save();

                var rvDok = new ErvRueckverkehrDokument();

                var file = "Running your application.pdf";
                var sourcePath = Path.Combine(MapProvider.Instance.ResolveInstance<IStoreConfiguration>().DocumentStoreLocation, file);

                MapProvider.Instance.ResolveType<IFileDataService>().AddFile(rvDok, sourcePath);

                rvDok.Subject = string.Format("Ladung Test - Nr.: {0}", i);

                rvDok.Rueckverkehr = rv;

                rvDok.Save();
            }
        }
    }

    public class ErvRueckverkehrAbrufenListViewCommand : IErvRueckverkehrAbrufenListViewCommand
    {
        void CreateRueckverkehrMocks(ListViewArguments listParameter)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
                return;

            new ErvRueckverkehrGenerator().Create(10);

            MapProvider.Instance.ResolveType<IRefreshListViewCommand>().Execute(listParameter);
        }

        public void Execute(ListViewArguments listParameter)
        {
            //TODO: Einlesen aus WebService und RV erstellen

            CreateRueckverkehrMocks(listParameter);
        }

        public string ID
        {
            get
            {
                return "cmd_erv_rueckverkehr_abrufen";
            }
        }

        public string Name
        {
            get
            {
                return "Rückverkehr abrufen";
            }
        }

        public string ImageName
        {
            get
            {
                return "cloud_computing_refresh";
            }
        }

        public int Width
        {
            get
            {
                return 160;
            }
        }

        public bool AllowExecute
        {
            get
            {
                return true;
            }
        }

        public bool Visible
        {
            get
            {
                return true;
            }
        }

        public ButtonDisplayStyle DisplayStyle
        {
            get
            {
                return ButtonDisplayStyle.Text;
            }
        }
    }
}
