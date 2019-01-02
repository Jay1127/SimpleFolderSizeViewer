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
        public int SubFolderCount => SubFolders.Count;
        public int SubFileCount => SubFiles.Count;
        public int SubNodeCount => SubNodes.Count;

        public List<Folder> SubFolders { get; }
        public List<File> SubFiles { get; }
        public List<FileSystemEntity> SubNodes { get; }

        public Folder(string path) : this(new DirectoryInfo(path))
        {
        }

        public Folder(DirectoryInfo directoryInfo) : base(directoryInfo)
        {
            SubFolders = InitSubFolders(directoryInfo);
            SubFiles = directoryInfo.EnumerateFiles()
                                    .Select(d => new File(d))
                                    .ToList();

            Size = new FileSize(SubFiles.Sum(f => f.Size.SizeByByte));
        }

        private List<Folder> InitSubFolders(DirectoryInfo directoryInfo)
        {
            var subFolders = new List<Folder>();

            foreach (var dirInfo in directoryInfo.EnumerateDirectories())
            {
                try
                {
                    if (dirInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                    {
                        continue;
                    }

                    subFolders.Add(new Folder(dirInfo.FullName));
                }
                catch (UnauthorizedAccessException e)
                {
                }
            }

            return subFolders;
        }
    }
}
