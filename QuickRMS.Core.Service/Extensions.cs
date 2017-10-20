using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRMS.Core.Service
{
   public static class Extensions
    {
       public static DateTime? TryGetDate(this object value)
       {
           var str = value.GetString();
           if (string.IsNullOrEmpty(str)) return null;

           DateTime result;
           bool isParsed = DateTime.TryParse(str, out result);

           if (isParsed)
               return result;

           return null;
       }

       public static DateTime GetDate(this object value)
       {
           var str = value.GetString();
           DateTime result;
           bool isParsed = DateTime.TryParse(str, out result);

           if (isParsed)
               return result;

           else
           {
               throw new Exception(string.Format("【{0}】值日期格式错误",str));
           }
       }

       public static string GetString(this object value)
       {
           if (value == null)
               return "";
           return value.ToString();
       }

       public static decimal? TryGetDecimal(this object value)
       {
           var str = value.GetString();
           decimal ret = 0;
           bool isParsed = decimal.TryParse(str, out ret);

           if (isParsed)
               return ret;
           return null;
       }

       public static int GetInt(this object value,int defVal)
       {
           var str = value.GetString();
           int ret = 0;
           bool isParsed = int.TryParse(str, out ret);

           if (isParsed)
               return ret;
           return defVal;
       }

       public static TEnum GetEnum<TEnum>(this string value) where TEnum : struct
       {
           var def = default(TEnum);
           var ret = Enum.TryParse<TEnum>(value, out def);
           return def;
       }
    }
}
