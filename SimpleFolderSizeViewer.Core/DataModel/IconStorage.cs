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
            Save(new DirectoryInfo(Environment.CurrentDirectory));
        }

        public Icon Select(FileSystemInfo fileSystemInfo)
        {
            if (!HasIcon(fileSystemInfo.Extension))
            {
                Save(fileSystemInfo);
            }

            return iconByExtension[fileSystemInfo.Extension];
        }

        private void Save(FileSystemInfo fileSystemInfo)
        {
            iconByExtension[fileSystemInfo.Extension] = IconExtractor.Extract(fileSystemInfo.FullName);
        }

        private bool HasIcon(string extension)
        {
            return iconByExtension.ContainsKey(extension);
        }
    }
}
