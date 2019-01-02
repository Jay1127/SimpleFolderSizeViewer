using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFolderSizeViewer.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// reference by https://github.com/kpym/tikzedt/blob/master/Components/FileListView/FileListView/IconExtractor.cs
    /// </remarks>
    class IconExtractor
    {
        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_LARGEICON = 0x0;
        private const uint SHGFI_SMALLICON = 0x1;
        private const uint SHGFI_OPENICON = 0x2;

        /// <summary>
        /// Get File or Folder Icon
        /// </summary>
        /// <param name="path"></param>
        public static Icon Extract(string path)
        {
            SHFILEINFO shFileInfo = new SHFILEINFO();
            IntPtr hIcon = SHGetFileInfo(path, 0, ref shFileInfo, (uint)(Marshal.SizeOf(shFileInfo)), SHGFI_SMALLICON | SHGFI_ICON);

            Icon icon = Icon.FromHandle(shFileInfo.HIcon).Clone() as Icon;
            DestroyIcon(shFileInfo.HIcon);
            return icon;
        }

        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath,
                                                   uint dwFileAttributes,
                                                   ref SHFILEINFO psfi,
                                                   uint cbSizeFileInfo,
                                                   uint uFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyIcon(IntPtr hIcon);

        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr HIcon;
            public IntPtr IIcon;
            public uint DwAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string SzDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string SzTypeName;
        }
    }
}