using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SimpleFolderSizeViewer.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.ViewModel
{
    public delegate void FilteringDataChangedHandler(FilteringInput fIlteringInput);

    public class FilteringViewModel : ViewModelBase, IRequestCloseViewModel
    {
        public FilteringInput FilteringInput { get; }
        public RelayCommand ApplyCommand { get; private set; }

        public FilteringDataChangedHandler FilteringDataChanged;
        public event EventHandler RequestClose;

        public FilteringViewModel()
        {
            FilteringInput = new FilteringInput();

            ApplyCommand = new RelayCommand(() =>
            {
                FilteringInput.VerifyAndInitFilter();
                FilteringDataChanged?.Invoke(FilteringInput);

                RequestClose?.Invoke(this, EventArgs.Empty);
            });
        }
    }
}
