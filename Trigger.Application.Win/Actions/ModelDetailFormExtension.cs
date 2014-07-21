using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;
using System.Linq;

namespace Trigger.WinForms.Actions
{
	
	public static class ModelDetailFormExtension
	{
		public static void ReloadObject(this DetailViewTemplate detailForm)
		{
			if (detailForm != null)
			{
				var store = Dependency.DependencyMapProvider.Instance.ResolveType<IStore>();
				var dataObject = store.Load(detailForm.ModelType, detailForm.CurrentObject.MappingId);
				if (dataObject != null)
				{
					var layout = new DetailViewGenerator(dataObject).GetLayout();
					detailForm.Content = layout;
				}
			}
		}
	}
}
