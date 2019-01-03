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

        private PathNavigator _pathNavigator;

        public RelayCommand OpenCommand { get; }
        public RelayCommand ScanCommand { get; }

        public RelayCommand MovePrevFolderCommand { get; }
        public RelayCommand MoveNextFolderCommand { get; }
        public RelayCommand MoveParentFolderCommand { get; }
        public RelayCommand MoveRootFolderCommand { get; }

        public CommandViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            OpenCommand = new RelayCommand(ExecuteOpenCommand);
            ScanCommand = new RelayCommand(ExecuteScanCommand);

            MovePrevFolderCommand = new RelayCommand(ExecuteMovePrevCommand);
            MoveNextFolderCommand = new RelayCommand(ExecuteMoveNextCommand);
            MoveParentFolderCommand = new RelayCommand(ExecuteMoveParentCommand);
            MoveRootFolderCommand = new RelayCommand(ExecuteMoveRootCommand);
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

                    _pathNavigator = new PathNavigator(root.Entity);
                }
            }
        }

        private void ExecuteScanCommand()
        {
            var folderTree = _mainViewModel.FolderTreeViewModel;
            var root = folderTree.Root;

            FolderSizeBuilder.Build(root.Entity);            
            
            folderTree.UpdateRoot(folderTree.Root);
        }

        private void ExecuteMovePrevCommand()
        {
            _pathNavigator.MovePrev();
        }

        private void ExecuteMoveNextCommand()
        {
            _pathNavigator.MoveNext();
        }

        private void ExecuteMoveParentCommand()
        {

        }

        private void ExecuteMoveRootCommand()
        {            
            _mainViewModel.FolderTreeViewModel.UpdateSelectedFolderToRoot();
        }

    }
}
