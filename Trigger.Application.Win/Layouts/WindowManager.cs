using System;
using System.Collections.Generic;
using Trigger.Datastore.Persistent;

namespace Trigger.WinForms.Layout
{
	public static class WindowManager
	{
		static readonly Dictionary<string, DetailViewTemplate> detailTemplates = new Dictionary<string, DetailViewTemplate>();

		static readonly Dictionary<Type, ListViewTemplate> listTemplates = new Dictionary<Type, ListViewTemplate>();

		public static void ShowDetailView(IPersistent targetObject)
		{
			if (targetObject == null)
				return;

			var detailTemplate = GetDetailView(targetObject);
			if (detailTemplate.Visible)
				detailTemplate.BringToFront();
			else
				detailTemplate.Show();
		}

		public static void ShowListView(Type targetType)
		{
			var listTemplate = WindowManager.GetListView(targetType);
			if (listTemplate.Visible)
				listTemplate.BringToFront();
			else
				listTemplate.Show();
		}

		public static IEnumerable<TemplateBase> ActiveViews
		{
			get
			{
				foreach (var item in detailTemplates)
					yield return item.Value;

				foreach (var item in listTemplates)
					yield return item.Value;
			}
		}

		public static void RemoveDetailView(IPersistent targetObject)
		{
			if (targetObject == null)
				return;

			if (targetObject.MappingId == null)
				return;

			if (detailTemplates.ContainsKey((string)targetObject.MappingId))
				detailTemplates.Remove((string)targetObject.MappingId);
		}

		public static void RemoveListView(Type targetType)
		{
			if (listTemplates.ContainsKey(targetType))
				listTemplates.Remove(targetType);
		}

		public static void AddDetailView(IPersistent targetObject, DetailViewTemplate template)
		{
			if (targetObject == null)
				return;

			if (targetObject.MappingId == null)
				return;

			if (!detailTemplates.ContainsKey((string)targetObject.MappingId))
				detailTemplates.Add((string)targetObject.MappingId, template);
		}

		public static void AddListView(Type targetType, ListViewTemplate template)
		{
			if (!listTemplates.ContainsKey(targetType))
				listTemplates.Add(targetType, template);
		}

		public static DetailViewTemplate GetDetailView(IPersistent targetObject)
		{
			if (targetObject == null)
				return null;

			if (detailTemplates.ContainsKey((string)targetObject.MappingId))
				return detailTemplates[(string)targetObject.MappingId];

			var template = new DetailViewTemplate(targetObject.GetType(), targetObject);
			detailTemplates.Add((string)targetObject.MappingId, template);

			return template;
		}

		public static ListViewTemplate GetListView(Type targetType)
		{
			if (listTemplates.ContainsKey(targetType))
				return listTemplates[targetType];

			var template = new ListViewTemplate(targetType, null);
			listTemplates.Add(targetType, template);

			return template;
		}
	}
	
}
