﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//
// <copyright file="DeviceMaintenanceMap.generated.cs">
//		Copyright(c)2013 QuickFramework.All rights reserved.
//		开发组织：QuickFramework
//		公司网站：QuickFramework
//		所属工程：QuickRMS.Domain.Data
//		生成时间：2017-06-09 13:58
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

using Quick.Framework.EFData;
using QuickRMS.Domain.Models.DeviceInfo;


namespace QuickRMS.Domain.Data.Mapping.DeviceInfo
{
    /// <summary>
    /// 数据表映射 —— DeviceMaintenance
    /// </summary>    
	internal partial class DeviceMaintenanceMap : EntityTypeConfiguration<DeviceMaintenance>, IEntityMapper
    {
        /// <summary>
        /// DeviceMaintenance-数据表映射构造函数
        /// </summary>
        public DeviceMaintenanceMap()
        {
			DeviceMaintenanceMapAppend();
        }

		/// <summary>
        /// 额外的数据映射
        /// </summary>
        partial void DeviceMaintenanceMapAppend();
		
        /// <summary>
        /// 将当前实体映射对象注册到当前数据访问上下文实体映射配置注册器中
        /// </summary>
        /// <param name="configurations">实体映射配置注册器</param>
        public void RegistTo(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}