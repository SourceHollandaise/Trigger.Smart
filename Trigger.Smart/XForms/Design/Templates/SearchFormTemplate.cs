using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Model;
using XForms.Store;

namespace XForms.Design
{
    public class SearchFormTemplate : Form
    {
        public SearchFormTemplate()
        {
            this.WindowStyle = WindowStyle.Default;
            this.Size = new Size(600, 80);
            this.Topmost = true;

            this.Content = GetContent();
        }

        Control GetContent()
        {
            var layout = new DynamicLayout();

            ListBox resultListBox = new ListBox()
            {
                Visible = false,
            };

            try
            {
                resultListBox.Font = new Font(resultListBox.Font.Family, 14F);
            }
            catch
            {

            }
               
            var searchBox = new SearchBox()
            {
                Size = new Size(-1, -1),
                PlaceholderText = "Application Search"
            };

            try
            {
                searchBox.Font = new Font(searchBox.Font.Family, 15F);
            }
            catch
            {

            }

            searchBox.TextChanged += (sender, e) =>
            {
                System.Threading.Thread.Sleep(200);
                resultListBox.Visible = searchBox.Text.Length >= 2;

                Size = resultListBox.Visible ? new Size(600, 480) : new Size(600, 80);

                if (resultListBox.Visible)
                {
                    resultListBox.Items.Clear();
 
                    var resultSet = GetResult(searchBox.Text);

                    foreach (var item in resultSet)
                    {
                        var displayNameAttribute = item.GetType().FindAttribute<DisplayNameAttribute>();

                        var imageAttribute = item.GetType().FindAttribute<ImageNameAttribute>();

                        string itemName = item.GetType().Name;

                        if (displayNameAttribute != null)
                            itemName = displayNameAttribute.DisplayName;

                        resultListBox.Items.Add(new ImageListItem
                        { 
                            Image = imageAttribute != null ? ImageExtensions.GetImage(imageAttribute.ImageName, 24) : null,
                            Text = itemName + " - " + item.GetRepresentation,
                            Key = item.MappingId.ToString(),
                            Tag = item
                        });
                    }
                }
            };

            resultListBox.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Escape)
                    this.Close();

                if (e.Key == Keys.Enter && resultListBox.SelectedValue != null)
                {
                    this.Close();

                    if ((resultListBox.SelectedValue as ListItem).Tag != null)
                    {
                        ((resultListBox.SelectedValue as ListItem).Tag as IStorable).ShowDetailContentEmbedded();
                    }
                }
            };

            resultListBox.SelectedValueChanged += (sender, e) =>
            {
                if (resultListBox.SelectedValue != null)
                {

                }
            };

            searchBox.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Escape)
                    this.Close();

                if (e.Key == Keys.Down)
                {
                    if (resultListBox.Visible)
                        resultListBox.Focus();
                }
            };

            layout.Add(searchBox);
            layout.Add(resultListBox);

            return layout;
        }

        IEnumerable<IStorable> GetResult(string input)
        {
            var descriptor = XForms.Dependency.MapProvider.Instance.ResolveType<IMainViewDescriptor>();
            var store = XForms.Dependency.MapProvider.Instance.ResolveType<IStore>();

            return store.SearchResult(input, descriptor.RegisteredTypes().Distinct().ToArray());
        }
    }
}