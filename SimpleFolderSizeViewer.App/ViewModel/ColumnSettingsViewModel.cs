using SimpleFolderSizeViewer.App.Model;
using SimpleFolderSizeViewer.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.ViewModel
{
    public class ColumnSettingsViewModel
    {
        public ColumnSettings ColumnSettings { get; }

        public ColumnSettingsViewModel()
        {
            ColumnSettings = new ColumnSettings();
        }
    }
}
