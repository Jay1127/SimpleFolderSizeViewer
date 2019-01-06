using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.ViewModel
{
    interface IRequestCloseViewModel
    {
        event EventHandler RequestClose;
    }
}
