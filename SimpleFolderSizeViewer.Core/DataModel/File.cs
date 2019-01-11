using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core.DataModel
{
    /// <summary>
    /// 파일을 정의
    /// </summary>
    public class File : FileSystemEntity
    {
        /// <summary>
        /// 파일을 생성
        /// </summary>
        /// <param name="path">파일 전체 경로</param>
        /// <param name="parent">상위 폴더</param>
        public File(string path, Folder parent) : this(new FileInfo(path), parent)
        {
        }

        /// <summary>
        /// 파일을 생성
        /// </summary>
        /// <param name="fileInfo">파일 관련 정보</param>
        /// <param name="parent">상위 폴더</param>
        public File(FileInfo fileInfo, Folder parent) : base(fileInfo, fileInfo.Length)
        {
        }
    }
}
