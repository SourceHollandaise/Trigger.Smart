using System;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using Trigger.Datastore.Persistent;

namespace Trigger.WinForms.Layout
{
	public class ListViewTemplate : TemplateBase
	{
		public GridView CurrentGrid
		{
			get;
			protected set;
		}

		public Func<object, bool> GridFilter
		{
			get;
			set;
		}

		public ListViewTemplate(Type modelType, IPersistent currentObject) : base(modelType, currentObject)
		{
			if (CurrentGrid == null)
				CurrentGrid = new ListViewGenerator(ModelType).GetContent();

			Content = CurrentGrid;

			Size = new Size(1024, 600);
			Title = ModelType.Name + "-List - Items: " + CurrentGrid.DataStore.AsEnumerable().Count();

		}
	}
}
