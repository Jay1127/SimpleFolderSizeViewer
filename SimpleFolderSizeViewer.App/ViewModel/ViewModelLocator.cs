using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace SimpleFolderSizeViewer.App.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<FilteringViewModel>();
            SimpleIoc.Default.Register<ColumnSettingsViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public FilteringViewModel Filtering
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FilteringViewModel>();
            }
        }

        public ColumnSettingsViewModel ColumnSettings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ColumnSettingsViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}