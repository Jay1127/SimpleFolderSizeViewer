using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.Model
{
    public class ColumnSettings : ObservableObject
    {
        public bool IsNameVisible { get; set; }
        public bool IsSizeVisible { get; set; }
        public bool IsPercentVisible { get; set; }
        public bool IsExtensionVisible { get; set; }
        public bool IsSubFolderCountVisible { get; set; }
        public bool IsSubFileCountVisible { get; set; }
        public bool IsModifiedTimeVisible { get; set; }
        public bool IsCreationTimeVisible { get; set; }

        public ColumnSettings()
        {
            IsNameVisible = true;
            IsSizeVisible = true;
            IsPercentVisible = true;
            IsExtensionVisible = true;
        }
    }
}
