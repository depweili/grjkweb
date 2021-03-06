//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Auth_Role
    {
        public Auth_Role()
        {
            this.Auth_RoleModulePermission = new HashSet<Auth_RoleModulePermission>();
            this.Authen_UserRole = new HashSet<Authen_UserRole>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int OrderSort { get; set; }
        public bool Enabled { get; set; }
        public Nullable<int> CreateId { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual ICollection<Auth_RoleModulePermission> Auth_RoleModulePermission { get; set; }
        public virtual ICollection<Authen_UserRole> Authen_UserRole { get; set; }
    }
}
