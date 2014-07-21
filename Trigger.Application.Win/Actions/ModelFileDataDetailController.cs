using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.WinForms.Layout;

namespace Trigger.WinForms.Actions
{

	public class ModelFileDataDetailController : ActionBaseController
	{
		public ButtonToolItem AddFileAction
		{
			get;
			protected set;
		}

		public ModelFileDataDetailController(TemplateBase template, Type modelType, IPersistentId model) : base(template, model)
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
				AddFileActionExecute();
			};

			yield return AddFileAction;
		}

		public virtual void AddFileActionExecute()
		{
			var service = Dependency.DependencyMapProvider.Instance.ResolveType<IFileDataService>();
			if (CurrentObject != null)
			{
				var fileDialog = new OpenFileDialog();
				fileDialog.MultiSelect = false;
				if (fileDialog.ShowDialog(null) == DialogResult.Ok)
				{
					service.AddFile(CurrentObject as IFileData, fileDialog.FileName);
					fileDialog.Dispose();
				}
			}
		}
	}
}
