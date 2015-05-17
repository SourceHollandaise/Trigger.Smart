using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Eto.Drawing;
using Eto.Forms;
using XForms.Dependency;
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
                resultListBox.Font = new Font(resultListBox.Font.Family, 18F);
            }
            catch
            {

            }
               
            var searchBox = new SearchBox()
            {
                Size = new Size(600, 80)
            };
       
            try
            {
                searchBox.Font = new Font(searchBox.Font.Family, 18F);
                searchBox.PlaceholderText = "Application Search";
                searchBox.Size = new Size(-1, -1);
            }
            catch
            {

            }

            searchBox.TextChanged += (sender, e) =>
            {
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

                        var imageItem = new ImageListItem
                        { 
                            Image = imageAttribute != null ? ImageExtensions.GetImage(imageAttribute.ImageName, 24) : null,

                            Text = itemName + " - " + item.GetRepresentation,
                            Key = item.MappingId.ToString(),
                            Tag = item
                        };

                        if (item is IThumbnailPreviewable)
                        {
                            imageItem.Image = (item as IThumbnailPreviewable).Thumbnail;
                        }

                        resultListBox.Items.Add(imageItem);
                    }
                }
            };

            resultListBox.MouseDoubleClick += (sender, e) =>
            {
                if (resultListBox.SelectedValue != null)
                {
                    if ((resultListBox.SelectedValue as ListItem).Tag != null)
                    {
                        this.Close();

                        ((resultListBox.SelectedValue as ListItem).Tag as IStorable).ShowDetailView();
                    }
                }
            };

            resultListBox.KeyDown += (sender, e) =>
            {
                if (e.Key == Keys.Escape)
                    this.Close();

                if (e.Key == Keys.Enter && resultListBox.SelectedValue != null)
                {
                    if ((resultListBox.SelectedValue as ListItem).Tag != null)
                    {
                        this.Close();

                        ((resultListBox.SelectedValue as ListItem).Tag as IStorable).ShowDetailView();
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

                if (e.Key == Keys.Down || e.Key == Keys.Tab)
                {
                    if (resultListBox.Visible)
                        resultListBox.Focus();
                }
            };

            layout.Add(searchBox);
            layout.Add(resultListBox);

            return layout;
        }

        IList<IStorable> GetResult(string input)
        {
            var descriptor = MapProvider.Instance.ResolveType<IMainViewDescriptor>();
            var store = MapProvider.Instance.ResolveType<IStore>();

            return store.SearchResult(input, descriptor.RegisteredTypes().Distinct().ToArray()).ToList();
        }
    }
}