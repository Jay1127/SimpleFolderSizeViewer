using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core.DataModel
{
    /// <summary>
    /// 파일 표시 단위
    /// </summary>
    public enum SizeUnit
    {
        /// <summary>
        /// 바이트(기본값)
        /// </summary>
        B,

        /// <summary>
        /// 킬로 바이트
        /// </summary>
        KB,

        /// <summary>
        /// 메가 바이트
        /// </summary>
        MB,

        /// <summary>
        /// 기가바이트
        /// </summary>
        GB
    }

    /// <summary>
    /// 파일 사이즈를 표현
    /// </summary>
    public class FileSize
    {
        /// <summary>
        /// 파일의 길이(바이트)
        /// </summary>
        public double SizeByByte { get; set; }

        /// <summary>
        /// 파일의 단위가 반영된 길이
        /// </summary>
        public double SizeByUnit => CalculateSizeByUnit();
        
        /// <summary>
        /// 현재 적용된 파일 단위
        /// </summary>
        public static SizeUnit Unit { get; set; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="lengthByte">초기 파일 크기</param>
        public FileSize(double lengthByte)
        {
            SizeByByte = lengthByte;
        }

        /// <summary>
        /// 바이트를 단위가  적용된 크기로 반환
        /// </summary>
        /// <returns>단위가 적용된 길이</returns>
        private double CalculateSizeByUnit()
            => SizeByByte / Math.Pow(1024, (int)Unit);
    }
}
