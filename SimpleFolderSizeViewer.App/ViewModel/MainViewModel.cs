using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using log4net;
using SimpleFolderSizeViewer.App.Model;
using SimpleFolderSizeViewer.Core;
using SimpleFolderSizeViewer.Core.DataModel;

namespace SimpleFolderSizeViewer.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private FolderTreeViewModel _folderTreeViewModel;
        private ScanStatus _scanStatus;

        public FolderContentViewModel FolderContentViewModel { get; private set; }
        public ColumnSettingsViewModel ColumnSettingsViewModel { get; private set; }
        public CommandViewModel CommandViewModel { get; private set; }
        public FilteringViewModel FilteringViewModel { get; }

        public FolderTreeViewModel FolderTreeViewModel
        {
            get => _folderTreeViewModel;
            set => Set<FolderTreeViewModel>(ref _folderTreeViewModel, value);
        }

        public ScanStatus ScanStatus  
        {
            get => _scanStatus;
            set => Set<ScanStatus>(ref _scanStatus, value);
        }

        public MainViewModel()
        {
            log.Info("App start");

            ColumnSettingsViewModel = new ViewModelLocator().ColumnSettings;
            CommandViewModel = new CommandViewModel(this);
            FolderContentViewModel = new FolderContentViewModel();
            FolderTreeViewModel = new FolderTreeViewModel();
            FilteringViewModel = new ViewModelLocator().Filtering;

            FolderTreeViewModel.FolderSelected += FolderContentViewModel.UpdateSubItems;
            FolderContentViewModel.FolderSelected += FolderTreeViewModel.UpdatedSelectedFolder;
            FilteringViewModel.FilteringDataChanged += FolderContentViewModel.Filter;
        }
    }
}