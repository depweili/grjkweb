//using Quick.Site.Common.Models;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;

//namespace Quick.Site.Common.Helper
//{
//    public class SysAction
//    {
//        /// <summary>
//        /// 获取Controller下的Action, 组合成LinkUrl提供给Module模块使用
//        /// </summary>
//        /// <returns></returns>
//        public static List<MVCModuleModel> GetAllModule()
//        {
//            var model = new List<MVCModuleModel>();

//            var types = Assembly.Load("QuickRMS.Site.WebUI").GetTypes();

//            foreach (var type in types)
//            {
//                if (type.BaseType.Name == "AdminController")
//                {
//                    var members = type.GetMethods();
//                    foreach (var member in members)
//                    {
//                        if (member.ReturnType.Name == "ActionResult")
//                        {
//                            object[] attrs = member.GetCustomAttributes(typeof(DescriptionAttribute), true);
//                            if(attrs.Length > 0)
//                            { 
//                                var item = new MVCModuleModel();
//                                item.AreaName = "test";
//                                item.ActionName = member.Name;
//                                item.ControllerName = member.DeclaringType.Name.Substring(0, member.DeclaringType.Name.Length - 10);
//                                item.LinkUrl = string.Format("{0}/{1}/{2}", item.AreaName, item.ControllerName, item.ActionName);
//                                model.Add(item);
//                            }
//                        }
//                    }
//                }
//            }
//            return model;
//        }
//    }
//}
