using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AJ.UtiliTools
{
    public class UtiliSetting
    {
        #region Application Settings

        public static String AppSetting(String settingName)
        {
            return ConfigurationManager.AppSettings[settingName];
        }

        public static KeyValueConfigurationCollection ApplicationSettings()
        {
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");

            return appSettingsSection.Settings;
        }

        public static void UpdateApplicationSetting(String key, String value)
        {
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");

            if (appSettingsSection != null)
            {
                if (appSettingsSection.Settings.AllKeys.Contains(key))
                {
                    appSettingsSection.Settings[key].Value = value;
                }
                else
                {
                    appSettingsSection.Settings.Add(key, value);
                }

                configuration.Save();
            }
        }

        public static void DeleteApplicationSetting(String key)
        {
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            AppSettingsSection appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");

            if (appSettingsSection != null)
            {
                if (appSettingsSection.Settings.AllKeys.Contains(key))
                {
                    appSettingsSection.Settings.Remove(key);
                }

                configuration.Save();
            }
        }

        #endregion

        #region Connection Strings

        public static String ConnectionString(String connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        public static ConnectionStringSettingsCollection ConnectionString()
        {
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            ConnectionStringsSection conStringsSection = (ConnectionStringsSection)configuration.GetSection("connectionStrings");

            return conStringsSection.ConnectionStrings;
        }

        private void UpdateConnectionString(String name, String connectionString, String providerName)
        {
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            ConnectionStringsSection conStringsSection = (ConnectionStringsSection)configuration.GetSection("connectionStrings");

            if (conStringsSection != null)
            {
                if (conStringsSection.ConnectionStrings[name] != null)
                {
                    conStringsSection.ConnectionStrings[name].ConnectionString = connectionString;
                }
                else
                {
                    conStringsSection.ConnectionStrings.Add(new ConnectionStringSettings(name, connectionString));
                }

                if (!providerName.IsNullOrEmpty())
                    conStringsSection.ConnectionStrings[name].ProviderName = providerName;

                configuration.Save();
            }
        }

        private void DeleteConnectionString(String name)
        {
            Configuration configuration = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);

            ConnectionStringsSection conStringsSection = (ConnectionStringsSection)configuration.GetSection("connectionStrings");

            if (conStringsSection != null)
            {
                if (conStringsSection.ConnectionStrings[name] != null)
                {
                    conStringsSection.ConnectionStrings.Remove(name);
                }

                configuration.Save();
            }
        }

        #endregion
    }
}