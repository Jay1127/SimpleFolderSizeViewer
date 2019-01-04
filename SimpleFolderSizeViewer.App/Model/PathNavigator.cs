using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.Model
{
    public class PathNavigator
    {
        private static readonly PathNavigator _instance = new PathNavigator();

        /// <summary>
        /// 현재 폴더의 위치(인덱스)
        /// </summary>
        private int _cursor;

        /// <summary>
        /// 폴더 이동 경로
        /// </summary>
        private List<FolderModel> _path;

        public static PathNavigator Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// 현재 지정된 폴더
        /// </summary>
        public FolderModel Current
        {
            get => _path[_cursor];
        }

        private PathNavigator()
        {
            _path = new List<FolderModel>();
            _cursor = -1;
        }

        public void AddPath(FolderModel item)
        {
            // datagrid에서 바로 선택된 경우는 자식이 선택된 걸 보장함.
            _cursor++;
            _path.Add(item);
            System.Diagnostics.Debug.Print(_cursor.ToString());
        }

        public void MakeNewPath(FolderModel item)
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
            System.Diagnostics.Debug.Print(_cursor.ToString());
        }

        public void MovePrev()
        {
            if (!CanMovePrev()) return;

            _cursor--;
            System.Diagnostics.Debug.Print(_cursor.ToString());
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
