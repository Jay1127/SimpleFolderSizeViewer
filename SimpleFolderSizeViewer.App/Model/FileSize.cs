using GalaSoft.MvvmLight;
using SimpleFolderSizeViewer.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.Model
{
    public enum SizeUnit
    {
        B,
        KB,
        MB,
        GB
    }

    public class FileSize : ObservableObject
    {
        private readonly FileSystemEntity _fileSystemEntity;

        public double SizeByByte { get => _fileSystemEntity.Size; }
        public static event EventHandler FileSizeUnitChanged;

        public double SizeByUnit
        {
            get
            {
                return CalculateSizeByUnit();
            }
        }

        public string SizeFormat
        {
            get
            {
                return $"{SizeByUnit:F2}{Unit}";
            }
        }

        private static SizeUnit _unit;
        public static SizeUnit Unit
        {
            get => _unit;
            set
            {
                _unit = value;
                FileSizeUnitChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public FileSize(FileSystemEntity fileSystemEntity)
        {
            _fileSystemEntity = fileSystemEntity;
        }

        private double CalculateSizeByUnit()
            => SizeByByte / Math.Pow(1024, (int)Unit);
    }
}
