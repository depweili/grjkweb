using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool.Entity;
using QuickRMS.Domain.Models.DeviceInfo;


namespace QuickRMS.Domain.Models.Authen
{
    public class Area : EntityBase<int>
    {
        public Area()
        {
            this.UserArea = new List<UserArea>();
            this.ChildArea = new List<Area>();
        }

        public string Code { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsLeaf { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public decimal? Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public decimal? Latitude { get; set; }

        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        public virtual Area ParentArea { get; set; }
        public virtual ICollection<Area> ChildArea { get; set; }
        public virtual ICollection<UserArea> UserArea { get; set; }

        public virtual ICollection<Device> Devices { get; set; }

    }
}
