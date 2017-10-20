using Quick.Site.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Quick.Site.Common
{
    //public class ConfigSettings
    //{
    //    /// <summary>
    //    /// 应用程序加载特定的Action动作
    //    /// </summary>
    //    /// <returns></returns>
    //    public static List<MVCModuleModel> GetAllModule()
    //    {
    //        var moduleModel = new List<MVCModuleModel>();
    //        if (HttpContext.Current.Application["MVCModule"] == null)
    //        {
    //            moduleModel = SysAction.GetAllModule();
    //            HttpContext.Current.Application["MVCModule"] = moduleModel;
    //        }
    //        else
    //        {
    //            moduleModel = (List<MVCModuleModel>)HttpContext.Current.Application["moduleModel"];
    //        }
    //        return moduleModel;
    //    }
    //}
}
