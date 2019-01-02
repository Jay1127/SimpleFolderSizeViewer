using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SimpleFolderSizeViewer.App.Model;
using SimpleFolderSizeViewer.Core;
using SimpleFolderSizeViewer.Core.DataModel;

namespace SimpleFolderSizeViewer.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private FolderTreeViewModel _folderTreeViewModel;

        public FolderContentViewModel FolderContentViewModel { get; private set; }
        public ColumnSettingsViewModel ColumnSettingsViewModel { get; private set; }
        public CommandViewModel CommandViewModel { get; private set; }

        public FolderTreeViewModel FolderTreeViewModel
        {
            get => _folderTreeViewModel;
            set => Set<FolderTreeViewModel>(ref _folderTreeViewModel, value);
        }

        public MainViewModel()
        {
            log.Info("App start");
            
            ColumnSettingsViewModel = new ColumnSettingsViewModel();
            CommandViewModel = new CommandViewModel(this);
            FolderContentViewModel = new FolderContentViewModel();
            FolderTreeViewModel = new FolderTreeViewModel
            {
                SelectionChangedCommand = new RelayCommand<object>(folder =>
                {
                    UpdateSelectedFolder(folder as FolderModel);
                })
            };
        }

        /// <summary>
        /// 선택된 폴더 업데이트 후 DataGrid에 SubNode초기화
        /// </summary>
        /// <param name="folder"></param>
        public void UpdateSelectedFolder(FolderModel folder)
        {
            if (folder == null) return;

            FolderTreeViewModel.SelectedFolder = folder;
            FolderContentViewModel.Update(folder);
        }
    }
}