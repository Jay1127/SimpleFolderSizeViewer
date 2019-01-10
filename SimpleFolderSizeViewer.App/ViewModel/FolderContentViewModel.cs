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

        /// <summary>
        /// 데이터그리드에서 더블클릭으로 선택된 객체 대한 행동 설정
        /// </summary>
        /// <param name="newItem"></param>
        private void UpdateSelectedItem(object newItem)
        {
            if (newItem is FolderModel)
            {
                SelectedFolder = newItem as FolderModel;
                UpdateSubItems(SelectedFolder);
                //SelectedFolder.IsSelected = true;
                //SelectedFolder.IsExpanded = true;                

                //if(SelectedFolder.Parent != null)
                //{
                //    SelectedFolder.Parent.IsExpanded = true;
                //}

                FolderSelected(SelectedFolder);

                var pathNavigator = Model.PathNavigator.Instance;
                pathNavigator.MakeNewPath(SelectedFolder);
            }
            else
            {

            }
        }

        /// <summary>
        /// (TreeView에서 클릭된) 선택된 객체의 서브아이템을 뷰에 업데이트
        /// </summary>
        /// <param name="selectedFolder"></param>
        public void UpdateSubItems(FolderModel selectedFolder)
        {
            SelectedFolder = selectedFolder;

            SubNodes.Clear();

            SubNodes.AddRange(selectedFolder.SubFolders);
            SubNodes.AddRange(selectedFolder.SubFiles);
        }

        public void Filter(FilteringInput filteringInput)
        {
            FilteringService service = new FilteringService(SubNodes);
            service.Filter(filteringInput);
        }

        public void UpdateFileSizeUnit()
        {
            SubNodes.Clear();

            SubNodes.AddRange(SelectedFolder.SubFolders);
            SubNodes.AddRange(SelectedFolder.SubFiles);
        }
    }
}
