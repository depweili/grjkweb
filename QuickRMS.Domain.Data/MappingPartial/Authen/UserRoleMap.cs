﻿
using System;
using System.ComponentModel.DataAnnotations.Schema;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.Authen;


namespace QuickRMS.Domain.Data.Mapping.Authen
{
   
	partial class UserRoleMap
    {
		/// <summary>
		/// 映射配置
		/// </summary>
  partial void UserRoleMapAppend()
        {
			// Primary Key
            this.HasKey(t => t.Id);

            // Properties 
            this.Property(t => t.CreateBy)
                     .HasMaxLength(50);

            this.Property(t => t.ModifyBy)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Authen_UserRole");
                 this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
                 this.Property(t => t.UserId).HasColumnName("UserId"); 
		 		 this.Property(t => t.RoleId).HasColumnName("RoleId"); 

		 		 this.Property(t => t.CreateId).HasColumnName("CreateId"); 
		 		 this.Property(t => t.CreateBy).HasColumnName("CreateBy"); 
		 		 this.Property(t => t.CreateTime).HasColumnName("CreateTime"); 
		 		 this.Property(t => t.ModifyId).HasColumnName("ModifyId"); 
		 		 this.Property(t => t.ModifyBy).HasColumnName("ModifyBy"); 
		 		 this.Property(t => t.ModifyTime).HasColumnName("ModifyTime");
		 		 this.Property(t => t.IsDeleted).HasColumnName("IsDeleted"); 
		             // Relation
                 this.HasRequired(t => t.User).WithMany(d => d.UserRoles).HasForeignKey(f => f.UserId).WillCascadeOnDelete(true);
                 this.HasRequired(t => t.Role).WithMany(d => d.UserRole).HasForeignKey(f => f.RoleId).WillCascadeOnDelete(true);
        }
		
    }
}
