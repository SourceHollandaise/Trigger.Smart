using System;
using Eto.Forms;
using XForms.Store;
using Eto.Drawing;

namespace XForms.Design
{

    public class ListDetailItemBuilder
    {
        readonly IStorable currentObject;

        readonly string headerText;

        readonly bool addCommandBar;

        public ListDetailItemBuilder(IStorable currentObject, string headerText = null, bool addCommandBar = false)
        {
            this.addCommandBar = addCommandBar;
            this.headerText = headerText;
            this.currentObject = currentObject;
        }

        public Control GetContent()
        {
            var cardViewGroupBox = new GroupBox();
            cardViewGroupBox.Size = new Size(-1, -1);
            if (headerText != null)
            {
                cardViewGroupBox.Text = headerText;

                try
                {
                    cardViewGroupBox.Font = new Font(cardViewGroupBox.Font.Family, 14f, FontStyle.Bold);

                }
                catch
                {

                }
            }

            var detailLayout = new DynamicLayout();
            detailLayout.Size = new Size(-1, -1);

            var detailViewDescriptorType = DetailViewDescriptorProvider.GetDescriptor(currentObject.GetType());
            if (detailViewDescriptorType != null)
            {
                var detailViewDescriptor = Activator.CreateInstance(detailViewDescriptorType) as IDetailViewDescriptor;

                var detailViewBuilder = new DetailViewBuilder(detailViewDescriptor, currentObject, addCommandBar);

                detailLayout.Add(detailViewBuilder.GetContent(), true);
                cardViewGroupBox.Content = detailLayout;
            }
                
            return cardViewGroupBox;
        }
    }
}
