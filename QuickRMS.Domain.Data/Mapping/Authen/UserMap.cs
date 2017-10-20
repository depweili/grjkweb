
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
   /*  partial void UserMapAppend()
        {
			// Primary Key
            this.HasKey(t => t.Id);

            // Properties 

            // Table & Column Mappings
            this.ToTable("Authen_User");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
				 this.Property(t => t.LoginName).HasColumnName("LoginName"); 
		 		 this.Property(t => t.LoginPwd).HasColumnName("LoginPwd"); 
		 		 this.Property(t => t.FullName).HasColumnName("FullName"); 
		 		 this.Property(t => t.Email).HasColumnName("Email"); 
		 		 this.Property(t => t.Phone).HasColumnName("Phone"); 
		 		 this.Property(t => t.Address).HasColumnName("Address"); 
		 		 this.Property(t => t.Enabled).HasColumnName("Enabled"); 
		 		 this.Property(t => t.PwdErrorCount).HasColumnName("PwdErrorCount"); 
		 		 this.Property(t => t.LoginCount).HasColumnName("LoginCount"); 
		 		 this.Property(t => t.RegisterTime).HasColumnName("RegisterTime"); 
		 		 this.Property(t => t.LastLoginTime).HasColumnName("LastLoginTime"); 
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
