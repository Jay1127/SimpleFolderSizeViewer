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
        private bool _canNotifing;

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

        /// <summary>
        /// (DataGrid에서 더블클릭된) 선택된 객체에 대해 업데이트
        /// </summary>
        /// <param name="selectedFolder"></param>
        public void UpdatedSelectedFolder(FolderModel selectedFolder)
        {
            SelectedFolder = selectedFolder;
            SelectedFolder.IsExpanded = true;

            _canNotifing = false;

            SelectedFolder.IsSelected = true;

            _canNotifing = true;

            if (!Root.IsSelected)
            {
                SelectedFolder.Parent.IsExpanded = true;
            }
        }

        /// <summary>
        /// 트리뷰에서 클릭해서 선택된 객체 설정 
        /// </summary>
        /// <param name="newItem"></param>
        private void NotifySelectedFolder(object newItem)
        {
            if (!_canNotifing) return; 

            SelectedFolder = newItem as FolderModel;
            FolderSelected(SelectedFolder);

            var pathNavigator = PathNavigator.Instance;
            pathNavigator.AddPath(SelectedFolder);
        }         

        public void UpdateSelectedFolderToRoot()
        {
            UpdateRoot(Root);
        }

        public void UpdateRoot(FolderModel root)
        {
            Root = root;
            Root.IsExpanded = true;

            UpdatedSelectedFolder(Root);
            FolderSelected(SelectedFolder);

            _canNotifing = false;

            FolderTree = new ObservableCollection<FolderModel>() { Root };

            _canNotifing = true;
        }     
    }
}
