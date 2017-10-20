using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool.Entity;

namespace QuickRMS.Domain.Models.Authen
{
    public class UserArea : EntityBase<int>
    {
        public UserArea()
        {
            
        }
        public int UserId { get; set; }
        public int AreaId { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        public virtual User User { get; set; }
        public virtual Area Area { get; set; }
    }
}
