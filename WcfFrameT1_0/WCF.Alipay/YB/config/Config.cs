using System;
using System.Collections.Generic;
using System.Text;

public class Config
{
    
        static Config()
        {
            //商户账户编号
            merchantAccount = "10012438072";
            //merchantAccount = "YB01000000144";

            //商户RSA密钥对——公钥，(请见“RSA密钥对生成说明.txt”)，该公钥需要在商户后台向易宝支付报备
            //商户后台(测试环境http://mobiletest.yeepay.com/merchant,正式环境http://www.yeepay.com)
            merchantPublickey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC/vC1WXlqdqHMe5bhslO3GXb/cYyO+/4aog/gEaa2/Z9J6uFk1ZulgwdM64hzTekTR5pVax625mAsSCGVVFq7PVTPMipuAupwn/CoHkPtiI+qGfhhAR8lRAD19TqepgFCLrAqxwzQ1+5EcvExdL4OXa1UUP+58mWuYkzTuUIkEcwIDAQAB";
            //merchantPublickey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDxhOgx8q1FAIKnqf6BqjCLKyXXSTRSNSfwS9Nc6E2ffmIpbieyN7mB7XqQKY/icnOB34vtPAjEmQUx4uc1h5R0ApdFm3RJEsWokV/beGjEtd1i2EoSgYwGSXaC32ExpcmsPrZu1hvzEflVmpJD19KcXxvnbmQEHiA6AS1Xy/vooQIDAQAB";

            //商户RSA密钥对——私钥，(请见“RSA密钥对生成说明.txt”)
            merchantPrivatekey = "MIICdgIBADANBgkqhkiG9w0BAQEFAASCAmAwggJcAgEAAoGBAL+8LVZeWp2ocx7luGyU7cZdv9xjI77/hqiD+ARprb9n0nq4WTVm6WDB0zriHNN6RNHmlVrHrbmYCxIIZVUWrs9VM8yKm4C6nCf8KgeQ+2Ij6oZ+GEBHyVEAPX1Op6mAUIusCrHDNDX7kRy8TF0vg5drVRQ/7nyZa5iTNO5QiQRzAgMBAAECgYAvdMcP8oihLxlXU2qDTZVQnNGpHcyyMJLG0OspRHLhxjO9djV2V0N6VF8Q2vhhi4jPHzBmaLfiEPTkgLl8BwI7Fznooyq6yOXQmxSJMqc3S6Td7AuGnvrg62H1c75gu+/q87+9jq+OZxVjcu/Fih06VGFX04kWj7uV4W57WIwlgQJBAOnFwmDlNXcdHOvOnC0gRjdhUcc/8YsR1nKc4EH1VM64mSOxBi0PLt+P6TDWvkwueHnzXnfayCU2xZ6jX34hyTMCQQDR9zAByyiYsaHk7DsYGIB9OQ5RaCp2TiazBFOq/ivwiaaQMuOXEvCwV9YQEnyANZjhgpi2h1qcGdfIzKIrkFfBAkAm+hkZrL3IWWtMCcvSXlI2w5wt+4RbaqL1wyBE/xGf0fl+kPJ1qtVm4wi/Yt6htxHRS3mRxEGgqswyUg0G670vAkEAtLInxZNil65fppTK8py7j4kX0mV0DaaKVYwGuWTOyc6c4wJ4rV5md0zQc7qFHQ2DSahL5uIrz6XJ+AxsEFPDwQJALfgNqbz+tsD4HUtC5Ul9AVCX8zQ6r0cltw7oACcGW7vEEhFjCVyyC70H41yY/0XB3KTJEZ5tXNMxlHz5WNjXqQ==";
            //merchantPrivatekey = "MIICdQIBADANBgkqhkiG9w0BAQEFAASCAl8wggJbAgEAAoGBAPGE6DHyrUUAgqep/oGqMIsrJddJNFI1J/BL01zoTZ9+YiluJ7I3uYHtepApj+Jyc4Hfi+08CMSZBTHi5zWHlHQCl0WbdEkSxaiRX9t4aMS13WLYShKBjAZJdoLfYTGlyaw+tm7WG/MR+VWakkPX0pxfG+duZAQeIDoBLVfL++ihAgMBAAECgYAw2urBV862+5BybA/AmPWy4SqJbxR3YKtQj3YVACTbk4w1x0OeaGlNIAW/7bheXTqCVf8PISrA4hdL7RNKH7/mhxoX3sDuCO5nsI4Dj5xF24CymFaSRmvbiKU0Ylso2xAWDZqEs4Le/eDZKSy4LfXA17mxHpMBkzQffDMtiAGBpQJBAPn3mcAwZwzS4wjXldJ+Zoa5pwu1ZRH9fGNYkvhMTp9I9cf3wqJUN+fVPC6TIgLWyDf88XgFfjilNKNz0c/aGGcCQQD3WRxwots1lDcUhS4dpOYYnN3moKNgB07Hkpxkm+bw7xvjjHqI8q/4Jiou16eQURG+hlBZlZz37Y7P+PHF2XG3AkAyng/1WhfUAfRVewpsuIncaEXKWi4gSXthxrLkMteM68JRfvtb0cAMYyKvr72oY4Phyoe/LSWVJOcW3kIzW8+rAkBWekhQNRARBnXPbdS2to1f85A9btJP454udlrJbhxrBh4pC1dYBAlz59v9rpY+Ban/g7QZ7g4IPH0exzm4Y5K3AkBjEVxIKzb2sPDe34Aa6Qd/p6YpG9G6ND0afY+m5phBhH+rNkfYFkr98cBqjDm6NFhT7+CmRrF903gDQZmxCspY";

            //易宝支付分配的公钥，该公钥由商户进入商户后台先上报自己的公钥再获取，商户后台目录为（产品管理——RSA公钥管理）
            //商户后台(测试环境http://mobiletest.yeepay.com/merchant,正式环境http://www.yeepay.com)
            yibaoPublickey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCO643XNKMwj24j0F5f9DmpWc70Xuth1nE5fVLGKXJSNrZuYjiJZ6XW5mdIS3qlNE5dy5iAb9MUuyWRUIa3eB6mLH8LQ5JOYBbdqm14IkVUeDv1UqZhLf1sMDfLjUXUOAx4EiOHgtNPmZV/tyJ0inxIXau+7frLImw3lFKDQs0SEQIDAQAB";
            //yibaoPublickey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCxnYJL7fH7DVsS920LOqCu8ZzebCc78MMGImzW8MaP/cmBGd57Cw7aRTmdJxFD6jj6lrSfprXIcT7ZXoGL5EYxWUTQGRsl4HZsr1AlaOKxT5UnsuEhA/K1dN1eA4lBpNCRHf9+XDlmqVBUguhNzy6nfNjb2aGE+hkxPP99I1iMlQIDAQAB";
        }

        public static string merchantAccount
        { get; set; }

        public static string merchantPublickey
        { get; set; }

        public static string merchantPrivatekey
        { get; set; }

        public static string yibaoPublickey
        { get; set; }
    }

