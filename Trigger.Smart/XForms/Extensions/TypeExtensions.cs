using System.Linq;
using System.Reflection;

namespace System
{
    public static class TypeExtensions
    {
        public static string GetDefaultPropertyValue(this object target)
        {
            var attribute = target.GetType().FindAttribute<System.ComponentModel.DefaultPropertyAttribute>();

            if (attribute != null)
            {
                var property = target.GetType().GetProperty(attribute.Name);
                if (property != null)
                    return property.GetValue(target, null) as string;
            }

            return null;
        }

        public static T FindAttribute<T>(this Type type) where T: Attribute
        {
            var attribute = type.GetCustomAttributes(typeof(T), true).FirstOrDefault();
            if (attribute != null)
                return (T)attribute;
            return null;
        }

        public static T FindAttribute<T>(this PropertyInfo info) where T: Attribute
        {
            var attribute = info.GetCustomAttributes(typeof(T), true).FirstOrDefault();
            if (attribute != null)
                return (T)attribute;
            return null;
        }
    }
}
	