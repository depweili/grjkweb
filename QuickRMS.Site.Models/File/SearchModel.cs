using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using QuickRMS.Domain.Models.Authen;


namespace QuickRMS.Site.Models.File
{
    public class SearchModel
    {
        public SearchModel()
        {
            FileTypes = new List<SelectListItem> { 
                   new SelectListItem { Text = "--- 请选择 ---", Value = "-1", Selected = true }, 
                //new SelectListItem { Text = "保险条款", Value = "10" }, 
                new SelectListItem { Text = "合同协议", Value = "11" }, 
                new SelectListItem { Text ="保险案例", Value = "12" },
                  new SelectListItem { Text ="方案", Value = "13" },
                    new SelectListItem { Text ="标书", Value = "14" },
                  new SelectListItem { Text ="其它", Value = "19" }
            };
        }

        [Display(Name = "上传日期")]
        [DataType(DataType.DateTime)]
        public DateTime? Date1 { get; set; }

        [Display(Name = "截至日期")]
        [DataType(DataType.DateTime)]
        public DateTime? Date2 { get; set; }

        [Display(Name = "文件名称")]
        public string FileName { get; set; }

        [Display(Name = "文件描述")]
        public string Memo { get; set; }

         [Display(Name = "文件类型")]
        public List<SelectListItem> FileTypes { get; set; }

         public int? FileType { get; set; }

       
    }
}
