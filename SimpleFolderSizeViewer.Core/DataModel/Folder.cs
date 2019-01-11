using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core.DataModel
{
    public class Folder : FileSystemEntity
    {
        public int SubFolderCount { get; set; }
        public int SubFileCount { get; set; }
        public int SubNodeCount { get => SubFolderCount + SubFileCount; }

        public List<Folder> SubFolders { get; }
        public List<File> SubFiles { get; }
        public List<FileSystemEntity> SubNodes { get; }

        public Folder(string path, Folder parent) : this(new DirectoryInfo(path), parent)
        {
        }

        public Folder(DirectoryInfo directoryInfo, Folder parent) : base(directoryInfo)
        {
            SubFolders = new List<Folder>();

            //SubFolders = InitSubFolders(directoryInfo);
            SubFiles = directoryInfo.EnumerateFiles()
                                    .Select(d => new File(d, this))
                                    .ToList();

            SubFileCount = SubFiles.Count;

            Size = new FileSize(SubFiles.Sum(f => f.Size.SizeByByte));
        }

        public void InitSubFolders()
        {
            ClearSubFolders();

            var directoryInfo = new DirectoryInfo(Path);

            foreach (var dirInfo in directoryInfo.EnumerateDirectories())
            {
                try
                {
                    if (dirInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                    {
                        continue;
                    }

                    SubFolders.Add(new Folder(dirInfo.FullName, this));
                    
                }
                catch (UnauthorizedAccessException e)
                {
                    ErrorLogRepository.Instance.ErrorList.Add(e.Message);
                }
            }

            SubFolderCount = SubFolders.Count;
        }

        public void ClearSubFolders()
        {
            ClearFileSystemEntities(SubFolders);
        }

        public void ClearSubFiles()
        {
            ClearFileSystemEntities(SubFiles);
        }

        private void ClearFileSystemEntities<T>(List<T> fileSystemEntities) where T : FileSystemEntity
        {
            fileSystemEntities.ForEach(entity =>
            {
                entity.Dispose();
            });

            fileSystemEntities.Clear();
        }
    }
}
