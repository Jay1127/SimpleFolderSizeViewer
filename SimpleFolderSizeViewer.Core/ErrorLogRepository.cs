using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core
{
    public class ErrorLogRepository
    {
        public static ErrorLogRepository Instance = new ErrorLogRepository();

        public List<string> ErrorList { get; }

        private ErrorLogRepository()
        {
            ErrorList = new List<string>();
        }
    }
}
