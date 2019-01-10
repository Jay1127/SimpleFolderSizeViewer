using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core.DataModel
{
    public enum SizeUnit
    {
        B,
        KB,
        MB,
        GB
    }

    public class FileSize
    {
        public double SizeByByte { get; set; }

        public double SizeByUnit => CalculateSizeByUnit();
        
        public static SizeUnit Unit { get; set; }

        public FileSize(double initialSizeByByte)
        {
            SizeByByte = initialSizeByByte;
        }

        private double CalculateSizeByUnit()
            => SizeByByte / Math.Pow(1024, (int)Unit);
    }
}
