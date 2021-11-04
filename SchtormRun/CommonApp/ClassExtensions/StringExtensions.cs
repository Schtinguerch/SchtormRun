using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SchtormRun
{
    public static class StringExtensions
    {
        public static Uri RelativeToAppUri(this string path) =>
            new Uri(string.Format("pack://application:,,,/{0};component/{1}",
                Assembly.GetExecutingAssembly().GetName().Name,
                path));

        public static string Preprocessed(this string code)
        {
            foreach (var replacement in CenterNode.PreprocessorReplacement)
                code = code.Replace(replacement.Key, replacement.Value);

            return code;
        }

        public static ImageSource GetIcon(this string filePath, bool isSmallIcon = true)
        {
            Interop.SHFILEINFO info = new Interop.SHFILEINFO(true);
            int cbFileInfo = Marshal.SizeOf(info);

            Interop.SHGFI flags = Interop.SHGFI.Icon 
                | (isSmallIcon? Interop.SHGFI.SmallIcon : Interop.SHGFI.LargeIcon) 
                | Interop.SHGFI.UseFileAttributes;

            Interop.SHGetFileInfo(filePath, 256, out info, (uint)cbFileInfo, flags);
            IntPtr iconHandle = info.hIcon;

            ImageSource iconSource = 
                Imaging.CreateBitmapSourceFromHIcon(
                    iconHandle,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

            Interop.DestroyIcon(iconHandle);
            return iconSource;
        }
    }
}
