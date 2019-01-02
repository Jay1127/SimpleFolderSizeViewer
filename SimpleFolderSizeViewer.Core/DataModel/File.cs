using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core.DataModel
{
    public class File : FileSystemEntity
    {
        public File(string path) : this(new FileInfo(path))
        {
        }

        public File(FileInfo fileInfo) : base(fileInfo, fileInfo.Length)
        {
            
        }
    }
}
