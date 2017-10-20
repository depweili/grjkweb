using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Site.Common.Models;
using QuickRMS.Domain.Models.DeviceInfo;

namespace QuickRMS.Site.Models.Device
{
    public class HistoryModel : EntityCommon
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int HistoryTypeId { get; set; }
        public string HistoryTypeName { get; set; }
        public byte[] Data { get; set; }
        public int RowNumber { get; set; }
    }
}
