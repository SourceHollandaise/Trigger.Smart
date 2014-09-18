using System.Collections.Generic;
using System.IO;
using XForms.Commands;
using XForms.Dependency;
using XForms.Store;

namespace Trigger.BCL.ParaOffice
{

    public class ErvRueckverkehrAbrufenListViewCommand : IErvRueckverkehrAbrufenListViewCommand
    {
        void CreateRueckverkehrMocks(ListViewArguments listParameter)
        {
            if (!System.Diagnostics.Debugger.IsAttached)
                return;

            new ErvRueckverkehrService().Load(10);

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
