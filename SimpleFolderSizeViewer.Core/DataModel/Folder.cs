using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core.DataModel
{
    /// <summary>
    /// 폴더를 표현
    /// </summary>
    public class Folder : FileSystemEntity
    {
        /// <summary>
        /// 하위 폴더 수
        /// </summary>
        public int SubFolderCount { get; set; }

        /// <summary>
        /// 하위 파일 수
        /// </summary>
        public int SubFileCount { get; set; }

        /// <summary>
        /// 하위 폴더 및 파일 수
        /// </summary>
        public int SubNodeCount => SubFolderCount + SubFileCount;

        /// <summary>
        /// 하위 폴더
        /// </summary>
        public List<Folder> SubFolders { get; }

        /// <summary>
        /// 하위 파일
        /// </summary>
        public List<File> SubFiles { get; }

        /// <summary>
        /// 폴더를 생성
        /// </summary>
        /// <param name="path">전체 폴더 경로</param>
        /// <param name="parent">상위 폴더</param>
        public Folder(string path, Folder parent) : this(new DirectoryInfo(path), parent)
        {
        }

        /// <summary>
        /// 폴더를 생성
        /// </summary>
        /// <param name="directoryInfo">폴더 정보</param>
        /// <param name="parent">상위 폴더</param>
        public Folder(DirectoryInfo directoryInfo, Folder parent) : base(directoryInfo)
        {
            SubFolders = new List<Folder>();

            //SubFolders = InitSubFolders(directoryInfo);
            SubFiles = directoryInfo.EnumerateFiles()
                                    .Select(d => new File(d, this))
                                    .ToList();

            SubFileCount = SubFiles.Count;

            Size = new FileSize(SubFiles.Sum(f => f.Size.SizeByByte));
        }

        /// <summary>
        /// 하위폴더를 초기화함.
        /// </summary>
        public void InitSubFolders()
        {
            ClearSubFolders();

            var directoryInfo = new DirectoryInfo(Path);

            foreach (var dirInfo in directoryInfo.EnumerateDirectories())
            {
                try
                {
                    if (dirInfo.Attributes.HasFlag(FileAttributes.ReparsePoint))
                    {
                        continue;
                    }

                    SubFolders.Add(new Folder(dirInfo.FullName, this));
                    
                }
                catch (UnauthorizedAccessException e)
                {
                    ErrorLogRepository.Instance.ErrorList.Add(e.Message);
                }
            }

            SubFolderCount = SubFolders.Count;
        }

        /// <summary>
        /// 하위 폴더 리스트를 지움.
        /// </summary>
        public void ClearSubFolders()
        {
            ClearFileSystemEntities(SubFolders);
        }

        /// <summary>
        /// 하위 파일 리스트를 지움.
        /// </summary>
        public void ClearSubFiles()
        {
            ClearFileSystemEntities(SubFiles);
        }

        /// <summary>
        /// 하위 파일 또는 폴더의 자원을 해제하여 지움.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileSystemEntities"></param>
        private void ClearFileSystemEntities<T>(List<T> fileSystemEntities) where T : FileSystemEntity
        {
            fileSystemEntities.ForEach(entity =>
            {
                entity.Dispose();
            });

            fileSystemEntities.Clear();
        }
    }
}
