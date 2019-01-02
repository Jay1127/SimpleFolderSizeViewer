using SimpleFolderSizeViewer.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core
{
    public class PathNavigator
    {
        private int _cursor;
        private List<Folder> _path;

        public PathNavigator()
        {
            _path = new List<Folder>();
            _cursor = -1;
        }

        public void AddPath(Folder item)
        {
            
        }


        public void Clear()
        {
            _path.Clear();
            _cursor = -1;
        }
    }
}
