using XForms.Commands;

namespace Trigger.BCL.ParaOffice
{
    public class ErvRueckverkehrGenerator
    {
        public ErvRueckverkehr Get()
        {
            var rv = new ErvRueckverkehr();
            rv.AktenZeichen = "1 C 332/13 Z";
            rv.Art = "LA";
            rv.EmpfangDatum = System.DateTime.Now;
            rv.ErvCode = "R123456";
            rv.Gericht = "BG Korneuburg (110)";
            rv.HinterlegungDatum = rv.EmpfangDatum.AddHours(-8);
            rv.Partei1 = "Max Muster";
            rv.Partei2 = "Anton Anwalt";

            rv.Save();

            return rv;
        }
    }

    public class ErvRueckverkehrAbrufenListViewCommand : IErvRueckverkehrAbrufenListViewCommand
    {
        //TODO: Einlesen aus WebService und RV erstellen
        public void Execute(ListViewArguments listParameter)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
                return;

            var generator = new ErvRueckverkehrGenerator();

            for (int i = 0; i < 5; i++)
            {
                generator.Get();
            }

            XForms.Dependency.MapProvider.Instance.ResolveType<IRefreshListViewCommand>().Execute(listParameter);
           
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
