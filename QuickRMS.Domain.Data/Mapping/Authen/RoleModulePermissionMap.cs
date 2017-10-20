
using System;
using System.ComponentModel.DataAnnotations.Schema;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.Authen;


namespace QuickRMS.Domain.Data.Mapping.Authen
{
   
	partial class RoleModulePermissionMap
    {
		/// <summary>
		/// 映射配置
		/// </summary>
   /*  partial void RoleModulePermissionMapAppend()
        {
			// Primary Key
            this.HasKey(t => t.Id);

            // Properties 

            // Table & Column Mappings
            this.ToTable("Authen_RoleModulePermission");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
				 this.Property(t => t.RoleId).HasColumnName("RoleId"); 
		 		 this.Property(t => t.ModuleId).HasColumnName("ModuleId"); 
		 		 this.Property(t => t.PermissionId).HasColumnName("PermissionId"); 
		 		 this.Property(t => t.CreateId).HasColumnName("CreateId"); 
		 		 this.Property(t => t.CreateBy).HasColumnName("CreateBy"); 
		 		 this.Property(t => t.CreateTime).HasColumnName("CreateTime"); 
		 		 this.Property(t => t.ModifyId).HasColumnName("ModifyId"); 
		 		 this.Property(t => t.ModifyBy).HasColumnName("ModifyBy"); 
		 		 this.Property(t => t.ModifyTime).HasColumnName("ModifyTime"); 
		 		 this.Property(t => t.Id).HasColumnName("Id"); 
		 		 this.Property(t => t.IsDeleted).HasColumnName("IsDeleted"); 
		             // Relation
        }
		*/
    }
}
