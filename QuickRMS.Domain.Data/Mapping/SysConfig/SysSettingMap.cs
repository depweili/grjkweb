
using System;
using System.ComponentModel.DataAnnotations.Schema;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.SysConfig;


namespace QuickRMS.Domain.Data.Mapping.SysConfig
{
   
	partial class SysSettingMap
    {
		/// <summary>
		/// 映射配置
		/// </summary>
   /*  partial void SysSettingMapAppend()
        {
			// Primary Key
            this.HasKey(t => t.Id);

            // Properties 

            // Table & Column Mappings
            this.ToTable("SysConfig_SysSetting");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
				 this.Property(t => t.SettingCode).HasColumnName("SettingCode"); 
		 		 this.Property(t => t.SettingValue).HasColumnName("SettingValue"); 
		 		 this.Property(t => t.Memo).HasColumnName("Memo"); 
		 		 this.Property(t => t.Id).HasColumnName("Id"); 
		 		 this.Property(t => t.IsDeleted).HasColumnName("IsDeleted"); 
		             // Relation
        }
		*/
    }
}
