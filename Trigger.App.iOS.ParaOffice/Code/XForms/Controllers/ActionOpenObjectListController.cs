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
                var detailView = new DetailViewTemplate(ModelType, listForm.CurrentGrid.SelectedItem as IStorable);
                detailView.Show();
            }
        }
    }
}
