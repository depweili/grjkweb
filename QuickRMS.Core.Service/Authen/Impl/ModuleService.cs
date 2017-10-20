﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//	   如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="ModuleService.cs">
//		Copyright(c)2013 QuickFramework.All rights reserved.
//		开发组织：QuickFramework
//		公司网站：QuickFramework
//		所属工程：QuickRMS.Core.Service
//		生成时间：2013-12-11 23:55
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using System.Linq;

using Quick.Framework.Tool;
using QuickRMS.Domain.Models.Authen;
using QuickRMS.Core.Repository.Authen;
using QuickRMS.Site.Models.Authen.Module;


namespace QuickRMS.Core.Service.Authen.Impl
{
	/// <summary>
    /// 服务层实现 —— ModuleService
    /// </summary>
    [Export(typeof(IModuleService))]
    public class ModuleService : CoreServiceBase, IModuleService
	{
		#region 属性

		[Import]
        public IModuleRepository ModuleRepository { get; set; }

        public IQueryable<Module> Modules
        {
            get { return ModuleRepository.Entities; }
        }

		#endregion

		#region 公共方法

		public OperationResult Insert(ModuleModel model)
        {
            var entity = new Module
            {
                Name = model.Name,
                Code = model.Code,
                ParentId = model.ParentId != 0 ? model.ParentId : null,
                LinkUrl = model.LinkUrl,
                Area = model.Area,
                Controller = model.Controller,
                Action = model.Action,
                OrderSort = model.OrderSort,
                Icon = model.Icon != null ? model.Icon : "",
                Enabled = model.Enabled
            };
            ModuleRepository.Insert(entity);
            return new OperationResult(OperationResultType.Success, "添加成功");
        }

        public OperationResult Update(ModuleModel model)
        {
            var entity = new Module
            {
                Id = model.Id,
                Name = model.Name,
                Code = model.Code,
                ParentId = model.ParentId != 0 ? model.ParentId : null,
                LinkUrl = model.LinkUrl,
                Area = model.Area,
                Controller = model.Controller,
                Action = model.Action,
                OrderSort = model.OrderSort,
				Icon = model.Icon != null ? model.Icon : "",
                Enabled = model.Enabled
            };          
            ModuleRepository.Update(entity);
            return new OperationResult(OperationResultType.Success, "更新成功");
        }

        public OperationResult Delete(int Id)
        {
			var model = Modules.FirstOrDefault(t => t.Id == Id);
			model.IsDeleted = true;

			ModuleRepository.Update(model);
			return new OperationResult(OperationResultType.Success, "删除成功");
		}

		#endregion
	}
}
