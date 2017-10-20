using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Framework.Tool
{
   
    public  class UpLoadMessage
    {
        public static string AddSuccess = "新增成功";
        public static string UpdSuccess = "更新成功";
        public static string RequiredError = "出单日期/业务员/业务归属/出单保险公司/保单号次不能为空";
        public static string TooManyYWY = "找到多个对应的业务员";
        public static string NoYWY = "没有找到对应的业务员";
        public static string Denied = "没有修改此保单的权限";
        public static string NoBDHC = "未找到该保单号的相关信息";

        public static string TooManyOrg = "找到多个对应的业务归属";
        public static string NoOrg = "没有找到对应的业务归属";
    }
}
