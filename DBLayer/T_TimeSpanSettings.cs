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
    
    public partial class T_TimeSpanSettings
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int TimeSpanID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int CurveCode { get; set; }
        public Nullable<int> Flag { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    }
}
