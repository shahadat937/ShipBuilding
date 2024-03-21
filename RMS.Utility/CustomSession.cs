using System.Web.SessionState;

namespace RMS.Utility
{
   public class CustomSession
    {
       private static HttpSessionState Session
       {
           get { return HttpHelper.Session; }
       }
       public static object GetValue(string key)
       {
           return Session[key];

       }
       public static object SetValue(string key, object value)
       {
           return Session[key] = value;

       }
    }
}
