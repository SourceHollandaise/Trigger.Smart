using Eto.Drawing;
using Eto.Forms;

namespace XForms.Design
{
    public class SearchFormTemplate : Form
    {
        public SearchFormTemplate()
        {
            this.WindowStyle = WindowStyle.Default;
            this.Size = new Size(480, 80);
            this.Topmost = true;

            this.Content = GetContent();
        }

        Control GetContent()
        {
            var layout = new DynamicLayout();

            var resultListBox = new ListBox();
            resultListBox.Visible = false;
            var searchBox = new SearchBox();
            searchBox.Size = new Size(-1, -1);
            searchBox.PlaceholderText = "Application Search";
            searchBox.Font = new Font(searchBox.Font.Family, 16F);

            searchBox.TextChanged += (sender, e) =>
            {
    
                resultListBox.Visible = searchBox.Text.Length > 3;
                this.Size = resultListBox.Visible ? new Size(480, 480) : new Size(480, 80);

            };

            searchBox.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Enter || e.Key == Keys.Escape)
                    this.Close();
            };

            layout.Add(searchBox);
            layout.Add(resultListBox);

            return layout;
        }
    }
}