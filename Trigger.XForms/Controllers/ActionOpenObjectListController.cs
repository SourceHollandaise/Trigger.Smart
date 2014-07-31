using Trigger.XStorable.DataStore;
using System;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
{
    public class ActionOpenObjectListController : ActionOpenObjectBaseController
    {
        public ActionOpenObjectListController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
        {
            this.ModelType = modelType;
            TargetView = ActionControllerTargetView.ListView;
        }

        public override void OpenObjectActionExecute()
        {
            var listForm = Template as ListViewTemplate;

            if (listForm != null)
            {
                WindowManager.ShowDetailView(listForm.CurrentGrid.SelectedItem as IStorable);
            }

            var startupView = Template as StartupView;
            if (startupView != null)
            {
                WindowManager.ShowDetailView(startupView.CurrentGridView.SelectedItem as IStorable);

            }
        }
    }
}
