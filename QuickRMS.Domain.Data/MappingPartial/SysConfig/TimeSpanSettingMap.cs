
using System;
using System.ComponentModel.DataAnnotations.Schema;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.SysConfig;


namespace QuickRMS.Domain.Data.Mapping.SysConfig
{
   
	partial class TimeSpanSettingMap
    {
		/// <summary>
		/// 映射配置
		/// </summary>
     partial void TimeSpanSettingMapAppend()
        {
			// Primary Key
            this.HasKey(t => t.Id);

            // Properties 

            // Table & Column Mappings
            this.ToTable("T_TimeSpanSettings");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
				 this.Property(t => t.DeviceId).HasColumnName("DeviceId"); 
		 		 this.Property(t => t.TimeSpanID).HasColumnName("TimeSpanID"); 
		 		 this.Property(t => t.StartTime).HasColumnName("StartTime"); 
		 		 this.Property(t => t.EndTime).HasColumnName("EndTime"); 
		 		 this.Property(t => t.CurveCode).HasColumnName("CurveCode"); 
		 		 this.Property(t => t.Flag).HasColumnName("Flag"); 
		 		
		 		 this.Property(t => t.IsDeleted).HasColumnName("IsDeleted"); 
		             // Relation
        }
		
    }
}
