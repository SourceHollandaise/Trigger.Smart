using XForms.Commands;
using XForms.Dependency;
using System;

namespace Trigger.BCL.ParaOffice
{

    public class ErvRueckverkehrAbrufenListViewCommand : IErvRueckverkehrAbrufenListViewCommand
    {
        public void Execute(ListViewArguments listParameter)
        {
            //TODO: Einlesen aus WebService und RV erstellen

            var service = MapProvider.Instance.ResolveType<IErvRueckverkehrService>();

            var item = service.Get(ApplicationModelQuery.CurrentSB.ErvCode, DateTime.Today, DateTime.Today);

            MapProvider.Instance.ResolveType<IRefreshListViewCommand>().Execute(listParameter);
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
