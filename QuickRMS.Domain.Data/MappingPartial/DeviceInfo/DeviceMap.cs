
using System;
using System.ComponentModel.DataAnnotations.Schema;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.DeviceInfo;


namespace QuickRMS.Domain.Data.Mapping.DeviceInfo
{
   
	partial class DeviceMap
    {
        /// <summary>
        /// 映射配置
        /// </summary>
        partial void DeviceMapAppend()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties 

            this.Property(r => r.Longitude).HasPrecision(12, 6);
            this.Property(r => r.Latitude).HasPrecision(12, 6);
            // Table & Column Mappings
            this.ToTable("T_Devices");
            this.Property(t => t.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.DeviceCode).HasColumnName("DeviceCode");
            this.Property(t => t.DeviceName).HasColumnName("DeviceName");
            this.Property(t => t.IP).HasColumnName("IP");
            this.Property(t => t.Port).HasColumnName("Port");
            this.Property(t => t.CtrlMode).HasColumnName("CtrlMode");
            this.Property(t => t.AreaId).HasColumnName("AreaId");
            this.Property(t => t.InstallTime).HasColumnName("InstallTime");
            this.Property(t => t.Memo).HasColumnName("Memo");
            this.Property(t => t.WorkMode).HasColumnName("WorkMode");
            this.Property(t => t.SaveInterval).HasColumnName("SaveInterval");
            this.Property(t => t.CtrlNumber).HasColumnName("CtrlNumber");
            this.Property(t => t.IsInited).HasColumnName("IsInited");
            this.Property(t => t.OutdoorFix).HasColumnName("OutdoorFix");
            this.Property(t => t.SupplyWaterFix1).HasColumnName("SupplyWaterFix1");
            this.Property(t => t.SupplyWaterFix2).HasColumnName("SupplyWaterFix2");
            this.Property(t => t.BackFix1).HasColumnName("BackFix1");
            this.Property(t => t.BackFix2).HasColumnName("BackFix2");
            this.Property(t => t.FixWater1).HasColumnName("FixWater1");
            this.Property(t => t.FixWater2).HasColumnName("FixWater2");
            this.Property(t => t.IsOnline).HasColumnName("IsOnline");
            this.Property(t => t.Longitude).HasColumnName("Longitude");
            this.Property(t => t.Latitude).HasColumnName("Latitude");

            this.Property(t => t.Company).HasColumnName("Company");
            this.Property(t => t.Address).HasColumnName("Address");

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
