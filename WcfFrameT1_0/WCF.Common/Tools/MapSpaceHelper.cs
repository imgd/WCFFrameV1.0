using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCF.Common.Lib
{
    public static class MapSpaceHelper
    {
        private const double EARTH_RADIUS = 6378.137; //地球半径
        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000;
            return s * 1000;
        }
        
            /// <summary>
        /// 计算 当前用户地点 定点坐标的距离
        /// </summary>
        /// <param name="coordinate">经纬度坐标字符串</param>
        /// <param name="lat">当前用户纬度</param>
        /// <param name="lng">当前用户经度</param>
        /// <returns>
        /// 距离 单位/米
        /// </returns>
        public static double ComputZheSpace(string coordinate, double lat, double lng)
        {
            //计算距离 单位/米
            double space = 0;
            if (lat > 0 && lng > 0)
            {
                if (coordinate != "" && coordinate.Trim() != ",")
                {
                    string[] codarr = coordinate.Split(',');
                    //codarr 数组必须包含经度纬度 两个
                    if (codarr.Length == 2)
                    {
                        if (codarr[0] != "" && codarr[1] != "")
                        {
                            space = MapSpaceHelper.GetDistance(lat, lng, double.Parse(codarr[1]), double.Parse(codarr[0]));
                        }
                    }
                }
            }
            return space;        
        }
    }
}
