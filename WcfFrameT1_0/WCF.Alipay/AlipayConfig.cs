using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCF.Common;
using WCF.Common.Tools;

namespace WCF.Alipay
{
    public static class AlipayConfig
    {
       
        public static readonly string WriteLogUrl = ConfigHelper.GetAppSettingsString("LogOrderPath");
        public static readonly string WriteLogPath = ConfigHelper.GetAppSettingsString("LogAlipayErrorLogPath");
        public static readonly string BaseDomain = ConfigHelper.GetAppSettingsString("PayDomain").ToString();

        /// <summary>
        /// 格式转化 成网站格式 23.00
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static decimal MoneyFormatDco(string money)
        {
            string moneyFormat = "0";
            double resu = 0;
            if (double.TryParse(money, out resu))
            {
                moneyFormat = Math.Round((resu / 100), 2).ToString().Trim();
            };
            return Convert.ToDecimal(moneyFormat);
        }
        /// <summary>
        /// 格式转化 成银联格式 2300
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string MoneyFormatEco(string money)
        {
            string moneyFormat = "0";
            double resu = 0;
            if (double.TryParse(money, out resu))
            {
                moneyFormat = (resu * 100).ToString().Trim();
            };
            return moneyFormat;
        }
    }
}
