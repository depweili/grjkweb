using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool.Entity;

namespace QuickRMS.Domain.Models.DeviceInfo
{
    public class History : EntityBase<int>
    {
        public int DeviceId { get; set; }
        public HistoryTypeCollect HistoryType { get; set; }
        public byte[] Data { get; set; }
        public int RowNumber { get; set; }


        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        public virtual Device Device { get; set; }
    }
}
