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

        private bool _isExpaned;
        public bool IsExpanded
        {
            get => _isExpaned;
            set => Set<bool>(ref _isExpaned, value);
        }

        public FolderModel(Folder folder) : this(folder, null)
        {

        }

        public FolderModel(Folder folder, FolderModel parent) : base(folder, parent)
        {
            var subfolders = from subFolder in folder.SubFolders
                             select new FolderModel(subFolder, this);

            var subFiles = from subFile in folder.SubFiles
                           select new FileModel(subFile, this);

            SubFolders = new ReadOnlyObservableCollection<FolderModel>(
                new ObservableCollection<FolderModel>(subfolders));

            SubFiles = new ReadOnlyObservableCollection<FileModel>(
                new ObservableCollection<FileModel>(subFiles));
        }
    }
}
