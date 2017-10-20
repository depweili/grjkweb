using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quick.Framework.Tool.Entity;

using QuickRMS.Domain.Models.SysConfig;

namespace QuickRMS.Domain.Models.Authen
{
    public class User : EntityBase<int>
    {
        public User()
        {
			this.UserRoles = new List<UserRole>();
			this.OperateLog = new List<OperateLog>();
        }

        public string UserCode { get; set; }
        public string UserPwd { get; set; }
        public string UserName { get; set; }

        public int? AreaID { get; set; }
        public string Memo { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public int? UserRole { get; set; }
        public bool Enabled { get; set; }
        public int PwdErrorCount { get; set; }
        public int LoginCount { get; set; }
        public DateTime? RegisterTime { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
		public virtual ICollection<OperateLog> OperateLog { get; set; }
        public virtual ICollection<UserArea> UserArea { get; set; }

    }
}
