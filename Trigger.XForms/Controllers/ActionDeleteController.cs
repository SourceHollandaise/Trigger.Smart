using System;
using System.Collections.Generic;
using Eto.Drawing;
using Eto.Forms;
using Trigger.XStorable.DataStore;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
{
    public class ActionDeleteController : ActionBaseController
    {
        public Command DeleteAction
        {
            get;
            protected set;
        }

        public ActionDeleteController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
        {
            Category = "Edit";
            TargetView = ActionControllerTargetView.DetailView;
        }

        public override IEnumerable<Command> Commands()
        {
            DeleteAction = new Command();
            DeleteAction.ID = "Delete_Tool_Action";
            DeleteAction.Image = ImageExtensions.GetImage("Delete32.png", 32);
            DeleteAction.MenuText = "Delete";
            DeleteAction.ToolBarText = "Delete";
            DeleteAction.Shortcut = Keys.Control & Keys.D;
            DeleteAction.Executed += (sender, e) =>
            {
                var result = MessageBox.Show("Delete " + CurrentObject.GetRepresentation + "?", MessageBoxButtons.OKCancel, MessageBoxType.Warning, MessageBoxDefaultButton.Cancel);
                if (result == DialogResult.Ok)
                    DeleteActionExecute();
            };

            yield return DeleteAction;
        }

        public virtual void DeleteActionExecute()
        {
            if (CurrentObject != null)
                CurrentObject.Delete();

            var listForm = WindowManager.GetListView(ModelType);
            if (listForm != null)
                listForm.ReloadList();
        }
    }
}
