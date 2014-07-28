using Eto.Forms;
using Trigger.XStorable.DataStore;
using System.Collections.Generic;
using Eto.Drawing;
using System;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
{
    public class ActionSaveController : ActionBaseController
    {
        public Command SaveAction
        {
            get;
            protected set;
        }

        public ActionSaveController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
        {
            TargetView = ActionControllerTargetView.DetailView;
        }

        public override IEnumerable<Command> Commands()
        {
            SaveAction = new Command();
            SaveAction.ID = "Save_Tool_Action";
            SaveAction.Image = ImageExtensions.GetImage("Save32.png", 32);
            SaveAction.MenuText = "Save";
            SaveAction.ToolBarText = "Save";
            SaveAction.Shortcut = Keys.Control & Keys.S;
            SaveAction.Executed += (sender, e) =>
            {
                var result = MessageBox.Show("Save " + CurrentObject.GetRepresentation + "?", MessageBoxButtons.YesNo, MessageBoxType.Question, MessageBoxDefaultButton.No);
                if (result == DialogResult.Ok)
                    SaveActionExecute();
            };

            yield return SaveAction;
        }

        public virtual void SaveActionExecute()
        {
            if (CurrentObject != null)
                CurrentObject.Save();
        }
    }
}
