using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RMS.Web.App_Start;
using RMS.Web.Helpers;

namespace RMS.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
          // ModelBinders.Binders.Add(typeof(DateTime), new CustomDateBinder());
            // ModelBinders.Binders.Add(typeof(DateTime?), new NullableCustomDateBinder());
            log4net.Config.XmlConfigurator.Configure();
            DipendencyConfig.SetupContainer();
           
        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            //GlobalFilters.Filters.Add(new SessionExpireFilterAttribute());
        }
    }
}
