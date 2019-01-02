using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core.DataModel
{
    public enum SizeUnit
    {
        Byte,
        KByte,
        MByte,
        GByte
    }

    public class FileSize
    {
        public double SizeByByte { get; set; }
        public double SizeByUnit
        {
            get
            {
                return CalculateSizeByUnit();
            }
        }

        public static SizeUnit Unit { get; set; }

        public FileSize(double sizeByByte = 0)
        {
            SizeByByte = sizeByByte;
        }

        private double CalculateSizeByUnit()
            => SizeByByte / Math.Pow(1024, (int)Unit);
    }
}
