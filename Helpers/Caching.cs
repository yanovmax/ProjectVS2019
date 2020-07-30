using RSS_Reader.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace RSS_Reader.Helpers
{
    public class Caching
    {

        public static object GetCache(string key)
        {
            try
            {
                Cache cacheObject = HttpRuntime.Cache;
                return cacheObject.Get(key);
            }

            catch (Exception ex)
            {
                Log.Write(ex.Message, "Helpers", "Caching", "GetCache");
                return null;
            }
        }

        public static T GetCache<T>(string key)
        {
            try
            {
                Cache cacheObject = HttpRuntime.Cache;
                return (T)cacheObject.Get(key);
            }

            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Log.Write(ex.Message, "Helpers", "Caching", "GetCache");
                return default(T);
            }
        }
        public static void SetCache(string key, object value)
        {
            try
            {
                SetCache(key, value, null);
            }

            catch (Exception ex)
            {
                Log.Write(ex.Message, "Helpers", "Caching", "SetCache");
            }
        }

        public static void SetCache(string key, object value, DateTime? expirationDate)
        {
            Cache cacheObject = HttpRuntime.Cache;
            try
            {
                if (!string.IsNullOrEmpty(key) && value != null)
                {
                    if (expirationDate.HasValue)
                    {
                        cacheObject.Insert(key, value, null, expirationDate.Value, Cache.NoSlidingExpiration);
                    }
                    else
                    {
                        cacheObject.Insert(key, value);
                    }
                }
            }

            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Log.Write(ex.Message, "Helpers", "Caching", "SetCache");
            }
        }

        public static void SetCache(string key, object value, BaseEnums.CacheDuration duration)
        {

            SetCache(key, value, getCacheDutaion(duration));

        }
        
        private static DateTime getCacheDutaion(BaseEnums.CacheDuration duration)
        {
            switch (duration)
            {
                case BaseEnums.CacheDuration.Small:
                    return DateTime.Now.AddMinutes(4);
                case BaseEnums.CacheDuration.Medium:
                    return DateTime.Now.AddMinutes(30);
                case BaseEnums.CacheDuration.Large:
                    return DateTime.Now.AddHours(2);
                case BaseEnums.CacheDuration.Huge:
                    return DateTime.Now.AddHours(4);
                default:
                    return DateTime.Now.AddMinutes(10);
            }
        }
    }
}