using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace StoredProcedureReports
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: "SprocDetails",
               url: "sproc-details/{sprocname}",
               defaults: new { controller = "Home", action = "Details", sprocname = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "SprocList",
                url: "sproc-list",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
