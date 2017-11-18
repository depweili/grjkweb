using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using QuickRMS.Domain.Data.Initialize;
using QuickRMS.Site.WebUI.Extension.ViewEngine;
using QuickRMS.Site.WebUI.Extension.ModelBinder;
using System.Web.Http;

namespace QuickRMS.Site.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

			//自定义View
			ViewEngines.Engines.Clear();
			ExtendedRazorViewEngine engine = new ExtendedRazorViewEngine();
			engine.AddPartialViewLocationFormat("~/Areas/Common/Views/Shared/{0}.cshtml");
			engine.AddPartialViewLocationFormat("~/Areas/Common/Views/Shared/{0}.vbhtml");
			ViewEngines.Engines.Add(engine);

			//Model去除前后空格
			ModelBinders.Binders.DefaultBinder = new TrimModelBinder();

            //设置MEF依赖注入容器
            MefConfig.RegisterMef();

            String configfilename = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            RemotingConfiguration.Configure(configfilename);

			//初始化DB
          //  DatabaseInitializer.Initialize();
        }
    }
}
