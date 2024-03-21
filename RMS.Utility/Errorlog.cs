using System;
using log4net;


namespace RMS.Utility
{
    public static class Errorlog
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Utility.Errorlog));

        public static void WriteLog(Exception filterContext)
        {
            _log.Error(filterContext.Message + "\n" + Environment.StackTrace);
        }

    
    }
}
