using Trigger.BCL.ParaOffice;
using XForms.Commands;
using XForms.Design;

namespace Trigger.BCL.ParaOffice
{
    public class AktPersonDetailViewCommand : IAktPersonDetailViewCommand
    {
        public void Execute(DetailViewArguments args)
        {
            var akt = args.CurrentObject as Akt;

            var aktPerson = new AktPerson();
            aktPerson.Initialize();
            aktPerson.Akt = akt;
            aktPerson.ShowDetailView();
        }

        public string ID
        {
            get
            {
                return "cmd_akt_person";
            }
        }

        public ButtonDisplayStyle DisplayStyle
        {
            get
            {
                return ButtonDisplayStyle.ImageAndText;
            }
        }

        public string Name
        {
            get
            {
                return "Aktperson";
            }
        }

        public string ImageName
        {
            get
            {
                return "user_add";
            }
        }

        public int Width
        {
            get
            {
                return 100;
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
    }
}
