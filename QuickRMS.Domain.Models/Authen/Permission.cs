using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Quick.Framework.Tool.Entity;
using QuickRMS.Domain.Models.SysConfig;

namespace QuickRMS.Domain.Models.Authen
{
    public class Permission : EntityBase<int>
    {
        public Permission()
        {
			this.ModulePermission = new List<ModulePermission>();
			this.RoleModulePermission = new List<RoleModulePermission>();
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public int OrderSort { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public int? CreateId { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public DateTime? ModifyTime { get; set; }

        public virtual ICollection<ModulePermission> ModulePermission { get; set; }
		public virtual ICollection<RoleModulePermission> RoleModulePermission { get; set; }
    }
}
