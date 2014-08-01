using System;
using Trigger.XForms.Visuals;
using System.Collections.Generic;
using Trigger.XForms.Controllers;
using Trigger.XStorable.DataStore;
using Eto.Forms;
using Eto.Drawing;
using Trigger.BCL.ParaOffice;
using Trigger.XForms.Commands;

namespace Trigger.BCL.ParaOffice
{

    public class AktPersonCommand : IAktPersonCommand
    {
        public void Execute(DetailViewArguments args)
        {
            var akt = args.CurrentObject as Akt;

            var aktPerson = new AktPerson();
            aktPerson.Initialize();
            aktPerson.Akt = akt;

            var detailView = new DetailViewTemplate(typeof(AktPerson), aktPerson);
            detailView.Show();
        }

        public string ID
        {
            get
            {
                return "cmd_akt_person";
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

    }
}
