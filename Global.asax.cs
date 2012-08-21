using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CBAM
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Scenario",                         // Route name
                "{projID}/Scenario/{action}/{id}", // URL with parameters
                new { controller = "Scenario", action = "Index", projID = "1", id = UrlParameter.Optional }  // Parameter defaults
            );

            routes.MapRoute(
               "Project",                    // Route name
                   "Project/{action}/{id}",  // URL with parameters
                   new { controller = "Project", action = "Index",  id = UrlParameter.Optional }  // Parameter defaults
               );

            routes.MapRoute(
                  "Home", // Route name
                  "Home/{action}/{id}", // URL with parameters
                  new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
              );

            routes.MapRoute(
                  "LogOn",                                              // Route name
                      "Account/{action}/{id}",                           // URL with parameters
                      new { controller = "Account", action = "Index", id = UrlParameter.Optional }  // Parameter defaults
                  );
            
            routes.MapRoute(
                "NewDefault",                            // Route name
                "{projID}/{controller}/{action}/{id}",   // URL with parameters
                new { controller = "Scenario", action = "Index", projID = "1", id = UrlParameter.Optional }  // Parameter defaults
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            // Catch-all for any unmatched URL
            routes.MapRoute(
                    "Error Catch-All",
                    "{*path}",
                    new { controller = "Home", action = "NotFound" } // NotFound doesn't exist, so HandleUnknownAction will be fired
            );


        }



        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}