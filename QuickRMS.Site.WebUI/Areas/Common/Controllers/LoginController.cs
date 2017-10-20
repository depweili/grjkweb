using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Quick.Framework.Tool;
using QuickRMS.Core.Service.Authen;
using QuickRMS.Site.Models.Authen.User;
using Quick.Framework.Common.SecurityHelper;

namespace QuickRMS.Site.WebUI.Areas.Common.Controllers
{
	[Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoginController : Controller
	{
		//
		// GET: /Common/Login/

		#region 属性
		[Import]
        public IUserService UserService { get; set; }
		#endregion
	
        public ActionResult Index()
        {
            //TODO:TEST
           // var entity = UserService.Users.FirstOrDefault();
           // var model = new LoginModel();
            return View();
        }

        [HttpPost]
        public ActionResult CheckLogin(LoginModel model)
        {
            OperationResult result = new OperationResult(OperationResultType.Warning, "用户名或密码错误");
            var user = UserService.Users.FirstOrDefault(t => t.UserCode == model.LoginName && t.IsDeleted == false);
            if (user != null)
            {
				if (user.Enabled == false)
				{
					result = new OperationResult(OperationResultType.Warning, "你的账户已经被禁用");
				}
				else if (DESProvider.DecryptString(user.UserPwd) == model.LoginPwd)
				{
					//更新User
					user.LastLoginTime = DateTime.Now;
					user.LoginCount += 1;
					UserService.Update(user);

					result = new OperationResult(OperationResultType.Success, "登录成功");
					Session["CurrentUser"] = user;

					Session.Timeout = 20;
				}          
            }
            return Json(result);           
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Session["CurrentUser"] = null;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ForgetPwd()
        {
            return PartialView();
        }
	}
}