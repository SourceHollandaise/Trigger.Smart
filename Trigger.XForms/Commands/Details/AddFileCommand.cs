using Eto.Forms;
using Trigger.XForms.Controllers;
using System;
using Trigger.XStorable.DataStore;
using Trigger.XStorable.Dependency;
using Trigger.XForms.Visuals;

namespace Trigger.XForms.Controllers
{

    public class AddFileCommand : IAddFileCommand
    {

        public void Execute(IStorable current)
        {
            var service = DependencyMapProvider.Instance.ResolveType<IFileDataService>();
            if (current != null)
            {
                var fileDialog = new OpenFileDialog();
                fileDialog.MultiSelect = false;
                if (fileDialog.ShowDialog(null) == DialogResult.Ok)
                {
                    service.AddFile(current as IFileData, fileDialog.FileName);
                    fileDialog.Dispose();
                }
            }
        }

        public string ID
        {
            get
            {
                return "cmd_add_file";
            }
        }

        public string Name
        {
            get
            {
                return "Datei aufnehmen";
            }
        }
    }
}
