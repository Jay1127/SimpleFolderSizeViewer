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
    public class CommandViewModel
    {
        private readonly MainViewModel _mainViewModel;

        public RelayCommand OpenCommand { get; }
        public RelayCommand ScanCommand { get; }

        public CommandViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            OpenCommand = new RelayCommand(() => ExecuteOpen());
            ScanCommand = new RelayCommand(() => ExecuteScan());
            
            //_mainViewModel = new ViewModelLocator().Main;
        }

        private void ExecuteOpen()
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    var folderTree = _mainViewModel.FolderTreeViewModel;
                    var root = new FolderModel(dialog.FileName);

                    folderTree.UpdateRoot(root);;
                }
            }
        }

        private void ExecuteScan()
        {
            var folderTree = _mainViewModel.FolderTreeViewModel;
            var root = folderTree.Root;

            FolderSizeBuilder.Build(root.Entity);            
            
            folderTree.UpdateRoot(folderTree.Root);
        }
    }
}
