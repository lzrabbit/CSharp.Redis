using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSharp.Redis
{
    /// <summary>
    /// 存储数据到用户目录
    /// </summary>
    public static class LocalApplicationData
    {
        /// <summary>
        /// 存储数据到用户目录指定文件
        /// </summary>
        /// <param name="path">文件相对路径</param>
        /// <param name="contents">要保存的内容</param>
        /// <param name="append">是否为追加模式</param>
        public static void Save(string path, string contents, bool append)
        {
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), path);
            var dir = Path.GetDirectoryName(path);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            if (append) File.AppendAllText(path, contents);
            else File.WriteAllText(path, contents);
        }

        /// <summary>
        /// 从用户目录加载指定文件
        /// </summary>
        /// <param name="path">文件相对路径</param>
        /// <returns></returns>
        public static string[] Load(string path)
        {
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), path);
            return File.ReadAllLines(path);
        }
    }
}
