using GalaSoft.MvvmLight;
using SimpleFolderSizeViewer.App.Extension;
using SimpleFolderSizeViewer.App.Model;
using SimpleFolderSizeViewer.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.ViewModel
{
    public class FolderContentViewModel : ViewModelBase
    {
        public ObservableCollection<IFileSystemModel<FileSystemEntity>> SubNodes { get; set; }

        public FolderContentViewModel()
        {
            SubNodes = new ObservableCollection<IFileSystemModel<FileSystemEntity>>();
        }

        public void UpdateSubItems(FolderModel selectedFolder)
        {
            SubNodes.Clear();

            SubNodes.AddRange(selectedFolder.SubFolders);
            SubNodes.AddRange(selectedFolder.SubFiles);
        }
    }
}
