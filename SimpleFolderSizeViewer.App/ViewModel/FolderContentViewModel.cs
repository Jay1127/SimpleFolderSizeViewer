using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
    public class FolderContentViewModel : ViewModelBase, IFolderSelectionHandle
    {
        public ObservableCollection<IFileSystemModel<FileSystemEntity>> SubNodes { get; set; }   

        private FolderModel _selectedFolder;
        public FolderModel SelectedFolder
        {
            get => _selectedFolder;
            set => Set<FolderModel>(ref _selectedFolder, value);
        }

        public RelayCommand<object> UpdateSelectedFolderCommand { get; }
        public FolderSelectedHandler FolderSelected { get; set; }

        public FolderContentViewModel()
        {
            SubNodes = new ObservableCollection<IFileSystemModel<FileSystemEntity>>();
            UpdateSelectedFolderCommand = new RelayCommand<object>(UpdateSelectedItem);
        }

        private void UpdateSelectedItem(object newItem)
        {
            if (newItem is FolderModel)
            {
                SelectedFolder = newItem as FolderModel;
                
                FolderSelected(SelectedFolder);
            }
            else
            {

            }
        }

        public void UpdateSubItems(FolderModel selectedFolder)
        {
            SelectedFolder = selectedFolder;

            SubNodes.Clear();

            SubNodes.AddRange(selectedFolder.SubFolders);
            SubNodes.AddRange(selectedFolder.SubFiles);
        }
    }
}
