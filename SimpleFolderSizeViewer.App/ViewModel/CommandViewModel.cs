using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.WindowsAPICodePack.Dialogs;
using SimpleFolderSizeViewer.App.Model;
using SimpleFolderSizeViewer.Core;
using SimpleFolderSizeViewer.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleFolderSizeViewer.App.ViewModel
{
    public class CommandViewModel : ViewModelBase
    {
        private readonly MainViewModel _mainViewModel;

        private Model.PathNavigator _pathNavigator;

        public RelayCommand OpenCommand { get; }
        public RelayCommand ScanCommand { get; }

        public RelayCommand MovePrevFolderCommand { get; }
        public RelayCommand MoveNextFolderCommand { get; }
        public RelayCommand MoveParentFolderCommand { get; }
        public RelayCommand MoveRootFolderCommand { get; }

        public RelayCommand ShowColumnSettingsCommand { get; }
        public RelayCommand ShowFilterDialogCommand { get; }

        public CommandViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            _pathNavigator = Model.PathNavigator.Instance;
            OpenCommand = new RelayCommand(ExecuteOpenCommand);
            ScanCommand = new RelayCommand(ExecuteScanCommand);

            MovePrevFolderCommand = new RelayCommand(ExecuteMovePrevCommand);
            MoveNextFolderCommand = new RelayCommand(ExecuteMoveNextCommand);
            MoveParentFolderCommand = new RelayCommand(ExecuteMoveParentCommand);
            MoveRootFolderCommand = new RelayCommand(ExecuteMoveRootCommand);

            ShowColumnSettingsCommand = new RelayCommand(ExecuteShowColumnSettings);
            ShowFilterDialogCommand = new RelayCommand(ExecuteShowDialog);
        }

        private void ExecuteOpenCommand()
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var folderTree = _mainViewModel.FolderTreeViewModel;
                    var root = new FolderModel(new Folder(dialog.FileName, null));      // no root is null;

                    folderTree.UpdateRoot(root);

                    _pathNavigator.AddPath(root);
                }
            }
        }

        private void ExecuteScanCommand()
        {
            var folderTree = _mainViewModel.FolderTreeViewModel;
            var root = folderTree.Root;

            if (root == null) return;

            FolderSizeBuilder.Build(root.Entity);            
            
            folderTree.UpdateRoot(root);
        }

        private void ExecuteMovePrevCommand()
        {
            if (!_pathNavigator.CanMovePrev())
            {
                return;
            }

            _pathNavigator.MovePrev();
            var folderTree = _mainViewModel.FolderTreeViewModel;
            folderTree.UpdatedSelectedFolder(_pathNavigator.Current);

            _mainViewModel.FolderContentViewModel.UpdateSubItems(_pathNavigator.Current);
        }

        private void ExecuteMoveNextCommand()
        {
            if (!_pathNavigator.CanMoveNext())
            {
                return;
            }

            _pathNavigator.MoveNext();
            var folderTree = _mainViewModel.FolderTreeViewModel;
            folderTree.UpdatedSelectedFolder(_pathNavigator.Current);

            _mainViewModel.FolderContentViewModel.UpdateSubItems(_pathNavigator.Current);
        }

        private void ExecuteMoveParentCommand()
        {
            var folderTree = _mainViewModel.FolderTreeViewModel;

            if (!CanMoveParent(folderTree.SelectedFolder))
            {
                return;
            }

            folderTree.UpdatedSelectedFolder(folderTree.SelectedFolder.Parent);
            _pathNavigator.AddPath(folderTree.SelectedFolder);

            _mainViewModel.FolderContentViewModel.UpdateSubItems(_pathNavigator.Current);
        }

        private void ExecuteMoveRootCommand()
        {            
            _mainViewModel.FolderTreeViewModel.UpdateSelectedFolderToRoot();
        }

        private void ExecuteShowDialog()
        {
            ShowDialog<FilteringDialog>(_mainViewModel.FilteringViewModel);
        }

        private void ExecuteShowColumnSettings()
        {
            ShowDialog<ColumnSettingDialog>(_mainViewModel.ColumnSettingsViewModel);
        }

        private void ShowDialog<T>(object viewModel) where T : Window, new()
        {
            var dialog = new T();
            dialog.DataContext = viewModel;

            dialog.ShowDialog();
        }

        private bool CanMoveParent(FolderModel selectedFolder)
        {
            return selectedFolder != null && selectedFolder.Parent != null;
        }
    }
}
