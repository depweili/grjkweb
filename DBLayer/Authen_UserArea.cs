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
    
    public partial class Authen_UserArea
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AreaId { get; set; }
        public Nullable<int> CreateId { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> ModifyId { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual T_Areas T_Areas { get; set; }
        public virtual T_Users T_Users { get; set; }
    }
}
