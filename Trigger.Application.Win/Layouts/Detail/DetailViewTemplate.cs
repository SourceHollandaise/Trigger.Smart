using Eto.Drawing;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using System.Linq;

namespace Trigger.WinForms.Layout
{
	public class DetailViewTemplate : TemplateBase
	{
		public DetailViewTemplate(Type modelType, IPersistent currentObject) : base(modelType, currentObject)
		{
			Content = new DetailViewGenerator(CurrentObject).GetLayout();

			Size = new Size(800, 600);
			Title = ModelType.Name + " - " + CurrentObject.GetDefaultPropertyValue();

			Controllers.Add(new ActionNewController(this, ModelType, CurrentObject));
			Controllers.Add(new ActionDeleteController(this, CurrentObject));
			Controllers.Add(new ActionSaveController(this, CurrentObject));
			Controllers.Add(new ActionRefreshDetailController(this, ModelType, CurrentObject));

			if (typeof(IFileData).IsAssignableFrom(ModelType))
				Controllers.Add(new ActionFileDataDetailController(this, ModelType, CurrentObject));
		}
	}
}
	

