using Eto.Forms;
using Trigger.XStorable.DataStore;
using System;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.XForms.Visuals;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Controllers
{

    public class ActionFileDataDetailController : ActionBaseController
    {
        public Command AddFileAction
        {
            get;
            protected set;
        }

        public Command OpenFileAction
        {
            get;
            protected set;
        }

        public ActionFileDataDetailController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
        {
            Category = "Store";
            TargetView = ActionControllerTargetView.DetailView;
            TargetModelType = typeof(IFileData);
        }

        public override IEnumerable<Command> Commands()
        {
            AddFileAction = new Command();
            AddFileAction.ID = "AddFile_Tool_Action";
            AddFileAction.Image = ImageExtensions.GetImage("Paperclip32.png", 32);
            AddFileAction.MenuText = "Add file";
            AddFileAction.ToolBarText = "Add file";
            AddFileAction.Executed += (sender, e) =>
            {
                AddFileActionExecute();
            };

            yield return AddFileAction;

            OpenFileAction = new Command();
            OpenFileAction.ID = "OpenFile_Tool_Action";
            OpenFileAction.Image = ImageExtensions.GetImage("File_info32.png", 32);
            OpenFileAction.MenuText = "Open file";
            OpenFileAction.ToolBarText = "Open file";
            OpenFileAction.Executed += (sender, e) =>
            {
                OpenFileActionExecute();
            };

            yield return OpenFileAction;
        }

        public virtual void AddFileActionExecute()
        {
            var service = DependencyMapProvider.Instance.ResolveType<IFileDataService>();
            if (CurrentObject != null)
            {
                var fileDialog = new OpenFileDialog();
                fileDialog.MultiSelect = false;
                if (fileDialog.ShowDialog(null) == DialogResult.Ok)
                {
                    service.AddFile(CurrentObject as IFileData, fileDialog.FileName);
                    fileDialog.Dispose();
                }
            }
        }

        public virtual void OpenFileActionExecute()
        {
            var service = DependencyMapProvider.Instance.ResolveType<IFileDataService>();
            if (CurrentObject != null)
            {
                service.OpenFile(CurrentObject as IFileData);
            }
        }
    }
}
