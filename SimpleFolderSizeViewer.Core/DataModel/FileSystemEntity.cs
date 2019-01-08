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
        public Icon Icon { get => IconStorage.Instance.Select(this); }
        public double Size { get; set; }

        public FileSystemEntity(FileSystemInfo fileSystemInfo, double sizeByByte = 0)
        {
            CreationTime = fileSystemInfo.CreationTime;
            Extension = GetExtension(fileSystemInfo);
            LastModifiedTime = fileSystemInfo.LastWriteTime;
            Name = fileSystemInfo.Name;
            Path = fileSystemInfo.FullName;
            Size = sizeByByte;
        }

        public void Dispose()
        {            
            Icon?.Dispose();
        }

        private string GetExtension(FileSystemInfo fileSystemInfo)
        {
            if (!string.IsNullOrEmpty(fileSystemInfo.Extension))
            {
                return fileSystemInfo.Extension;
            }
            else if (IsDrive(fileSystemInfo))
            {
                return ".drive";
            }
            else if (IsDirectory(fileSystemInfo))
            {
                return ".folder";
            }
            else if (IsFileWithEmptyExtension(fileSystemInfo))
            {
                return ".file";
            }
            else
            {
                return fileSystemInfo.Extension;
            }
        }

        private bool IsDrive(FileSystemInfo fileSystemInfo)
        {
            return DriveInfo.GetDrives().Select(drive => drive.RootDirectory.FullName)
                                        .Contains(fileSystemInfo.FullName);
        }

        private bool IsFileWithEmptyExtension(FileSystemInfo fileSystemInfo)
        {
            return !IsDirectory(fileSystemInfo) && string.IsNullOrEmpty(fileSystemInfo.Extension);
        }

        private bool IsDirectory(FileSystemInfo fileSystemInfo)
        {
            return (fileSystemInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
        }
    }
}
