using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
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

            var resultListBox = new ListBox();
            resultListBox.Visible = false;
            var searchBox = new SearchBox();
            searchBox.Size = new Size(-1, -1);
            searchBox.PlaceholderText = "Application Search";

            try
            {
                searchBox.Font = new Font(searchBox.Font.Family, 18F);
            }
            catch
            {

            }

            searchBox.TextChanged += (sender, e) =>
            {
                resultListBox.Visible = searchBox.Text.Length >= 2;
                Size = resultListBox.Visible ? new Size(600, 480) : new Size(600, 80);

                if (searchBox.Visible)
                {
                    resultListBox.Items.Clear();

                    var resultSet = GetResult(searchBox.Text);

                    foreach (var item in resultSet)
                    {
                        var displayNameAttribute = item.GetType().FindAttribute<System.ComponentModel.DisplayNameAttribute>();

                        string itemName = item.GetType().Name;

                        if (displayNameAttribute != null)
                            itemName = displayNameAttribute.DisplayName;

                        resultListBox.Items.Add(new ListItem
                        { 
                            Text = itemName + " - " + item.GetRepresentation,
                            Key = item.MappingId.ToString(),
                            Tag = item
                        });
                    }
                }
            };

            resultListBox.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Enter && resultListBox.SelectedValue != null)
                {
                    this.Close();

                    if ((resultListBox.SelectedValue as ListItem).Tag != null)
                    {
                        ((resultListBox.SelectedValue as ListItem).Tag as IStorable).ShowDetailContentEmbedded();
                    }
                }
            };

            searchBox.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Escape)
                    this.Close();
            };

            layout.Add(searchBox);
            layout.Add(resultListBox);

            return layout;
        }

        IEnumerable<IStorable> GetResult(string input)
        {
            var descriptor = XForms.Dependency.MapProvider.Instance.ResolveType<IMainViewDescriptor>();
            var store = XForms.Dependency.MapProvider.Instance.ResolveType<IStore>();

            foreach (var type in descriptor.RegisteredTypes())
            {
                var resultSet = store.LoadAll(type).Where(p => p.GetSearchString().ToLowerInvariant().Contains(input.ToLowerInvariant()));

                foreach (var item in resultSet)
                {
                    yield return item;
                }
            }
        }
    }
}