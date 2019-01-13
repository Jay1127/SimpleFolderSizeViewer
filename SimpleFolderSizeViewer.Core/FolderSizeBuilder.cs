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
    /// <summary>
    /// 폴더의 크기를 생성하는 클래스
    /// </summary>
    public class FolderSizeBuilder
    {
        public delegate void FolderSizeChangedHandler();

        /// <summary>
        /// 폴더 사이즈 변경된 경우 호출
        /// </summary>
        public event FolderSizeChangedHandler FolderSizeChanged;

        public FolderSizeBuilder()
        {

        }

        /// <summary>
        /// 폴더의 크기 생성
        /// </summary>
        /// <param name="root"></param>
        public void Build(Folder root)
        {
            root.InitSubFolders();

            foreach (var subFolder in root.SubFolders)
            {
                Build(subFolder);
                root.Size.SizeByByte += subFolder.Size.SizeByByte;
                root.SubFolderCount += subFolder.SubFolderCount;
                root.SubFileCount += subFolder.SubFileCount;
            }

            root.SetPercentToSubItems();

            FolderSizeChanged?.Invoke();
        }
    }
}
