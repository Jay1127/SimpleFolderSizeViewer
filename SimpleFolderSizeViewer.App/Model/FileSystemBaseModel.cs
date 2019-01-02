using GalaSoft.MvvmLight;
using SimpleFolderSizeViewer.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleFolderSizeViewer.App.Model
{
    public abstract class FileSystemBaseModel<T> : ObservableObject, IFileSystemModel<T> where T : FileSystemEntity
    {
        public T Entity { get; }
        public bool IsSelected { get; set; }
        public abstract ICommand DoubleClickedCommand { get; set; }

        public FileSystemBaseModel(T model)
        {
            Entity = model;
        }

        public void Dispose()
        {
            DoubleClickedCommand = null;
            Entity?.Dispose();
        }
    }
}
