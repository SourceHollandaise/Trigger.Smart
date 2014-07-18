using System;
using Eto.Forms;
using Trigger.Datastore.Persistent;

namespace Trigger.CRM.Forms
{
	public class ModelDetailTemplate : Form
	{
		public ModelDetailTemplate(PersistentModelBase model)
		{
			Size = new Eto.Drawing.Size(800, 600);
			Title = model.GetType().Name + " - " + model.GetRepresentation();

			Content = new ModelDetailLayoutManager().GetLayout(model);
		}
	}
}
	

