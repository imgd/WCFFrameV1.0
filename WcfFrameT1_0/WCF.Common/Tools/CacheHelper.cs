using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace WCF.Common.Tools
{
    public static class CacheHelper
    {
        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        /// <returns></returns>
        public static object GetCache(string cacheKey)
        {
            return HttpRuntime.Cache[cacheKey];
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="cacheKey">缓存键</param>
        public static void RemoveCache(string cacheKey)
        {
            if (GetCache(cacheKey) != null)
            {
                HttpRuntime.Cache.Remove(cacheKey);
            }
        }
        /// <summary>
        /// 添加到缓存
        /// </summary>
        /// <param name="value">缓存值</param>
        /// <param name="cacheKey">缓存键</param>
        public static void InsertCache(object value, string cacheKey)
        {
            if (GetCache(cacheKey) == null)
            {
                HttpRuntime.Cache.Insert(cacheKey, value);
            }
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="value">缓存值</param>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="isReplace">缓存存在是否替换</param>
        public static void InsertCache(object value, string cacheKey, bool isReplace)
        {
            RemoveCache(cacheKey);
            InsertCache(value, cacheKey);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="value">缓存值</param>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="dependency">缓存依赖项(sql表依赖 请使用 SqlCacheDependency )</param>
        public static void InsertCache(object value, string cacheKey, CacheDependency dependency)
        {
            if (GetCache(cacheKey) == null)
            {
                HttpRuntime.Cache.Insert(cacheKey, value);
            }
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="value">缓存值</param>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="dependency">缓存依赖项(sql表依赖 请使用 SqlCacheDependency )</param>
        /// <param name="expriationTime">缓存滑动(连续访问时间)过期时间</param>
        public static void InsertCache(object value, string cacheKey, CacheDependency dependency, TimeSpan expriationTime)
        {
            if (GetCache(cacheKey) == null)
            {
                HttpRuntime.Cache.Insert(cacheKey, value, dependency, System.Web.Caching.Cache.NoAbsoluteExpiration, expriationTime);
            }
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="value">缓存值</param>
        /// <param name="cacheKey">缓存键</param>
        /// <param name="dependency">缓存依赖项(sql表依赖 请使用 SqlCacheDependency )</param>
        /// <param name="expriationTime">缓存绝对过期时间</param>
        public static void InsertCache(object value, string cacheKey, CacheDependency dependency, DateTime expriationTime)
        {
            if (GetCache(cacheKey) == null)
            {
                HttpRuntime.Cache.Insert(cacheKey, value, dependency, expriationTime, TimeSpan.Zero);
            }
        }

        /// <summary>
        /// 更新缓存值
        /// </summary>
        /// <param name="value">缓存值</param>
        /// <param name="cacheKey">缓存键值</param>
        public static void UpdateCache(object value, string cacheKey)
        {
            if (GetCache(cacheKey) == null)
            {
                InsertCache(value, cacheKey);
            }
            else
            {
                HttpRuntime.Cache[cacheKey] = value;
            }
        }
    }
}
