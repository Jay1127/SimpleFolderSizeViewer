using GalaSoft.MvvmLight;
using SimpleFolderSizeViewer.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.ViewModel
{
    public class ErrorLogViewModel : ViewModelBase
    {
        public ObservableCollection<string> ErrorLogs { get; }

        public ErrorLogViewModel()
        {
            ErrorLogs = new ObservableCollection<string>(ErrorLogRepository.Instance.ErrorList);
        }
    }
}
