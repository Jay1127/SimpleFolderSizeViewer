﻿using GalaSoft.MvvmLight;
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

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => Set(ref _isSelected, value);
        }

        public FolderModel Parent { get; protected set; }

        public string SizeFormat
        {
            get
            {
                return $"{Entity.Size.SizeByUnit:F2}{FileSize.Unit}";
            }
        }

        public FileSystemBaseModel(T model, FolderModel parent)
        {
            Entity = model;
            Parent = parent;
        }

        public void Dispose()
        {
            Entity?.Dispose();
        }
    }
}
