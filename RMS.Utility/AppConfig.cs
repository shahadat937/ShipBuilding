using System;
using System.Configuration;

namespace RMS.Utility
{
    public static class AppConfig
    {
        public static int PageSize
        {
            get
            {
                var appSettingPageSize = ConfigurationManager.AppSettings["PageSize"];
                if (string.IsNullOrEmpty(appSettingPageSize)) return 0;
                var pageSize = Convert.ToInt16(appSettingPageSize);
                return pageSize >= 0 ? pageSize : 0;
            }
        }
    }
}
