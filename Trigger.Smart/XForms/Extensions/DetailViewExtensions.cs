using System.Collections.Generic;
using XForms.Store;

namespace XForms.Design
{
    public static class DetailViewExtensions
    {
        static readonly Dictionary<IStorable, DetailViewTemplate> detailTemplates = new Dictionary<IStorable, DetailViewTemplate>();

        public static void ShowDetailView(this IStorable targetObject)
        {
            if (targetObject != null)
            {
                if (detailTemplates.ContainsKey(targetObject))
                    detailTemplates[targetObject].Show();
                else
                {
                    var template = new DetailViewTemplate(targetObject.GetType(), targetObject);
                    if (!detailTemplates.ContainsKey(targetObject))
                        detailTemplates.Add(targetObject, template);

                    template.Show();
                }
            }
        }

        public static void TryCloseDetailView(this IStorable targetObject)
        {
            if (detailTemplates.ContainsKey(targetObject))
            {
                detailTemplates[targetObject].Close();
                RemoveDetailView(targetObject);
            }
        }

        public static void RemoveDetailView(this IStorable targetObject)
        {
            if (detailTemplates.ContainsKey(targetObject))
                detailTemplates.Remove(targetObject);
        }

        public static DetailViewTemplate TryGetDetailView(this IStorable targetObject)
        {
            if (targetObject != null)
            {
                if (detailTemplates.ContainsKey(targetObject))
                    return detailTemplates[targetObject];

                var template = new DetailViewTemplate(targetObject.GetType(), targetObject);
                detailTemplates.Add(targetObject, template);

                return template;
            }

            return null;
        }
    }
}
