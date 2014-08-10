
namespace XForms.Design
{
    public class ViewItemDescription
    {
        public string FieldName { get; set; }

        public int Index { get; set; }

        public string LabelText { get; set; }

        public bool ShowLabel { get; set; }

        public bool Fill { get; set; }

        public bool Visible { get; set; }

        public bool Required { get; set; }

        public bool ReadOnly { get; set; }

        public ListPropertyMode ListMode { get; set; }

        public LabelOrientation LabelOrientation { get; set; }

        public ViewItemDescription(string fieldName, int index)
        {
            FieldName = fieldName;
            Index = index;
            LabelOrientation = LabelOrientation.Left;
            ShowLabel = true;
            Visible = true;
        }
    }
}
