using System;
using Eto.Forms;
using System.Collections.Generic;
using Eto.Drawing;
using Trigger.Datastore.Persistent;

namespace Trigger.WinForms.Layout
{
	public static class TemplateManager
	{
		static Dictionary<IPersistentId, DetailViewTemplate> detailTemplates = new Dictionary<IPersistentId, DetailViewTemplate>();

		static Dictionary<Type, ListViewTemplate> listTemplates = new Dictionary<Type, ListViewTemplate>();

		public static void RemoveDetailTemplate(IPersistentId targetObject)
		{
			if (detailTemplates.ContainsKey(targetObject))
				detailTemplates.Remove(targetObject);
		}

		public static void RemoveListTemplate(Type type)
		{
			if (listTemplates.ContainsKey(type))
				listTemplates.Remove(type);
		}

		public static DetailViewTemplate GetDetailTemplate(IPersistentId targetObject)
		{
			if (detailTemplates.ContainsKey(targetObject))
				return detailTemplates[targetObject];

			var template = new DetailViewTemplate(targetObject.GetType(), targetObject);
			detailTemplates.Add(targetObject, template);

			return template;
		}

		public static ListViewTemplate GetListTemplate(Type type)
		{
			if (listTemplates.ContainsKey(type))
				return listTemplates[type];

			var template = new ListViewTemplate(type, null);
			listTemplates.Add(type, template);

			return template;
		}
	}
	
}
