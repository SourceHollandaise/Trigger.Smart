using Eto.Forms;
using Trigger.Datastore.Persistent;
using Trigger.WinForms.Actions;
using System;
using Trigger.WinForms.Layout;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Eto.Drawing;

namespace Trigger.WinForms.Actions
{
	public class ActionLinkedListController : ActionBaseController
	{
		public ActionLinkedListController(TemplateBase template, Type modelType, IStorable currentObject) : base(template, modelType, currentObject)
		{
			Category = "Links";
			TargetView = ActionControllerTargetView.DetailView;
			Visiblity = ActionVisibility.Menu;
		}

		public override IEnumerable<Command> Commands()
		{
			var properties = ModelType.GetProperties().Where(p => p.GetCustomAttributes(typeof(LinkedListAttribute), true).FirstOrDefault() != null).ToList();

			foreach (var property in properties)
			{
				var attribute = property.GetCustomAttributes(typeof(LinkedListAttribute), true).FirstOrDefault() as LinkedListAttribute;

				var linkCommand = new Command();
				linkCommand.ID = "Linked_" + attribute.LinkType.Name + "_Menu_Action";
				linkCommand.MenuText = "Linked " + attribute.LinkType.Name;
				linkCommand.ToolBarText = "Linked " + attribute.LinkType.Name;
				linkCommand.Image = ImageExtensions.GetImage("Folder_add32.png", 24);
				linkCommand.Tag = new Link { LinkType = attribute.LinkType, LinkProperty = property };

				linkCommand.Executed += (sender, e) =>
				{
					if (linkCommand.Tag is Link)
						LinkCommandExecute((Link)linkCommand.Tag);
				};

				yield return linkCommand;
			}
		}

		protected virtual void LinkCommandExecute(Link link)
		{
			var linkList = link.LinkProperty.GetValue(CurrentObject, null);
			if (linkList != null)
			{
				var listView = new ListViewTemplate(link.LinkType, null);
				listView.CurrentGrid.DataStore = new DataStoreCollection(linkList as IEnumerable<IStorable>);
				listView.Show();
			}
		}

		protected struct Link
		{
			public Type LinkType
			{
				get;
				set;
			}

			public PropertyInfo LinkProperty
			{
				get;
				set;
			}
		}
	}
}