using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using System.Collections.Generic;
using Eto.Drawing;

namespace Trigger.WinForms.Actions
{

	public class ModelFileDataDetailController : ActionBaseController
	{
		public ButtonToolItem AddFileAction
		{
			get;
			protected set;
		}

		public ModelFileDataDetailController(Form template, Type modelType, IPersistentId model) : base(template, model)
		{
			this.ModelType = modelType;
		}

		public override IEnumerable<ToolItem> ActionItems()
		{
			AddFileAction = new ButtonToolItem();
			AddFileAction.ID = "AddFile_Tool_Action";
			AddFileAction.Image = Bitmap.FromResource("Paperclip32.png");
			AddFileAction.Text = "Refresh";

			AddFileAction.Click += (sender, e) =>
			{
				AddFileExecute();
			};

			yield return AddFileAction;
		}

		protected virtual void AddFileExecute()
		{
			var service = Dependency.DependencyMapProvider.Instance.ResolveType<IFileDataService>();
			if (CurrentObject != null)
			{
				using (var fileDialog = new OpenFileDialog())
				{
					fileDialog.MultiSelect = false;
					if (fileDialog.ShowDialog(null) == DialogResult.Ok)
						service.AddFile(CurrentObject as IFileData, fileDialog.FileName);
				}
			}
		}
	}
}
