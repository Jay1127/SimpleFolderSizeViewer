using SimpleFolderSizeViewer.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.ViewModel
{
    public delegate void FolderSelectedHandler(FolderModel selectedFolder);

    public interface IFolderSelectionHandle
    {
        FolderModel SelectedFolder { get; set; }
        FolderSelectedHandler FolderSelected { get; set; }
    }
}
