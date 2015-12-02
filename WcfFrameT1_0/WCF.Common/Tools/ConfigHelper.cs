using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WCF.Common.Tools
{
    public static class ConfigHelper
    {
        #region Base

        /*ConnectionStrings节点*/
        public static string GetConfigConnnString(string configName)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[configName].ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string GetConfigConnnString(string configName, string defaultName)
        {
            try
            {
                return ConfigurationManager.ConnectionStrings[configName].ToString();
            }
            catch (Exception)
            {
                return defaultName;
            }
        }
        public static void SetConfigConnnStringValue(string configName, string configValue)
        {
            try
            {
                ConfigurationManager.ConnectionStrings.Remove(configName);
                ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings(configName, configValue));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /*AppSettings节点*/
        public static string GetAppSettingsString(string configName)
        {
            try
            {
                return ConfigurationManager.AppSettings[configName].ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public static string GetAppSettingsString(string configName, string defaultName)
        {
            try
            {
                return ConfigurationManager.AppSettings[configName].ToString();
            }
            catch (Exception)
            {
                return defaultName;
            }
        }
        public static void SetAppSettingsString(string configName, string configValue)
        {
            try
            {
                 ConfigurationManager.AppSettings.Set(configName, configValue);
            }
            catch (Exception e)
            {                
                throw e;
            }
        }



        #endregion

        #region Public Dbprovider

        /*DBhelper*/
        public static string GetConnectionString()
        {
            return GetConfigConnnString("ConnectionString");
        }
        public static string GetConnectionString(string configName)
        {
            return GetAppSettingsString(configName);
        }

        /*MongodbHelper*/
        public static string GetMongoCollections()
        {
            return GetConfigConnnString("MongoCollections");
        }
        public static string GetMongoDbName()
        {
            return GetConfigConnnString("MongoDBName");
        }
        #endregion
    }
}
