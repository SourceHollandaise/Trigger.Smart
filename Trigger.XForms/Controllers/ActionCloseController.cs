using System;
using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XForms.Controllers;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
{


    public class ActionCloseController : ActionBaseController
    {
        public Command CloseAction
        {
            get;
            protected set;
        }

        public ActionCloseController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
        {
            Category = "File";
            TargetView = ActionControllerTargetView.DetailView;
        }

        public override IEnumerable<Command> Commands()
        {
            CloseAction = new Command();
            CloseAction.ID = "Close_Tool_Action";
            CloseAction.Image = ImageExtensions.GetImage("Close32.png", 32);
            CloseAction.MenuText = "Close";
            CloseAction.ToolBarText = "Close";
            CloseAction.Executed += (sender, e) =>
            {
                CloseActionExecute();
            };

            yield return CloseAction;
        }

        protected virtual void CloseActionExecute()
        {
            Template.Close();
        }
    }
}
