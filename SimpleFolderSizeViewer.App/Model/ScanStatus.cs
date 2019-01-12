using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.Model
{
    public class ScanStatus : ObservableObject
    {
        private object _scanLock = new object();

        public bool IsScanning
            => CompletedCount != TotalCount;

        public string ScanText
            => $"Scanning folder...({CompletedCount}/{TotalCount})";

        public int TotalCount { get; }

        private int _completedCount;
        public int CompletedCount
        {
            get => _completedCount;
            private set
            {
                Set(ref _completedCount, value);
                RaisePropertyChanged(nameof(ScanText));
                RaisePropertyChanged(nameof(IsScanning));
            }
        }

        public ScanStatus(int totalCount)
        {
            CompletedCount = 0;
            TotalCount = totalCount;
        }

        public void Complete()
        {
            lock (_scanLock)
            {
                CompletedCount++;
            }
        }
    }
}
