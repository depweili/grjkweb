
using System;
using System.ComponentModel.DataAnnotations.Schema;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.Authen;


namespace QuickRMS.Domain.Data.Mapping.Authen
{
   
	partial class AreaMap
    {
		/// <summary>
		/// 映射配置
		/// </summary>
  partial void AreaMapAppend()
        {
			// Primary Key
            this.HasKey(t => t.Id);

            // Properties 
            this.Property(r => r.Longitude).HasPrecision(12, 6);
            this.Property(r => r.Latitude).HasPrecision(12, 6);
            this.Property(t => t.Name)
                       .HasMaxLength(50);
            this.Property(t => t.Code)
                            .HasMaxLength(50);
            this.Property(t => t.Description)
                .HasMaxLength(100);

            this.Property(t => t.CreateBy)
                .HasMaxLength(50);

            this.Property(t => t.ModifyBy)
                .HasMaxLength(50);
            // Table & Column Mappings
            this.ToTable("T_Areas");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
				 this.Property(t => t.Code).HasColumnName("Code"); 
		 		 this.Property(t => t.ParentId).HasColumnName("ParentId"); 
		 		 this.Property(t => t.Name).HasColumnName("Name"); 
		 		 this.Property(t => t.Description).HasColumnName("Description"); 
		 		 this.Property(t => t.IsLeaf).HasColumnName("IsLeaf"); 
		 		 this.Property(t => t.CreateId).HasColumnName("CreateId"); 
		 		 this.Property(t => t.CreateBy).HasColumnName("CreateBy"); 
		 		 this.Property(t => t.CreateTime).HasColumnName("CreateTime"); 
		 		 this.Property(t => t.ModifyId).HasColumnName("ModifyId"); 
		 		 this.Property(t => t.ModifyBy).HasColumnName("ModifyBy"); 
		 		 this.Property(t => t.ModifyTime).HasColumnName("ModifyTime"); 
		 		 this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
                 this.Property(t => t.Longitude).HasColumnName("Longitude");
                 this.Property(t => t.Latitude).HasColumnName("Latitude"); 
		             // Relation
                 this.HasOptional(t => t.ParentArea).WithMany(t => t.ChildArea).HasForeignKey(d => d.ParentId);
        }
		
    }
}
