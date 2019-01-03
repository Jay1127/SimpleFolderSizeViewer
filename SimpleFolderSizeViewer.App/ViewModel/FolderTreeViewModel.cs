using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SimpleFolderSizeViewer.App.Model;
using SimpleFolderSizeViewer.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleFolderSizeViewer.App.ViewModel
{
    public class FolderTreeViewModel : ViewModelBase, IFolderSelectionHandle
    {
        private ObservableCollection<FolderModel> _folderTree;
        public ObservableCollection<FolderModel> FolderTree
        {
            get => _folderTree;
            set => Set<ObservableCollection<FolderModel>>(ref _folderTree, value);
        }

        private FolderModel _root;
        public FolderModel Root
        {
            get => _root;
            set => Set<FolderModel>(ref _root, value);
        }

        private FolderModel _selectedFolder;
        public FolderModel SelectedFolder
        {
            get => _selectedFolder;
            set => Set<FolderModel>(ref _selectedFolder, value);
        }

        public RelayCommand<object> UpdateSelectedFolderCommand { get; }
        
        public FolderSelectedHandler FolderSelected { get; set; }

        public FolderTreeViewModel()
        {
            UpdateSelectedFolderCommand = new RelayCommand<object>(NotifySelectedFolder);
        }

        public void UpdatedSelectedFolder(FolderModel selectedFolder)
        {
            SelectedFolder = selectedFolder;
        }

        private void NotifySelectedFolder(object newItem)
        {
            UpdatedSelectedFolder(newItem as FolderModel);
            FolderSelected(SelectedFolder);
        } 

        public void UpdateRoot(FolderModel root)
        {
            Root = root;
            SelectedFolder = root;
            FolderTree = new ObservableCollection<FolderModel>() { Root };
        }        
    }
}
