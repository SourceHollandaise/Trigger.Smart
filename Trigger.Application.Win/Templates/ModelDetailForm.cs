using Eto.Drawing;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;

namespace Trigger.WinForms.Layout
{
	public class ModelDetailForm : TemplateBase
	{
		public ModelDetailForm(Type modelType, IPersistentId currentObject) : base(modelType, currentObject)
		{
			Content = new ModelDetailLayoutManager(CurrentObject).GetLayout();

			Size = new Size(800, 600);
			Title = ModelType.Name + " - " + CurrentObject.GetRepresentation();

			Controllers.Add(new ModelNewObjectActionController(this, ModelType, CurrentObject));
			Controllers.Add(new ModelModificationController(this, CurrentObject));
			Controllers.Add(new ModelRefreshDetailController(this, ModelType, CurrentObject));

			if (typeof(IFileData).IsAssignableFrom(ModelType))
				Controllers.Add(new ModelFileDataDetailController(this, ModelType, CurrentObject));
		}
	}
}
	

