using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core
{
    /// <summary>
    /// 폴더 정보 확인시 권한에러에 대한 정보 저장 클래스
    /// </summary>
    public class ErrorLogRepository
    {
        private static readonly ErrorLogRepository instance = new ErrorLogRepository();

        /// <summary>
        /// 싱글톤 객체 반환
        /// </summary>
        public static ErrorLogRepository Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// 에러 리스트
        /// </summary>
        public List<string> ErrorList { get; }

        /// <summary>
        /// 생성자
        /// </summary>
        private ErrorLogRepository()
        {
            ErrorList = new List<string>();
        }
    }
}
