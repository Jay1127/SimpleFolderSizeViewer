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
    /// FileSystem객체 기본 클래스
    /// </summary>
    public class FileSystemEntity : IDisposable
    {
        /// <summary>
        /// 파일시스템 엔티티의 아이콘
        /// </summary>
        private Icon _icon;

        /// <summary>
        /// 생성 시간
        /// </summary>
        public DateTime CreationTime { get; }

        /// <summary>
        /// 확장자(폴더는 ".folder", 확장자 없는 파일은 ".file")
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// 마지막으로 수정된 시간
        /// </summary>
        public DateTime LastModifiedTime { get; }

        /// <summary>
        /// 파일시스템 엔티티의 이름(파일 또는 폴더명)
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 파일시스템 엔티티의 아이콘
        /// </summary>
        public Icon Icon
        {
            get
            {
                _icon = _icon ?? IconStorage.Instance.Select(this);
                return Icon;
            }
        }

        /// <summary>
        /// 파일시스템 엔티티의 상위 폴더
        /// </summary>
        public Folder Parent { get; }

        /// <summary>
        /// 파일시스템 엔티티의 전체 경로
        /// </summary>
        public string Path { get; }
        
        /// <summary>
        /// 파일시스템 엔티티의 크기
        /// </summary>
        public FileSize Size { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="fileSystemInfo">fileSystem 정보</param>
        /// <param name="sizeByByte">파일 크기</param>
        public FileSystemEntity(FileSystemInfo fileSystemInfo, double sizeByByte = 0)
        {
            CreationTime = fileSystemInfo.CreationTime;
            Extension = GetExtension(fileSystemInfo);
            LastModifiedTime = fileSystemInfo.LastWriteTime;
            Name = fileSystemInfo.Name;
            Path = fileSystemInfo.FullName;
            Size = new FileSize(sizeByByte);
        }

        /// <summary>
        /// 할당된 자원 해제(할당 자원 : icon)
        /// </summary>
        public void Dispose()
        {
            _icon?.Dispose();
        }

        /// <summary>
        /// 파일 정보에서 확장자를 추출함
        /// </summary>
        /// <param name="fileSystemInfo">파일 정보</param>
        /// <returns>파일 : ".확장자", 드라이브 : ".drive", 폴더 : ".folder", 확장자 없는 파일 : ".file"</returns>
        private string GetExtension(FileSystemInfo fileSystemInfo)
        {
            if (!string.IsNullOrEmpty(fileSystemInfo.Extension))
            {
                return fileSystemInfo.Extension;
            }
            if (IsDrive(fileSystemInfo))
            {
                return ".drive";
            }
            else if (IsDirectory(fileSystemInfo))
            {
                return ".folder";
            }
            else if (IsFileWithNoExtension(fileSystemInfo))
            {
                return ".file";
            }

            return string.Empty;
        }

        /// <summary>
        /// 파일 정보가 드라이브 인지 확인함.
        /// </summary>
        /// <param name="fileSystemInfo">파일 정보</param>
        /// <returns>드라이브인 경우 참 반환</returns>
        private bool IsDrive(FileSystemInfo fileSystemInfo)
        {
            return DriveInfo.GetDrives().Select(drive => drive.RootDirectory.FullName)
                                        .Contains(fileSystemInfo.FullName);
        }

        /// <summary>
        /// 확장자가 없는 파일인지 확인함.
        /// </summary>
        /// <param name="fileSystemInfo">파일 정보</param>
        /// <returns>확장자가 없고 파일인 경우에 참 반환</returns>
        private bool IsFileWithNoExtension(FileSystemInfo fileSystemInfo)
        {
            return !IsDirectory(fileSystemInfo) && string.IsNullOrEmpty(fileSystemInfo.Extension);
        }

        /// <summary>
        /// 파일 정보가 폴더인지 확인함.
        /// </summary>
        /// <param name="fileSystemInfo">파일 정보</param>
        /// <returns>폴더인 경우 참 반환</returns>
        private bool IsDirectory(FileSystemInfo fileSystemInfo)
        {
            return (fileSystemInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory;
        }
    }
}
