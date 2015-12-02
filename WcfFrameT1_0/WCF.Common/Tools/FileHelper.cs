using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace WCF.Common.Tools
{
    /// <summary>
    /// 物理文件操作类
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// 创建图片名称
        /// </summary>
        /// <param name="extension">扩展名(可选,默认为rar)</param>
        /// <returns></returns>
        public static string CreateFileName(string extension = ".rar")
        {
            return Guid.NewGuid() + extension;
        }

        /// <summary>
        /// 返回文件扩展名，不含“.”
        /// </summary>
        /// <param name="_filepath">文件全名称</param>
        /// <returns>string</returns>
        public static string GetFileExt(string _filepath)
        {
            if (string.IsNullOrEmpty(_filepath))
            {
                return "";
            }
            if (_filepath.LastIndexOf(".") > 0)
            {
                return _filepath.Substring(_filepath.LastIndexOf(".") + 1); //文件扩展名，不含“.”
            }
            return "";
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        /// <summary>
        /// 复制文件夹
        /// </summary>
        /// <param name="valueFromDirectory"></param>
        /// <param name="valueToDirectory"></param>
        public static void CopyFiles(string valueFromDirectory, string valueToDirectory)
        {
            //创建目标文件夹
            Directory.CreateDirectory(valueToDirectory);
            if (!Directory.Exists(valueFromDirectory))
                return;
            string[] directories = Directory.GetDirectories(valueFromDirectory);
            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    CopyFiles(d, valueToDirectory + d.Substring(d.LastIndexOf("\\")));
                }
            }

            string[] files = Directory.GetFiles(valueFromDirectory);
            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Copy(s, valueToDirectory + s.Substring(s.LastIndexOf("\\")), true);
                }
            }
        }

        /// <summary>
        /// 删除指定文件夹对应其他文件夹里的文件
        /// </summary>
        /// <param name="varFromDirectory"></param>
        /// <param name="varToDirectory"></param>
        public static void DeleteFiles(string varFromDirectory, string varToDirectory)
        {
            Directory.CreateDirectory(varToDirectory);

            if (!Directory.Exists(varFromDirectory)) return;

            string[] directories = Directory.GetDirectories(varFromDirectory);

            if (directories.Length > 0)
            {
                foreach (string d in directories)
                {
                    DeleteFiles(d, varToDirectory + d.Substring(d.LastIndexOf("\\")));
                }
            }

            string[] files = Directory.GetFiles(varFromDirectory);

            if (files.Length > 0)
            {
                foreach (string s in files)
                {
                    File.Delete(varToDirectory + s.Substring(s.LastIndexOf("\\")));
                }
            }
        }

        /// <summary>
        /// 删除指定目录下的所有指定文件类型
        /// </summary>
        /// <param name="path">文件存放的文件夹（绝对路径）</param>
        /// <param name="pattern">文件名  例如"*.txt", "*.jpeg","aaa.jpeg"</param>
        public static void DeleteOneTypeFiles(string path, string fileType)
        {
            //文件名数组
            string[] strFileName = Directory.GetFiles(path, fileType);
            if (strFileName != null)
            {
                foreach (var item in strFileName)
                {
                    File.Delete(item);
                }
            }
        }

        /// <summary>
        /// 判断文件是文件夹是否存在，如果不存在就新创建文件
        /// 传入值必须为物理路径
        /// </summary>
        /// <param name="folder"></param>
        public static void CreateDir(string folder)
        {
            try
            {
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
            }
            catch
            { }
        }
    }

}
