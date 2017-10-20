﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//	   如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类进行实现。
// </auto-generated>
//
// <copyright file="UserService.cs">
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
using System.Collections.Generic;

using Quick.Framework.Tool;
using QuickRMS.Domain.Models.Authen;
using QuickRMS.Core.Repository.Authen;
using QuickRMS.Site.Models;
using QuickRMS.Site.Models.Authen.User;
using Quick.Framework.Common.SecurityHelper;


namespace QuickRMS.Core.Service.Authen.Impl
{
	/// <summary>
    /// 服务层实现 —— UserService
    /// </summary>
    [Export(typeof(IUserService))]
    public class UserService : CoreServiceBase, IUserService
    {
        #region 属性

        [Import]
        public IUserRepository UserRepository { get;set; }

        [Import]
        public IRoleRepository RoleRepository { get;set; }

        [Import]
        public IUserRoleRepository UserRoleRepository { get; set; }

        public IQueryable<User> Users
        {
            get { return UserRepository.Entities; }
        }

        public IQueryable<Role> Roles
        {
            get { return RoleRepository.Entities; }
        }

        public IQueryable<UserRole> UserRoles
        {
            get { return UserRoleRepository.Entities; }
        }

        #endregion

        #region 公共方法

        public OperationResult Insert(UserModel model) 
        {
            var entity = new User 
            {
                UserCode = model.LoginName,
				UserPwd = DESProvider.EncryptString(model.NewLoginPwd),
                UserName = model.FullName,
                Memo = model.Email,
                Tel = model.Phone,
                Enabled = model.Enabled,
                PwdErrorCount = 0,
                LoginCount = 0,
                RegisterTime = DateTime.Now,
				CreateId = model.CreateId,
				CreateBy = model.CreateBy,
				CreateTime = DateTime.Now,
				ModifyId = model.ModifyId,
				ModifyBy = model.ModifyBy,
				ModifyTime = DateTime.Now
            };		

            #region Add User Role Mapping

            foreach (int roleId in model.SelectedRoleList)
            {
                if (Roles.Any(t => t.Id == roleId))
                {
                    entity.UserRoles.Add(
						new UserRole() 
						{ 
							User = entity, 
							RoleId = roleId,
							CreateId = model.CreateId,
							CreateBy = model.CreateBy,
							CreateTime = DateTime.Now,
							ModifyId = model.ModifyId,
							ModifyBy = model.ModifyBy,
							ModifyTime = DateTime.Now
						});
                }
            } 

            #endregion

            UserRepository.Insert(entity);
			return new OperationResult(OperationResultType.Success, "添加成功");
        }

		/// <summary>
		/// 更新登录信息
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
        public OperationResult Update(User model)
        {
            UserRepository.Update(model);
			return new OperationResult(OperationResultType.Success);
        }

		/// <summary>
		/// 更新基本信息
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
        public OperationResult Update(UpdateUserModel model)
        {
            var entity = Users.FirstOrDefault(t => t.Id == model.Id);

            //entity.FullName = model.FullName;
            entity.Tel = model.Phone;
            entity.Enabled = model.Enabled;
			entity.ModifyId = model.ModifyId;
			entity.ModifyBy = model.ModifyBy;
			entity.ModifyTime = DateTime.Now;

            #region Update User Role Mapping
            var oldRoleIds = entity.UserRoles.Where(t => t.IsDeleted == false).Select(t => t.RoleId).ToList();
            var newRoleIds = model.SelectedRoleList.ToList();
            var intersectRoleIds = oldRoleIds.Intersect(newRoleIds).ToList(); // Same Ids
            var removeIds = oldRoleIds.Except(intersectRoleIds).ToList(); // Remove Ids
            var addIds = newRoleIds.Except(intersectRoleIds).ToList(); // Add Ids
            foreach (var removeId in removeIds)
            {
                //更新状态->物理删除0321
               // var userRole = UserRoles.FirstOrDefault(t => t.UserId == model.Id && t.RoleId == removeId);
                //userRole.IsDeleted = true;
                //userRole.ModifyId = model.ModifyId;
                //userRole.ModifyBy = model.ModifyBy;
                //userRole.ModifyTime = DateTime.Now;

                UserRoleRepository.Delete(t => t.UserId == model.Id && t.RoleId == removeId);
            }
            foreach (var addId in addIds)
            {
				var userRole = UserRoles.FirstOrDefault(t => t.UserId == model.Id && t.RoleId == addId);
				// 已有该记录，更新状态
				if (userRole != null)
				{
					userRole.IsDeleted = false;
					userRole.ModifyId = model.ModifyId;
					userRole.ModifyBy = model.ModifyBy;
					userRole.ModifyTime = DateTime.Now;
					UserRoleRepository.Update(userRole);			
				}
				// 插入
				else
				{
					entity.UserRoles.Add(new UserRole { 
						UserId = model.Id, 
						RoleId = addId,
						CreateId = model.CreateId,
						CreateBy = model.CreateBy,
						CreateTime = DateTime.Now,
						ModifyId = model.ModifyId,
						ModifyBy = model.ModifyBy,
						ModifyTime = DateTime.Now
					});
				}
            }
			
            #endregion
            
            UserRepository.Update(entity);           
			return new OperationResult(OperationResultType.Success, "更新成功");
        }

		/// <summary>
		/// 修改密码
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public OperationResult Update(ChangePwdModel model)
		{
			var entity = Users.FirstOrDefault(t => t.Id == model.Id);
			entity.UserPwd = DESProvider.EncryptString(model.NewLoginPwd);
			entity.ModifyId = model.ModifyId;
			entity.ModifyBy = model.ModifyBy;
			entity.ModifyTime = DateTime.Now;

			UserRepository.Update(entity);
			return new OperationResult(OperationResultType.Success, "修改密码成功");
		}

        public OperationResult Delete(UserModel model)
        {
			var entity = Users.FirstOrDefault(t => t.Id == model.Id);
			entity.IsDeleted = true;
			entity.ModifyId = model.ModifyId;
			entity.ModifyBy = model.ModifyBy;
			entity.ModifyTime = DateTime.Now;

			UserRepository.Update(entity);
            return new OperationResult(OperationResultType.Success, "删除成功");
        }

        #endregion
    }
}
