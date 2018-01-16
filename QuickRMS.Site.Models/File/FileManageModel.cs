using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Site.Common.Models;
using QuickRMS.Domain.Models.Authen;

namespace QuickRMS.Site.Models.File
{
    public class FileManageModel : EntityCommon
    {
        public FileManageModel()
        {
            Search = new SearchModel();
        }
        public int Id { get; set; }
        [Display(Name = "文件名称")]
        [Required(ErrorMessage = "文件名称不能为空")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "文件名称{2}～{1}个字符")]
        public string FileName { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }
        public int FileSize { get; set; }

         [Display(Name = "文件类型")]
        public FileType FileType { get; set; }

        [Display(Name = "文件描述")]
        public string Memo { get; set; }

        public string FilePath { get; set; }

        public SearchModel Search { get; set; }
    }
}
