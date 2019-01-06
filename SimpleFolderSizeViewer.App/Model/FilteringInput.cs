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
            }
        }

        private string _startRangeText;
        public string StartRangeText
        {
            get => _startRangeText;
            set
            {
                Set(ref _startRangeText, value);
            }
        }

        private string _endRangeText;
        public string EndRangeText
        {
            get => _endRangeText;
            set
            {
                Set(ref _endRangeText, value);
            }
        }

        public double StartRange { get; set; }
        public double EndRange { get; set; }

        public bool CanFilter { get; private set; }

        public FilteringInput()
        {
            FilteringOption = FilteringOption.None;
            StartRangeText = string.Empty;
            EndRangeText = string.Empty;
        }

        public void VerifyAndInitFilter()
        {
            if(FilteringOption == FilteringOption.None)
            {
                CanFilter = false;
                return;
            }

            double startRange = 0.0;
            double endRange = double.MaxValue;

            if(StartRangeText.HasAnyCharacter() && !double.TryParse(StartRangeText, out startRange))
            {
                CanFilter = false;
                return;
            }

            if (EndRangeText.HasAnyCharacter() && !double.TryParse(EndRangeText, out endRange))
            {
                CanFilter = false;
                return;
            }

            CanFilter = true;
            StartRange = startRange;
            EndRange = endRange;
        }      
    }
}
