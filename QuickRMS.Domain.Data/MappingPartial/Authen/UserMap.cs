
using System;
using System.ComponentModel.DataAnnotations.Schema;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.Authen;


namespace QuickRMS.Domain.Data.Mapping.Authen
{
   
	partial class UserMap
    {
		/// <summary>
		/// 映射配置
		/// </summary>
     partial void UserMapAppend()
        {
			// Primary Key
            this.HasKey(t => t.Id);

          
            // Properties
            this.Property(t => t.UserCode)
                .HasMaxLength(50);

            this.Property(t => t.UserPwd)
                .HasMaxLength(50);

            this.Property(t => t.UserName)
                .HasMaxLength(50);

            this.Property(t => t.Memo)
                .HasMaxLength(100);

            this.Property(t => t.Tel)
                .HasMaxLength(20);

            this.Property(t => t.CreateBy)
                .HasMaxLength(50);

            this.Property(t => t.ModifyBy)
                .HasMaxLength(50);
            // Table & Column Mappings
            this.ToTable("T_Users");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.UserCode).HasColumnName("UserCode");
            this.Property(t => t.UserPwd).HasColumnName("UserPwd");
            this.Property(t => t.UserName).HasColumnName("UserName");
                 this.Property(t => t.Memo).HasColumnName("Memo");
                 this.Property(t => t.Tel).HasColumnName("Tel"); 
		 		 this.Property(t => t.Address).HasColumnName("Address"); 
		 		 this.Property(t => t.Enabled).HasColumnName("Enabled"); 
		 		 this.Property(t => t.PwdErrorCount).HasColumnName("PwdErrorCount"); 
		 		 this.Property(t => t.LoginCount).HasColumnName("LoginCount"); 
		 		 this.Property(t => t.RegisterTime).HasColumnName("RegisterTime"); 
		 		 this.Property(t => t.LastLoginTime).HasColumnName("LastLoginTime");
                 this.Property(t => t.UserRole).HasColumnName("UserRole");
                 this.Property(t => t.AreaID).HasColumnName("AreaID"); 

		 		 this.Property(t => t.CreateId).HasColumnName("CreateId"); 
		 		 this.Property(t => t.CreateBy).HasColumnName("CreateBy"); 
		 		 this.Property(t => t.CreateTime).HasColumnName("CreateTime"); 
		 		 this.Property(t => t.ModifyId).HasColumnName("ModifyId"); 
		 		 this.Property(t => t.ModifyBy).HasColumnName("ModifyBy"); 
		 		 this.Property(t => t.ModifyTime).HasColumnName("ModifyTime"); 
		 		 this.Property(t => t.IsDeleted).HasColumnName("IsDeleted"); 
		             // Relation
        }
		
    }
}
