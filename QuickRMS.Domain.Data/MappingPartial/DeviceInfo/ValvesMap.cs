
using System;
using System.ComponentModel.DataAnnotations.Schema;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.DeviceInfo;


namespace QuickRMS.Domain.Data.Mapping.DeviceInfo
{

    partial class ValvesMap
    {
        /// <summary>
        /// 映射配置
        /// </summary>
        partial void ValvesMapAppend()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties 

            // Table & Column Mappings
            this.ToTable("T_Valves");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.DeviceId).HasColumnName("DeviceId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Step1).HasColumnName("Step1");
            this.Property(t => t.Step2).HasColumnName("Step2");
            this.Property(t => t.Step3).HasColumnName("Step3");
            this.Property(t => t.Step4).HasColumnName("Step4");
            this.Property(t => t.Step5).HasColumnName("Step5");
            this.Property(t => t.Steering).HasColumnName("Steering");
            this.Property(t => t.MaxStep).HasColumnName("MaxStep");
            this.Property(t => t.CtrlInterval).HasColumnName("CtrlInterval");
            this.Property(t => t.MinValue).HasColumnName("MinValue");
            this.Property(t => t.MaxValue).HasColumnName("MaxValue");
            this.Property(t => t.CurrentValue).HasColumnName("CurrentValue");
            this.Property(t => t.SetValue).HasColumnName("SetValue");
            this.Property(t => t.WorkMode).HasColumnName("WorkMode");
            this.Property(t => t.WorkBy).HasColumnName("WorkBy");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            // Relation
            this.HasRequired(t => t.Device)
                .WithMany(d => d.Valveses)
                .HasForeignKey(f => f.DeviceId)
                .WillCascadeOnDelete(true);
        }

    }
}
