using SimpleFolderSizeViewer.Core.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SimpleFolderSizeViewer.App.Model
{
    class FilteringService
    {
        private ICollectionView _collectionView;
        private readonly ICollection<IFileSystemModel<FileSystemEntity>> _items;

        public FilteringService(ICollection<IFileSystemModel<FileSystemEntity>> items)
        {
            _items = items;
            _collectionView = CollectionViewSource.GetDefaultView(items);
        }

        public void Filter(FilteringInput fIlteringInput)
        {
            if (!fIlteringInput.CanFilter)
            {
                _collectionView.Filter = null;
                return;
            }

            if(fIlteringInput.FilteringOption == FilteringOption.Percent)
            {

            }

            if(fIlteringInput.FilteringOption == FilteringOption.Size)
            {
                _collectionView.Filter = (obj) =>
                {
                    double size = (obj as IFileSystemModel<FileSystemEntity>).FileSize.SizeByUnit;

                    return size >= fIlteringInput.StartRange &&
                           size <= fIlteringInput.EndRange;
                };
            }
        }
    }
}
