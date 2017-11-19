using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Quick.Site.Common.Models;

namespace QuickRMS.Site.Models.Authen.Device
{
    public class DeviceViewModel : EntityCommon
    {
        public DeviceViewModel()
        {
            Search = new SearchModel();
            AreaList = new List<SelectListItem>() {
                new SelectListItem { Text = "---选择区划---", Value = ""}, 
            };
        }
        public List<SelectListItem> AreaList { get; set; }
        public int Id { get; set; }
        [Display(Name = "设备编码")]
        [Required(ErrorMessage = "设备编码不能为空")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "设备编码{2}～{1}个字符")]
        public string DeviceCode { get; set; }
        [Display(Name = "设备名称")]
        [Required(ErrorMessage = "设备名称不能为空")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "设备名称{2}～{1}个字符")]
        public string DeviceName { get; set; }

         [Display(Name = "IP地址")]
        public string IP { get; set; }

        [Display(Name = "端口号")]
        public int? Port { get; set; }
        

        [Display(Name = "所属区域")]       
        public int? AreaId { get; set; }

       
        public string AreaName { get; set; }

        [Display(Name = "安装日期")]
        public DateTime? InstallTime { get; set; }

        [Display(Name = "安装日期")]
        public string InstallDate { get; set; }

        [Display(Name = "备注")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "备注{2}～{1}个字符")]
        public string Memo { get; set; }

   
        public bool IsInited { get; set; }

        [Display(Name = "是否在线")] 
        public bool IsOnline { get; set; }
       [Display(Name = "经度")] 
        public decimal? Longitude { get; set; }

        [Display(Name = "纬度")] 
        public decimal? Latitude { get; set; }
          [Display(Name = "公司")] 
        public string Company { get; set; }
          [Display(Name = "地址")] 
        public string Address { get; set; }

          public SearchModel Search { get; set; }

    }

    public class SearchModel
    {
        public SearchModel()
        {
            AreaList = new List<SelectListItem>() {
                new SelectListItem { Text = "---选择区划---", Value = "0"}, 
            };
        }

        [Display(Name = "设备名称")]
        public string Name { get; set; }

        [Display(Name = "设备编码")]
        public string DeviceCode { get; set; }

         [Display(Name = "所属区划")]
        public List<SelectListItem> AreaList { get; set; }
    }
}
