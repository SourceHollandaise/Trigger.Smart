using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using System;
using Trigger.XForms.Visuals;
using Eto.Forms;

namespace Trigger.XForms.Controllers
{
    public class ActionRefreshListController : ActionRefreshBaseController
    {
        public ActionRefreshListController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
        {
            this.ModelType = modelType;
            TargetView = ActionControllerTargetView.ListView;
        }

        public override void RefreshActionExecute()
        {
//            var startupView = Template as MainViewTemplate;
//            if (startupView != null)
//                startupView.CurrentGridView.ReloadList(startupView.CurrentActiveType);

            var listForm = Template as ListViewTemplate;
            if (listForm != null)
                listForm.ReloadList();
        }
    }
}
