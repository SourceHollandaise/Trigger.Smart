using System;
using Trigger.XForms.Visuals;
using System.Collections.Generic;
using Trigger.XForms.Controllers;
using Trigger.XStorable.DataStore;
using Eto.Forms;
using Eto.Drawing;
using Trigger.BCL.ParaOffice;

namespace Trigger.App.ParaOffice
{
    public class ActionAktPersonenController : ActionBaseController
    {
        public Command ActionAddAktPerson { get; protected set; }

        public ActionAktPersonenController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
        {
            this.Category = "Edit";
            this.TargetModelType = typeof(Akt);
            this.TargetView = ActionControllerTargetView.DetailView;
        }

        public override IEnumerable<Command> Commands()
        {
            ActionAddAktPerson = new Command();
            ActionAddAktPerson.ID = "AddAktPerson_Tool_Action";
            ActionAddAktPerson.Image = ImageExtensions.GetImage("User32.png", 32);
            ActionAddAktPerson.MenuText = "Neue Aktperson";
            ActionAddAktPerson.ToolBarText = "Neue Aktperson";
          
            ActionAddAktPerson.Executed += (sender, e) =>
            {
                ActionAddAktPersonExecute();
            };

            yield return ActionAddAktPerson;
        }

        public virtual void ActionAddAktPersonExecute()
        {
            var akt = CurrentObject as Akt;

            var aktPerson = new AktPerson();
            aktPerson.Initialize();
            aktPerson.Akt = akt;

            var detailView = new DetailViewTemplate(typeof(AktPerson), aktPerson);
            detailView.Show();
        }
    }
}
