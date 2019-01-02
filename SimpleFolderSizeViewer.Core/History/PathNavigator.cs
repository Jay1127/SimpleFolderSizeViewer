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
        /// <summary>
        /// 현재 폴더의 위치(인덱스)
        /// </summary>
        private int _cursor;

        /// <summary>
        /// 폴더 이동 경로
        /// </summary>
        private List<Folder> _path;

        /// <summary>
        /// 현재 지정된 폴더
        /// </summary>
        public Folder Current
        {
            get => _path[_cursor];
        }

        public PathNavigator(Folder root)
        {
            _path = new List<Folder>() { root };
            _cursor = -1;
        }

        public void AddPath(Folder item)
        {
            // datagrid에서 바로 선택된 경우는 자식이 선택된 걸 보장함.
            _cursor++;
            _path.Add(item);
        }

        public void MakeNewPath(Folder item)
        {
            // treeview에서 아이템 선택된 경우는 자식이 선택된 걸 보장 못함.

            bool isPathDeleted = !Current.SubFolders.Contains(item) || CanMoveNext();
            if (isPathDeleted)
            {
                DeleteAllFromNextItem();
            }

            AddPath(item);
        }

        public void MoveNext()
        {
            if (!CanMoveNext()) return;

            _cursor++;
        }

        public void MovePrev()
        {
            if (!CanMovePrev()) return;

            _cursor--;
        }

        public bool CanMoveNext() => _cursor != _path.Count - 1;

        public bool CanMovePrev() => _cursor > 0;        

        public void Clear()
        {
            _path.Clear();
            _cursor = -1;
        }

        private void DeleteAllFromNextItem()
        {
            _path.RemoveRange(_cursor + 1, _path.Count - _cursor - 1);
        }
    }
}
