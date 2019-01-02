using SimpleFolderSizeViewer.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core
{
    public class FolderSizeBuilder
    {
        public static void Build(Folder root)
        {
            foreach (var subFolder in root.SubFolders)
            {
                Build(subFolder);
                root.Size.SizeByByte += subFolder.Size.SizeByByte;
            }
        }
    }
}
