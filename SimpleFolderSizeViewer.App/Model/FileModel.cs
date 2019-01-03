using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleFolderSizeViewer.Core.DataModel;

namespace SimpleFolderSizeViewer.App.Model
{
    public class FileModel : FileSystemBaseModel<File>
    {
        public FileModel(File file) : base(file)
        {
        }
    }
}
