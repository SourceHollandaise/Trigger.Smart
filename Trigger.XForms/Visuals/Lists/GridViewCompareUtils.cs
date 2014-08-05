using System;
using Eto.Forms;
using Trigger.XStorable.DataStore;

namespace Trigger.XForms.Visuals
{

    class GridViewCompareUtils
    {
        IListViewDescriptor descriptor;

        GridColumn column;

        public GridViewCompareUtils(IListViewDescriptor descriptor)
        {
            this.descriptor = descriptor;
        }

        public GridViewCompareUtils(GridColumn column)
        {
            this.column = column;

        }

        public int Compare(object x, object y)
        {
            var xValue = x.GetType().GetProperty(descriptor.DefaultSortProperty).GetValue(x, null);
            var yValue = y.GetType().GetProperty(descriptor.DefaultSortProperty).GetValue(y, null);

            if (xValue == null && yValue == null)
                return 0;
            else if (xValue == null)
                return descriptor.DefaultSorting == ColumnSorting.Ascending ? -1 : 1;
            else if (yValue == null)
                return descriptor.DefaultSorting == ColumnSorting.Ascending ? 1 : -1;
            else
            {
                if (xValue is DateTime && yValue is DateTime)
                {
                    var result = DateTime.Compare((DateTime)xValue, (DateTime)yValue);

                    return descriptor.DefaultSorting == ColumnSorting.Ascending ? (result * 1) : (result * -1);
                }

                if (xValue is string && yValue is string)
                {
                    var result = string.Compare((string)xValue, (string)yValue, StringComparison.CurrentCulture);

                    return descriptor.DefaultSorting == ColumnSorting.Ascending ? (result * 1) : (result * -1);
                }

                if (xValue is IStorable && yValue is IStorable)
                {
                    var result = ((IStorable)xValue).CompareTo((IStorable)yValue);

                    return descriptor.DefaultSorting == ColumnSorting.Ascending ? (result * 1) : (result * -1);
                }

                return 0;
            }
        }

        public int ColumnCompare(object x, object y)
        {
            var xValue = x.GetType().GetProperty(column.ID).GetValue(x, null);
            var yValue = y.GetType().GetProperty(column.ID).GetValue(y, null);

            if (xValue == null && yValue == null)
                return 0;
            else if (xValue == null)
                return -1;
            else if (yValue == null)
                return 1;
            else
            {
                if (xValue is DateTime && yValue is DateTime)
                {
                    return DateTime.Compare((DateTime)xValue, (DateTime)yValue);
                }

                if (xValue is string && yValue is string)
                {
                    return string.Compare((string)xValue, (string)yValue, StringComparison.CurrentCulture);
                }

                if (xValue is bool && yValue is bool)
                {
                    return string.Compare(xValue.ToString(), yValue.ToString(), StringComparison.CurrentCulture);
                }

                if (xValue is Enum && yValue is Enum)
                {
                    return string.Compare(Enum.GetName(xValue.GetType(), xValue), Enum.GetName(yValue.GetType(), yValue), StringComparison.CurrentCulture);
                }

                if (xValue is IStorable && yValue is IStorable)
                {
                    return ((IStorable)xValue).CompareTo((IStorable)yValue);
                }
            }
            return 0;
        }
    }
}
