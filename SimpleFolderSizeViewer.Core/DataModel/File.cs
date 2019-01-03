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
        public File(string path, Folder parent) : this(new FileInfo(path), parent)
        {
        }

        public File(FileInfo fileInfo, Folder parent) : base(fileInfo, fileInfo.Length)
        {
        }
    }
}
