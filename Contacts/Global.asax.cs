using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Contacts.Models;

namespace Contacts
{
   public class MvcApplication : System.Web.HttpApplication
   {
      protected void Application_Start()
      {
//       Database.SetInitializer<CompanyContactsContext>( new CompanyContactsContextInitializer() );
         AreaRegistration.RegisterAllAreas();
         FilterConfig.RegisterGlobalFilters( GlobalFilters.Filters );
         RouteConfig.RegisterRoutes( RouteTable.Routes );
         BundleConfig.RegisterBundles( BundleTable.Bundles );
      }
   }
}
