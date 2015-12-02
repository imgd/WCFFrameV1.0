﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Extensions.FileExtension;
using Extensions.ConvertExtension;
using Extensions.StringExtension;
using WCF.Common.Tools;


namespace Extensions.LogExtension
{
    //---------------------------------------------------------------
    //   日志 扩展方法类                                                   
    // —————————————————————————————————————————————————             
    // | varsion 1.0                                   |             
    // | creat by gd 2014.7.31                         |
    // | edit2014.12.24 添加 memory cache               |
    // | 联系我:@大白2013 http://weibo.com/u/2239977692  |            
    // —————————————————————————————————————————————————             
    //                                                               
    // *使用说明：                                                    
    //    使用当前扩展类添加引用: using Extensions.LogExtension;                      
    //    使用所有扩展类添加引用: using Extensions;                         
    // -------------------------------------------------------------- 
    public static class LogExtension
    {
        private static readonly string LogBasePath = ConfigHelper.GetAppSettingsString("LogPath");

        /// <summary>
        /// 写入日志到某个目录
        /// </summary>
        /// <param name="file_Url">目录路径 开头要加 ~/  结尾加/</param>
        /// <param name="Write_Path">文件路径名称 如a.txt</param>
        /// <param name="Write_Msg">要写入的信息</param>
        public static void WriteNoteBook(this string write_Msg, string file_Url, string write_Path)
        {
            //默认根路径
            if (file_Url.IsNullOrEnpty())
            {
                file_Url = LogBasePath;
            }

            //默认日志格式文件
            if (write_Path.IsNullOrEnpty())
            {
                write_Path = DateTime.Now.Year.ToString("yyyyMMdd") + ".log";
            }

            //物理根路径
            string base_Url = file_Url.GetMapPath();
            //写入路径
            string path = (file_Url + write_Path).GetMapPath();
            //创建文件夹
            base_Url.CreateFile();
            //写入日志信息
            write_Msg.WriteStream(path);
        }

        /// <summary>
        /// 订单相关写入日志
        /// </summary>
        /// <param name="logStr"></param>
        /// <param name="logPath"></param>
        public static void WriteLog(this string logStr)
        {
            string baseLogUrl = string.Format("{0}{1}/{2}/",
                LogBasePath, "Order",
                DateTime.Now.ToString("yyyyMM"));

            string writeOrderPath = DateTime.Now.ToString("yyyyMMdd") + ".log";

            logStr.WriteNoteBook(baseLogUrl, writeOrderPath);
        }
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="logStr"></param>
        /// <param name="logType"></param>
        public static void WriteLog(this string logStr, string logType)
        {
            string baseLogUrl = string.Format("{0}{1}/{2}/",
                LogBasePath, logType,
                DateTime.Now.ToString("yyyyMM"));

            string writeOrderPath = DateTime.Now.ToString("yyyyMMdd") + ".log";

            logStr.WriteNoteBook(baseLogUrl, writeOrderPath);
        }

    }
}
