
using System;
using System.ComponentModel.DataAnnotations.Schema;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.DeviceInfo;


namespace QuickRMS.Domain.Data.Mapping.DeviceInfo
{
   
	partial class DeviceMaintenanceMap
    {
		/// <summary>
		/// 映射配置
		/// </summary>
   partial void DeviceMaintenanceMapAppend()
        {
			// Primary Key
            this.HasKey(t => t.Id);

            // Properties 

            // Table & Column Mappings
            this.ToTable("T_DeviceMaintenances");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
				 this.Property(t => t.DeviceId).HasColumnName("DeviceId"); 
		 		 this.Property(t => t.MaintenanceDate).HasColumnName("MaintenanceDate"); 
		 		 this.Property(t => t.Memo).HasColumnName("Memo"); 
		 	
		 		 this.Property(t => t.IsDeleted).HasColumnName("IsDeleted"); 
		             // Relation

                 this.HasRequired(t => t.Device).WithMany(d => d.DeviceMaintenances).HasForeignKey(f => f.DeviceId).WillCascadeOnDelete(true);
        }
		
    }
}
