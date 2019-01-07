using GalaSoft.MvvmLight;
using SimpleFolderSizeViewer.App.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.Model
{
    public class FilteringInput : ObservableObject
    {
        private FilteringOption _filteringOption;
        public FilteringOption FilteringOption
        {
            get => _filteringOption;
            set
            {                
                Set(ref _filteringOption, value);

                if(_filteringOption == FilteringOption.None)
                {
                    StartRange = null;
                    EndRange = null;
                }
            }
        }

        private double? _startRange;
        public double? StartRange
        {
            get => _startRange;
            set
            {
                Set(ref _startRange, value);
            }
        }

        private double? _endRange;
        public double? EndRange
        {
            get => _endRange;
            set
            {
                Set(ref _endRange, value);
            }
        }

        public bool CanFilter { get => _filteringOption != FilteringOption.None; }

        public FilteringInput()
        {
            FilteringOption = FilteringOption.None;
        }
    }
}
