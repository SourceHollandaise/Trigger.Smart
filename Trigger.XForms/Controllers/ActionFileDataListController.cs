using Eto.Forms;
using Trigger.XStorable.DataStore;

using System;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.XForms.Visuals;
using Trigger.XStorable.Dependency;

namespace Trigger.XForms.Controllers
{
    public class ActionFileDataListController : ActionBaseController
    {
        public Command LoadFilesAction
        {
            get;
            protected set;
        }

        public ActionFileDataListController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
        {
            Category = "Store";
            TargetView = ActionControllerTargetView.ListView;
            TargetModelType = typeof(IFileData);
        }

        public override IEnumerable<Command> Commands()
        {
            LoadFilesAction = new Command();
            LoadFilesAction.ID = "LoadFiles_Tool_Action";
            LoadFilesAction.Image = ImageExtensions.GetImage("File_add32.png", 32);
            LoadFilesAction.MenuText = "Load files";
            LoadFilesAction.ToolBarText = "Load files";
            LoadFilesAction.Executed += (sender, e) =>
            {
                LoadFilesActionExecute();
            };

            yield return LoadFilesAction;
        }

        public virtual void LoadFilesActionExecute()
        {
            var service = DependencyMapProvider.Instance.ResolveType<IFileDataService>();
            service.LoadFromStore();

            var listForm = Template as ListViewTemplate;
            if (listForm != null)
                listForm.ReloadList();	

//            var startupView = Template as MainViewTemplate;
//            if (startupView != null)
//                startupView.CurrentGridView.ReloadList(startupView.CurrentActiveType);
        }
    }
}
