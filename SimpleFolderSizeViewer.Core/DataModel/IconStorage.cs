using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core.DataModel
{
    /// <summary>
    /// 아이콘을 저장하는 클래스
    /// </summary>
    class IconStorage
    {
        private static readonly IconStorage instance = new IconStorage();

        /// <summary>
        /// 싱글톤 객체 반환
        /// </summary>
        public static IconStorage Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// 아이콘 저장 딕셔너리(키는 확장자)
        /// </summary>
        private readonly Dictionary<string, Icon> iconByExtension;

        /// <summary>
        /// 생성자
        /// </summary>
        private IconStorage()
        {
            iconByExtension = new Dictionary<string, Icon>();
        }

        /// <summary>
        /// 해당 확장자에 해당하는 아이콘을 가져옴.
        /// </summary>
        /// <param name="fileSystemEntity">파일 정보</param>
        /// <returns>아이콘</returns>
        public Icon Select(FileSystemEntity fileSystemEntity)
        {
            if (!HasIcon(fileSystemEntity.Extension))
            {
                Save(fileSystemEntity);
            }

            return iconByExtension[fileSystemEntity.Extension];
        }

        /// <summary>
        /// 아이콘을 저정함.
        /// </summary>
        /// <param name="fileSystemEntity"></param>
        private void Save(FileSystemEntity fileSystemEntity)
        {
            iconByExtension[fileSystemEntity.Extension] = IconExtractor.Extract(fileSystemEntity.Path);
        }

        /// <summary>
        /// 아이콘이 있는지 확인
        /// </summary>
        /// <param name="extension"></param>
        /// <returns></returns>
        private bool HasIcon(string extension)
        {
            return iconByExtension.ContainsKey(extension);
        }
    }
}
