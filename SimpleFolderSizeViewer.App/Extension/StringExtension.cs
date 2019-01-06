using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.App.Extension
{
    static class StringExtension
    {
        /// <summary>
        /// 문자열이 특정값으로 설정된 경우 true, 빈 값이나 공백 값인 경우 false 반환
        /// </summary>
        /// <param name="content">검사할 문자열</param>
        /// <returns></returns>
        public static bool HasAnyCharacter(this string content)
        {
            return !string.IsNullOrEmpty(content) && !string.IsNullOrWhiteSpace(content);
        }
    }
}
