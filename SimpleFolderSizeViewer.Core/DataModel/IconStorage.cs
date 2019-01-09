using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core.DataModel
{
    class IconStorage
    {
        private static readonly IconStorage instance = new IconStorage();
        public static IconStorage Instance
        {
            get
            {
                return instance;
            }
        }

        private readonly Dictionary<string, Icon> iconByExtension;

        private IconStorage()
        {
            iconByExtension = new Dictionary<string, Icon>();
        }

        public Icon Select(FileSystemInfo fileSystemInfo)
        {
            if (!HasIcon(fileSystemInfo.Extension))
            {
                Save(fileSystemInfo);
            }

            return iconByExtension[fileSystemInfo.Extension];
        }

        public Icon Select(FileSystemEntity fileSystemEntity)
        {
            if (!HasIcon(fileSystemEntity.Extension))
            {
                Save(fileSystemEntity);
            }
            return iconByExtension[fileSystemEntity.Extension];
        }

        private void Save(FileSystemInfo fileSystemInfo)
        {
            iconByExtension[fileSystemInfo.Extension] = IconExtractor.Extract(fileSystemInfo.FullName);
        }

        private void Save(FileSystemEntity fileSystemEntity)
        {
            iconByExtension[fileSystemEntity.Extension] = IconExtractor.Extract(fileSystemEntity.Path);
        }

        private bool HasIcon(string extension)
        {
            return iconByExtension.ContainsKey(extension);
        }
    }
}
