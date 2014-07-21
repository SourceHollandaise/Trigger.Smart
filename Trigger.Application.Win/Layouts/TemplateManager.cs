using System;
using System.Collections.Generic;
using Trigger.Datastore.Persistent;

namespace Trigger.WinForms.Layout
{
	public static class TemplateManager
	{
		static readonly Dictionary<IPersistent, DetailViewTemplate> detailTemplates = new Dictionary<IPersistent, DetailViewTemplate>();

		static readonly Dictionary<Type, ListViewTemplate> listTemplates = new Dictionary<Type, ListViewTemplate>();

		public static void ShowDetailTemplate(IPersistent targetObject)
		{
			if (targetObject == null)
				return;

			var detailTemplate = GetDetailTemplate(targetObject);
			if (detailTemplate.Visible)
				detailTemplate.BringToFront();
			else
				detailTemplate.Show();
		}

		public static void ShowListTemplate(Type targetType)
		{
			var listTemplate = TemplateManager.GetListTemplate(targetType);
			if (listTemplate.Visible)
				listTemplate.BringToFront();
			else
				listTemplate.Show();
		}

		public static IEnumerable<TemplateBase> ActiveTemplates
		{
			get
			{
				foreach (var item in detailTemplates)
					yield return item.Value;

				foreach (var item in listTemplates)
					yield return item.Value;
			}
		}

		public static void RemoveDetailTemplate(IPersistent targetObject)
		{
			if (targetObject == null)
				return;

			if (detailTemplates.ContainsKey(targetObject))
				detailTemplates.Remove(targetObject);
		}

		public static void RemoveListTemplate(Type targetType)
		{
			if (listTemplates.ContainsKey(targetType))
				listTemplates.Remove(targetType);
		}

		public static DetailViewTemplate GetDetailTemplate(IPersistent targetObject)
		{
			if (targetObject == null)
				return null;

			if (detailTemplates.ContainsKey(targetObject))
				return detailTemplates[targetObject];

			var template = new DetailViewTemplate(targetObject.GetType(), targetObject);
			detailTemplates.Add(targetObject, template);

			return template;
		}

		public static ListViewTemplate GetListTemplate(Type targetType)
		{
			if (listTemplates.ContainsKey(targetType))
				return listTemplates[targetType];

			var template = new ListViewTemplate(targetType, null);
			listTemplates.Add(targetType, template);

			return template;
		}
	}
	
}
