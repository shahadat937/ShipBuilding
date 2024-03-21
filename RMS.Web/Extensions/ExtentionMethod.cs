using System;

namespace RMS.Web.Extensions
{
    public static class ExtentionMethod
    {
        public static string ToDateFormate(this DateTime dateTime)
        {
                return dateTime.ToString("dd MMM yyyy");
        }
    }
}