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

            ShowFilterDialogCommand = new RelayCommand(ExecuteShowFilterDialogCommand);
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
            folderTree.UpdatedSelectedFolder(folderTree.SelectedFolder.Parent);
            _pathNavigator.AddPath(folderTree.SelectedFolder);

            _mainViewModel.FolderContentViewModel.UpdateSubItems(_pathNavigator.Current);
        }

        private void ExecuteMoveRootCommand()
        {            
            _mainViewModel.FolderTreeViewModel.UpdateSelectedFolderToRoot();
        }

        private void ExecuteShowFilterDialogCommand()
        {
            var dialog = new FilteringDialog()
            {
                DataContext = _mainViewModel.FilteringViewModel
            };

            dialog.ShowDialog();
        }
    }
}
