using System;
using Eto.Forms;
using XForms.Store;
using Eto.Drawing;
using XForms.Commands;

namespace XForms.Design
{

    public class ListDetailItemBuilder
    {
        readonly IStorable currentObject;

        readonly string headerText;

        readonly bool addCommandBar;

        readonly IDetailViewDescriptor descriptor;

        public ListDetailItemBuilder(IDetailViewDescriptor descriptor, IStorable currentObject, string headerText = null, bool addCommandBar = false)
        {
            this.descriptor = descriptor;
            this.addCommandBar = addCommandBar;
            this.headerText = headerText;
            this.currentObject = currentObject;
        }

        public Control GetContent()
        {
            var detailViewGroupBox = new GroupBox();
            detailViewGroupBox.Size = new Size(-1, -1);
            if (headerText != null)
            {
                detailViewGroupBox.Text = headerText;

                try
                {
                    detailViewGroupBox.Font = new Font(detailViewGroupBox.Font.Family, 14f, FontStyle.Bold);

                }
                catch
                {

                }
            }

            var detailLayout = new DynamicLayout();
            detailLayout.Size = new Size(-1, -1);

            if (addCommandBar)
            {  
                var commandBarBuilder = new DetailViewCommandBarBuilder(currentObject, descriptor.Commands);
                detailLayout.Add(commandBarBuilder.GetContent());
  
                if (descriptor.IsTaggable)
                {
                    var tabButtonBuilder = new TagButtonBuilder(currentObject);
                    detailLayout.Add(tabButtonBuilder.GetContent());
                }
            }

            var detailViewBuilder = new DetailViewBuilder(descriptor, currentObject, false);
            detailLayout.Add(detailViewBuilder.GetContent(), true);
            detailViewGroupBox.Content = detailLayout;
                
            return detailViewGroupBox;
        }
    }
}
