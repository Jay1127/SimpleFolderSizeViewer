using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core.DataModel
{
    public class FileSystemEntity : IDisposable
    {
        public DateTime CreationTime { get; }
        public string Extension { get; }
        public DateTime LastModifiedTime { get; }
        public string Name { get; }
        public Folder Parent { get; }
        public string Path { get; }
        public Icon Icon { get; }
        public FileSize Size { get; set; }

        public FileSystemEntity(FileSystemInfo fileSystemInfo, double sizeByByte = 0)
        {
            CreationTime = fileSystemInfo.CreationTime;
            Extension = fileSystemInfo.Extension;
            LastModifiedTime = fileSystemInfo.LastWriteTime;
            Name = fileSystemInfo.Name;
            Path = fileSystemInfo.FullName;
            Size = new FileSize(sizeByByte);            
            Icon = IconStorage.Instance.Select(fileSystemInfo);
        }

        public void Dispose()
        {            
            Icon?.Dispose();
        }
    }
}
