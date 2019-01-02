using GalaSoft.MvvmLight;
using SimpleFolderSizeViewer.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleFolderSizeViewer.App.Model
{
    public class FolderModel : FileSystemBaseModel<Folder>
    {
        public ReadOnlyObservableCollection<FolderModel> SubFolders { get; set; }
        public ReadOnlyObservableCollection<FileModel> SubFiles { get; set; }

        public bool IsExpanded { get; set; }

        public override ICommand DoubleClickedCommand { get; set; }

        public FolderModel(string path) : this(new Folder(path))
        {

        }

        public FolderModel(Folder folder) : base(folder)
        {
            var subfolders = from subFolder in folder.SubFolders
                             select new FolderModel(subFolder);

            var subFiles = from subFile in folder.SubFiles
                           select new FileModel(subFile);

            SubFolders = new ReadOnlyObservableCollection<FolderModel>(
                new ObservableCollection<FolderModel>(subfolders));

            SubFiles = new ReadOnlyObservableCollection<FileModel>(
                new ObservableCollection<FileModel>(subFiles));
        }
    }
}
