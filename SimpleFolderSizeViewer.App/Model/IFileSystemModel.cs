using SimpleFolderSizeViewer.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.Model
{
    public interface IFileSystemModel<out T> : IDisposable where T : FileSystemEntity
    {
        T Entity { get; }
        FolderModel Parent { get; }
        FileSize FileSize { get; }
    }
}
