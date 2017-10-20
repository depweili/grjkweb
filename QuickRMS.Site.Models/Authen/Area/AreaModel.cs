using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Quick.Site.Common.Models;

namespace QuickRMS.Site.Models.Authen.Area
{
    public class AreaModel : EntityCommon
    {
        public AreaModel()
        {
            Search = new SearchModel();
            Enabled = true;
        }

        public int Id { get; set; }

        [Display(Name = "机构名称")]
        [Required(ErrorMessage = "机构名称不能为空")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "机构名称{2}～{1}个字符")]
        public string Name { get; set; }

        [Display(Name = "机构描述")]
        public string Description { get; set; }

       
        public int? ParentId { get; set; }

        [Display(Name = "上级机构")]
        public string ParentName { get; set; }

        [Display(Name = "排序")]
        [Required(ErrorMessage = "排序不能为空")]
        [RegularExpression(@"\d+", ErrorMessage = "排序必须是数字")]
        public int OrderSort { get; set; }

        [Display(Name = "是否激活")]
        public bool Enabled { get; set; }

        public string EnabledText
        {
            get
            {
                if (Enabled == true)
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
            set { }
        }

        public SearchModel Search { get; set; }
    }

    public class SearchModel
    {
        public SearchModel()
        {
            EnabledItems = new List<SelectListItem>
            {
                new SelectListItem {Text = "--- 请选择 ---", Value = "-1", Selected = true},
                new SelectListItem {Text = "是", Value = "1"},
                new SelectListItem {Text = "否", Value = "0"}
            };
        }

        [Display(Name = "机构名称")]
        public string Name { get; set; }

        [Display(Name = "是否已激活")]
        public bool Enabled { get; set; }

        public List<SelectListItem> EnabledItems { get; set; }
    }
}

