using System;
using System.Collections.Generic;
using System.Linq;
using Eto.Forms;
using XForms.Dependency;
using XForms.Store;

namespace XForms.Design
{
    public static class ListViewTemplateExtensions
    {
        public static Control CreateListViewLayout(this IListViewDescriptor descriptor, IMainViewTemplate template)
        {
            Control content = null;

            descriptor.ListDetailView = !template.IsReduced;

            if (descriptor.ListDetailView)
            {
                var builder = new ListDetailViewBuilder(descriptor, template.CurrentActiveType);

                content = builder.GetContent();
            }
            else
            {
                var builder = new ListViewBuilder(descriptor, template.CurrentActiveType);

                content = builder.GetContent();

                builder.CurrentGridView.MouseDoubleClick += (sender, e) =>
                {
                    var detailContent = CreateDetailViewLayout(builder.CurrentGridView);
                    if (detailContent != null)
                    {
                        template.SetContent(detailContent);
                    }
                };

                builder.CurrentGridView.KeyUp += (sender, e) =>
                {
                    if (e.Key == Keys.Enter)
                    {
                        var detailContent = CreateDetailViewLayout(builder.CurrentGridView);
                        if (detailContent != null)
                        {
                            template.SetContent(detailContent);
                        }
                    }
                };
            }

            TemplateNavigator.Add(content);

            return content;
        }

        static Control CreateDetailViewLayout(GridView currentGridView)
        {
            var currentObject = currentGridView.SelectedItem as IStorable;
            if (currentObject != null)
            {
                currentObject.ShowDetailContentEmbedded();
            }

            return null;
        }

        public static void ReloadList(this GridView gridView, Type type, IEnumerable<IStorable> customDataSource = null)
        {
            if (gridView == null)
                return;

            var descriptorType = ListViewDescriptorProvider.GetDescriptor(type);
            if (descriptorType != null)
            {
                var descriptor = Activator.CreateInstance(descriptorType) as IListViewDescriptor;
                if (descriptor != null)
                {
                    if (customDataSource == null)
                    {
                        var dataSet = descriptor.Repository ?? MapProvider.Instance.ResolveType<IStore>().LoadAll(type);

                        if (descriptor.Filter != null)
                            gridView.DataStore = new DataStoreCollection(dataSet.Where(descriptor.Filter));
                        else
                            gridView.DataStore = new DataStoreCollection(dataSet);
                    }
                    else
                    {
                        var dataSet = descriptor.Repository ?? customDataSource;

                        if (descriptor.Filter != null)
                            dataSet = customDataSource.Where(descriptor.Filter);
                        gridView.DataStore = new DataStoreCollection(dataSet);
                    }
                }
            }
        }
    }
}
