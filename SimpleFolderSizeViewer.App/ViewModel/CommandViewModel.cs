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
        public RelayCommand ShowErrorLogDialogCommand { get; }

        public RelayCommand SetFolderSizeUnitCommand { get; }

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
            ShowColumnSettingsCommand = new RelayCommand(ExecuteShowDialogCommand<ColumnSettingDialog>);
            ShowFilterDialogCommand = new RelayCommand(ExecuteShowDialogCommand<FilteringDialog>);
            ShowErrorLogDialogCommand = new RelayCommand(ExecuteShowDialogCommand<ErrorLogDialog>);

            SetFolderSizeUnitCommand = new RelayCommand(ExecuteFolderSizeUnitCommand);
        }

        private void ExecuteOpenCommand()
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var folderTree = _mainViewModel.FolderTreeViewModel;
                    var root = new Folder(dialog.FileName, null);
                    root.InitSubFolders();

                    var rootModel = new FolderModel(root);      // no root is null;                    
                    rootModel.InitSubFolders();

                    folderTree.UpdateRoot(rootModel);

                    _pathNavigator.Clear();
                    _pathNavigator.AddPath(rootModel);
                }
            }
        }

        private void ExecuteScanCommand()
        {
            var folderTree = _mainViewModel.FolderTreeViewModel;
            var root = folderTree.Root;

            if (root == null) return;

            //var scanStatus = _mainViewModel.ScanStatus;
            //scanStatus = new ScanStatus(root.SubFolders.Count);

            Parallel.ForEach(root.SubFolders, async subFolder =>
            {
                await Task.Run(() =>
                {
                    var builder = new FolderSizeBuilder();
                    builder.FolderSizeChanged += () =>
                    {
                        subFolder.RaisePropertyChanged(nameof(subFolder.SizeFormat));

                        // 느린 경우(SubFolder/FIleCount) wrapping해서 proertychanged 호출...!!
                        subFolder.RaisePropertyChanged(nameof(subFolder.Entity));
                    };

                    builder.Build(subFolder.Entity);

                    subFolder.InitSubFolders();
                    subFolder.InitSubFolderTree();
                });

                //scanStatus.Complete();
            });
            
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

        private void ExecuteFolderSizeUnitCommand()
        {
            _mainViewModel.FolderContentViewModel.UpdateFileSizeUnit();
            //Core.DataModel.FileSize.Unit
        }

        private void ExecuteShowDialogCommand<T>() where T : Window, new()
        {
            ShowDialog<T>();
        }

        public void ShowDialog<T>() where T : Window, new()
        {
            var dialog = new T();
            dialog.ShowDialog();
        }

        private bool CanMoveParent(FolderModel selectedFolder)
        {
            return selectedFolder != null && selectedFolder.Parent != null;
        }
    }
}
