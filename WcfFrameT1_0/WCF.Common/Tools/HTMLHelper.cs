using System;
using System.Text;
using System.Text.RegularExpressions;


namespace WCF.Common.Tools
{
    /// <summary>
    /// HTML代码相关处理
    /// </summary>
    public class HTMLHelper
    {
        /// <summary>
        /// 转换为Html格式，\r\n将会转会为html的回车符
        /// </summary>
        /// <param name="originalText">原始文本</param>
        /// <returns>Html格式的文本</returns>
        public static string Encode(string originalText, params HTMLHelper_EncodeAndDecodeType[] encodeTypes)
        {
            if (originalText == null || originalText == "")
            {
                return "";
            }

            for (int i = 0; i < encodeTypes.Length; i++)
            {
                switch (encodeTypes[i])
                {
                    case HTMLHelper_EncodeAndDecodeType.LT:
                        originalText = originalText.Replace("<", "&lt;");
                        break;
                    case HTMLHelper_EncodeAndDecodeType.GT:
                        originalText = originalText.Replace(">", "&gt;");
                        break;
                    case HTMLHelper_EncodeAndDecodeType.NBSP:
                        originalText = originalText.Replace(" ", "&nbsp;");
                        break;
                    case HTMLHelper_EncodeAndDecodeType.QUOT:
                        originalText = originalText.Replace("\"", "&quot;");
                        originalText = originalText.Replace("\'", "&quot;");
                        break;
                    case HTMLHelper_EncodeAndDecodeType.BR:
                        originalText = originalText.Replace("\r\n", "<br />");
                        originalText = originalText.Replace("\r", "<br />");
                        originalText = originalText.Replace("\n", "<br />");
                        break;
                    case HTMLHelper_EncodeAndDecodeType.AMP:
                        originalText = originalText.Replace("&", "&amp;");
                        break;
                }
            }

            return originalText;
        }

        /// <summary>
        /// 还原加密的HTML
        /// </summary>
        /// <param name="originalHtml">加密的过HTML</param>
        /// <returns>还原加密的HTML</returns>
        public static string Decode(string orginalHtml, params HTMLHelper_EncodeAndDecodeType[] encodeTypes)
        {
            if (orginalHtml == null || orginalHtml == "")
            {
                return "";
            }

            for (int i = 0; i < encodeTypes.Length; i++)
            {
                switch (encodeTypes[i])
                {
                    case HTMLHelper_EncodeAndDecodeType.LT:
                        orginalHtml = orginalHtml.Replace("&lt;", "<");
                        break;
                    case HTMLHelper_EncodeAndDecodeType.GT:
                        orginalHtml = orginalHtml.Replace("&gt;", ">");
                        break;
                    case HTMLHelper_EncodeAndDecodeType.NBSP:
                        orginalHtml = orginalHtml.Replace("&nbsp;", " ");
                        break;
                    case HTMLHelper_EncodeAndDecodeType.QUOT:
                        orginalHtml = orginalHtml.Replace("&quot;", "\"");
                        break;
                    case HTMLHelper_EncodeAndDecodeType.BR:
                        orginalHtml = orginalHtml.Replace("<br />", "\r\n");
                        break;
                    case HTMLHelper_EncodeAndDecodeType.AMP:
                        orginalHtml = orginalHtml.Replace("&amp;", "&");
                        break;
                }
            }

            return orginalHtml;
        }



        /// <summary>
        /// 创建CSS标签
        /// </summary>
        /// <param name="cssUrl">css文件路径(站点路径)</param>
        /// <returns>CSS标签文本(默认为Domains.Css主机)</returns>
        public static string CreateCssNode(string cssUrl)
        {
            return "<link href=\"" + DomainsHelper.Css + cssUrl + "\" rel=\"stylesheet\" type=\"text/css\"/>\r\n";
        }

        /// <summary>
        /// 创建JS标签
        /// </summary>
        /// <param name="jsUrl">js文件路径(站点路径)</param>
        /// <returns>JS标签文本(默认为Domains.Js主机)</returns>
        public static string CreateScriptNode(string jsUrl)
        {
            return "<script type=\"text/javascript\" src=\"" + DomainsHelper.Js + jsUrl + "\"></script>\r\n";
        }

        /// <summary>
        /// 创建CSS标签,自定义主机
        /// </summary>
        /// <param name="cssUrl">Css文件路劲(站点路径)</param>
        /// <param name="domain">主机域名</param>
        /// <returns>给定主机路径下的Css文件标签</returns>
        public static string CreateCssNode(string cssUrl, string domain)
        {
            return "<link href=\"" + domain + cssUrl + "\" rel=\"stylesheet\" type=\"text/css\"/>\r\n";
        }

        /// <summary>
        /// 创建Js标签,自定义主机
        /// </summary>
        /// <param name="jsUrl">js文件路径(站点路径)</param>
        /// <param name="domain">主机域名</param>
        /// <returns>给定主机路径下的js文件标签</returns>
        public static string CreateScriptNode(string jsUrl, string domain)
        {
            return "<script type=\"text/javascript\" src=\"" + domain + jsUrl + "\"></script>\r\n";
        }

        /// <summary>
        /// HTMLHelper辅助类中 Encode和 Decode方法的类型
        /// </summary>
        public enum HTMLHelper_EncodeAndDecodeType
        {
            /// <summary>
            /// 左尖括号
            /// </summary>
            LT,
            /// <summary>
            /// 右尖括号
            /// </summary>
            GT,
            /// <summary>
            /// &符号
            /// </summary>
            AMP,
            /// <summary>
            /// 空格符号
            /// </summary>
            NBSP,
            /// <summary>
            /// 引号(单引号与双引号)
            /// </summary>
            QUOT,
            /// <summary>
            /// 换行符号
            /// </summary>
            BR
        }

    }
}
