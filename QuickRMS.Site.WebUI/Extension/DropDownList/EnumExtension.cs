using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuickRMS.Site.WebUI.Extension.DropDownList
{
    public static class EnumExtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItem(this Enum valueEnum)
        {
            var values = Enum.GetValues(valueEnum.GetType());
            var result = from int value in values
                select
                    new SelectListItem
                    {
                        Text = Enum.GetName(valueEnum.GetType(), value),
                        Value = value.ToString(),
                        Selected = Enum.GetName(valueEnum.GetType(), value) == valueEnum.ToString() ? true : false
                    };
            return result;
        }

        public static List<SelectListItem> ToSelectListItem(this Enum valueEnum, string selectName)
        {
            return (from int value in Enum.GetValues(valueEnum.GetType())
                    select new SelectListItem
                    {
                        Text = Enum.GetName(valueEnum.GetType(), value),
                        Value = value.ToString(),
                        Selected = Enum.GetName(valueEnum.GetType(), value) == selectName ? true : false
                    }).ToList();
        }
    }
}