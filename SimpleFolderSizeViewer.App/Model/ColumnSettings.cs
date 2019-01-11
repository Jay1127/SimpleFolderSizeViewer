using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.Model
{
    public class ColumnSettings : ObservableObject
    {
        private bool _isNameVisible;
        private bool _isSizeVisible;
        private bool _isPercentVisible;
        private bool _isExtensionVisible;
        private bool _isSubFolderCountVisible;
        private bool _isSubFileCountVisible;
        private bool _isModifiedTimeVisible;
        private bool _isCreationTimeVisible;

        public bool IsNameVisible
        {
            get => _isNameVisible;
            set => Set(ref _isNameVisible, value);
        }

        public bool IsSizeVisible
        {
            get => _isSizeVisible;
            set => Set(ref _isSizeVisible, value);
        }

        public bool IsPercentVisible
        {
            get => _isPercentVisible;
            set => Set(ref _isPercentVisible, value);
        }

        public bool IsExtensionVisible
        {
            get => _isExtensionVisible;
            set => Set(ref _isExtensionVisible, value);
        }

        public bool IsSubFolderCountVisible
        {
            get => _isSubFolderCountVisible;
            set => Set(ref _isSubFolderCountVisible, value);
        }

        public bool IsSubFileCountVisible
        {
            get => _isSubFileCountVisible;
            set => Set(ref _isSubFileCountVisible, value);
        }

        public bool IsModifiedTimeVisible
        {
            get => _isModifiedTimeVisible;
            set => Set(ref _isModifiedTimeVisible, value);
        }

        public bool IsCreationTimeVisible
        {
            get => _isCreationTimeVisible;
            set => Set(ref _isCreationTimeVisible, value);
        }

        public ColumnSettings()
        {
            IsNameVisible = true;
            IsSizeVisible = true;
            IsPercentVisible = true;
            IsExtensionVisible = true;
        }
    }
}
