using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quick.Framework.Tool;
using QuickRMS.Domain.Models.Authen;
using QuickRMS.Site.Models.Authen.RoleModulePermission;
using QuickRMS.Site.Models.Authen.UserArea;


namespace QuickRMS.Core.Service.Authen
{
    public interface IUserAreaService
    {
        #region 属性

        IQueryable<UserArea> UserAreas { get; }

        #endregion

        #region 公共方法
        OperationResult SetUserArea(UserAreaModel model);
        OperationResult RemoveUserArea(UserAreaModel model);
        #endregion
    }
}
