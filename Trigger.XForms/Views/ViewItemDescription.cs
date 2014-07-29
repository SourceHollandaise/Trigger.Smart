using System.Collections.Generic;

namespace Trigger.XForms
{
    public class ViewItemDescription
    {
        public ViewItemDescription(string fieldName, int index)
        {
            FieldName = fieldName;
            Index = index;
            LabelOrientation = LabelOrientation.Left;
            ShowLabel = true;
        }

        public string FieldName { get; set; }

        public string LabelText { get; set; }

        public bool ShowLabel { get; set; }

        public LabelOrientation LabelOrientation { get; set; }

        public int Index { get; set; }

        public bool Fill { get; set; }
    }
}
