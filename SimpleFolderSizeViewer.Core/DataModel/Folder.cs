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

        public Folder(string path, Folder parent) : this(new DirectoryInfo(path), parent)
        {
        }

        public Folder(DirectoryInfo directoryInfo, Folder parent) : base(directoryInfo)
        {
            SubFolders = InitSubFolders(directoryInfo);
            SubFiles = directoryInfo.EnumerateFiles()
                                    .Select(d => new File(d, this))
                                    .ToList();

            Size = SubFiles.Sum(f => f.Size);
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

                    subFolders.Add(new Folder(dirInfo.FullName, this));
                }
                catch (UnauthorizedAccessException e)
                {
                }
            }

            return subFolders;
        }
    }
}
