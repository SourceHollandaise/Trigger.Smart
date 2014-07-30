using System.Linq;
using Eto.Drawing;
using System.Linq.Expressions;

namespace System
{

    public static class TypeExtensions
    {
        public static object GetDefaultPropertyValue(this object target)
        {
            var attribute = target.GetType().GetCustomAttributes(typeof(System.ComponentModel.DefaultPropertyAttribute), true).FirstOrDefault() as System.ComponentModel.DefaultPropertyAttribute;

            if (attribute != null)
            {
                var property = target.GetType().GetProperty(attribute.Name);
                if (property != null)
                    return property.GetValue(target, null);
            }

            return null;

        }
    }

    public class FieldNames<T>
    {
        public string GetName<U>(Expression<Func<T, U>> expression)
        {
            MemberExpression memberExpression = expression.Body as MemberExpression;
            if (memberExpression != null)
                return memberExpression.Member.Name;

            throw new InvalidOperationException("Member expression expected");
        }
    }
}
	