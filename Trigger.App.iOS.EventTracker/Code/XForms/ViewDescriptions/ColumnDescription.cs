
namespace Trigger.XForms
{
    public class ColumnDescription
    {
        public string FieldName { get; set; }

        public int Index { get; set; }

        public string ColumnHeaderText { get; set; }

        public ColumnSorting Sorting { get; set; }

        public bool AutoSize { get; set; }

        public bool AllowResize { get; set; }

        public ColumnDescription(string fieldName, int index)
        {
            FieldName = fieldName;
            Index = index;
            Sorting = ColumnSorting.Ascending;
            AutoSize = true;
            AllowResize = true;
        }
    }
}
