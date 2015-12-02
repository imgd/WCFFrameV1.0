using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sucool.InternetFace.Alipay.YB.Util
{
    public static class YBPAY
    {
        public static string CreatePayByPostHTML(string data, string encryptkey, string merchantaccount,bool isMobile)
        {
            StringBuilder writer = new StringBuilder();
            writer.Append(" <!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"> ");
            writer.Append(" <html xmlns=\"http://www.w3.org/1999/xhtml\" > ");
            writer.Append(" <head runat=\"server\"><title>易宝支付</title> ");
            writer.Append(" <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\"/> ");
            writer.Append(" </head><body> ");
            writer.AppendFormat(" <form id=\"formpay\" method=\"get\" action=\"{0}\"> ", isMobile ? (APIURLConfig.payMobilePrefix+APIURLConfig.webpayURI) : (APIURLConfig.payWebPrefix + APIURLConfig.pcwebURI));

            writer.AppendFormat(" <input type=\"text\" name=\"data\" style=\"display:none;\" value=\"{0}\"/> ",data);
            writer.AppendFormat(" <input type=\"text\" name=\"encryptkey\" style=\"display:none;\" value=\"{0}\"/> ", encryptkey);
            writer.AppendFormat(" <input type=\"text\" name=\"merchantaccount\" style=\"display:none;\" value=\"{0}\"/> ", merchantaccount);

            writer.Append(" </form> ");
            writer.Append(" <script type=\"text/javascript\"> document.getElementById(\"formpay\").submit();</script> ");
            writer.Append(" </body></html>");
            return writer.ToString();
        }
    }
}
