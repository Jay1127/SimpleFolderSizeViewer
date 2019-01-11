using SimpleFolderSizeViewer.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core
{
    /// <summary>
    /// 폴더의 경로 탐색 클래스
    /// </summary>
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

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="root">루트 폴더</param>
        public PathNavigator(Folder root)
        {
            _path = new List<Folder>() { root };
            _cursor = -1;
        }

        /// <summary>
        /// 경로를 추가함
        /// </summary>
        /// <param name="item"></param>
        public void AddPath(Folder item)
        {
            // datagrid에서 바로 선택된 경우는 자식이 선택된 걸 보장함.
            _cursor++;
            _path.Add(item);
        }

        /// <summary>
        /// 새로운 경로를 생성함.
        /// </summary>
        /// <param name="item"></param>
        public void MakeNewPath(Folder item)
        {
            if (IsNewPathNeeded(item))
            {
                DeleteAllFromNextItem();
            }

            AddPath(item);
        }

        /// <summary>
        /// 다음 폴더로 이동
        /// </summary>
        public void MoveNext()
        {
            if (!CanMoveNext()) return;

            _cursor++;
        }

        /// <summary>
        /// 이전 폴더로 이동
        /// </summary>
        public void MovePrev()
        {
            if (!CanMovePrev()) return;

            _cursor--;
        }

        /// <summary>
        /// 다음 폴더로 이동할 수 있는지 판단.
        /// </summary>
        /// <returns></returns>
        public bool CanMoveNext() => _cursor != _path.Count - 1;

        /// <summary>
        /// 이전 폴더로 이동할 수 있는지 판단.
        /// </summary>
        /// <returns></returns>
        public bool CanMovePrev() => _cursor > 0;        

        /// <summary>
        /// 기존 경로를 모두 지음.
        /// </summary>
        public void Clear()
        {
            _path.Clear();
            _cursor = -1;
        }

        /// <summary>
        /// 다음 아이템부터 모두 삭제
        /// </summary>
        private void DeleteAllFromNextItem()
        {
            _path.RemoveRange(_cursor + 1, _path.Count - _cursor - 1);
        }

        /// <summary>
        /// 새로운 경로를 만들어야 하는지 판단
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool IsNewPathNeeded(Folder item)
        {
            return !Current.SubFolders.Contains(item) || CanMoveNext();
        }
    }
}
