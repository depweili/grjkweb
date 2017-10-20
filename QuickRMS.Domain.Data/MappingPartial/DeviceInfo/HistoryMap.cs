
using System;
using System.ComponentModel.DataAnnotations.Schema;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.DeviceInfo;


namespace QuickRMS.Domain.Data.Mapping.DeviceInfo
{
   
	partial class HistoryMap
    {
		/// <summary>
		/// 映射配置
		/// </summary>
   partial void HistoryMapAppend()
        {
			// Primary Key
            this.HasKey(t => t.Id);

            // Properties 

            // Table & Column Mappings
            this.ToTable("T_History");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
				 this.Property(t => t.DeviceId).HasColumnName("DeviceId"); 
		 		 this.Property(t => t.HistoryType).HasColumnName("HistoryType"); 
		 		 this.Property(t => t.Data).HasColumnName("Data"); 
		 		 this.Property(t => t.RowNumber).HasColumnName("RowNumber"); 
		 		 this.Property(t => t.CreateId).HasColumnName("CreateId"); 
		 		 this.Property(t => t.CreateBy).HasColumnName("CreateBy"); 
		 		 this.Property(t => t.CreateTime).HasColumnName("CreateTime"); 
		 		 this.Property(t => t.ModifyId).HasColumnName("ModifyId"); 
		 		 this.Property(t => t.ModifyBy).HasColumnName("ModifyBy"); 
		 		 this.Property(t => t.ModifyTime).HasColumnName("ModifyTime"); 
		 		
		 		 this.Property(t => t.IsDeleted).HasColumnName("IsDeleted"); 
		             // Relation


                 this.HasRequired(t => t.Device).WithMany(d => d.Histories).HasForeignKey(f => f.DeviceId).WillCascadeOnDelete(true);
       
        }
		
    }
}
